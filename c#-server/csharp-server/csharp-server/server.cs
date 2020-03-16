using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Assertions;

using Unity.Collections;
using Unity.Networking.Transport;

namespace csharp_server
{
    public class Server: MonoBehaviour
    {

        public NetworkDriver m_Driver;
        private NativeList<NetworkConnection> m_Connections;

        void Start()
        {
            m_Driver = NetworkDriver.Create();
            var endpoint = NetworkEndPoint.AnyIpv4;
            endpoint.Port = 9000;
            if (m_Driver.Bind(endpoint) != 0)
                Debug.Log("Failed to bind to port 9000");
            else
                m_Driver.Listen();

            m_Connections = new NativeList<NetworkConnection>(16, Allocator.Persistent);
        }

        void OnDestroy()
        {
            m_Driver.Dispose();
            m_Connections.Dispose();
        }

        void Update()
        {
            m_Driver.ScheduleUpdate().Complete();

            for (int i = 0; i < m_Connections.Length; i++)
            {
                if (!m_Connections[i].IsCreated)
                {
                    m_Connections.RemoveAtSwapBack(i);
                    --i;
                }
            }

            NetworkConnection c;
            while ((c = m_Driver.Accept()) != default(NetworkConnection))
            {
                m_Connections.Add(c);
                Debug.Log("Accepted a connection");
            }

            DataStreamReader stream;
            for (int i = 0; i < m_Connections.Length; i++)
            {
                if (!m_Connections[i].IsCreated)
                    continue;

                NetworkEvent.Type cmd;
                while ((cmd = m_Driver.PopEventForConnection(m_Connections[i], out stream)) != NetworkEvent.Type.Empty)
                {
                    if (cmd == NetworkEvent.Type.Data)
                    {
                        uint number = stream.ReadUInt();
                        Debug.Log("Got " + number + " from the Client adding + 2 to it.");

                        number += 2;

                        var writer = m_Driver.BeginSend(NetworkPipeline.Null, m_Connections[i]);
                        writer.WriteUInt(number);
                        m_Driver.EndSend(writer);
                    }
                    else if (cmd == NetworkEvent.Type.Disconnect)
                    {
                        Debug.Log("Client disconnected from server");
                        m_Connections[i] = default(NetworkConnection);
                    }
                }
            }
        }
    }


}
