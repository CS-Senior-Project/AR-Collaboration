using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using System.IO;

using Unity.Collections;
using Unity.Networking.Transport;

public class ServerBehavior : MonoBehaviour
{
    public UdpNetworkDriver m_Driver;
    private NativeList<NetworkConnection> m_Connections;

    // Start is called before the first frame update
    void Start()
    {
        m_Driver = new UdpNetworkDriver(new INetworkParameter[0]);
        var endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = 9000;
        if (m_Driver.Bind(endpoint) != 0)
            Debug.Log("Failed to bind to port 9000");
        else {
            ObjectDump.Write(Console.out, endpoint);
            Debug.Log("Endpoint22: " + NetworkEndPoint.AnyIpv4);
            m_Driver.Listen();
        }

        m_Connections = new NativeList<NetworkConnection>(16, Allocator.Persistent);
    }

    void OnDestroy() {
        m_Driver.Dispose();
        m_Connections.Dispose();
    }

    // Update is called once per frame
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
                    var readerCtx = default(DataStreamReader.Context);

                    uint number = stream.ReadUInt(ref readerCtx);
                    Debug.Log("Got " + number + " from the Client adding + 2 to it.");

                    number +=2;

                    using (var writer = new DataStreamWriter(4, Allocator.Temp))
                    {
                        writer.Write(number);
                        m_Driver.Send(NetworkPipeline.Null, m_Connections[i], writer);
                    }
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

public static class ObjectDump
{
	public static void Write(TextWriter writer, object obj)
	{
		if (obj == null)
		{
			writer.WriteLine("Object is null");
			return;
		}

		writer.Write("Hash: ");
		writer.WriteLine(obj.GetHashCode());
		writer.Write("Type: ");
		writer.WriteLine(obj.GetType());

		var props = GetProperties(obj);

		if (props.Count > 0)
		{
			writer.WriteLine("-------------------------");
		}

		foreach (var prop in props)
		{
			writer.Write(prop.Key);
			writer.Write(": ");
			writer.WriteLine(prop.Value);
		}
	}

	private static Dictionary<string, string> GetProperties(object obj)
	{
		var props = new Dictionary<string, string>();
		if (obj == null)
			return props;

		var type = obj.GetType();
		foreach (var prop in type.GetProperties())
		{
			var val = prop.GetValue(obj, new object[] { });
			var valStr = val == null ? "" : val.ToString();
			props.Add(prop.Name, valStr);
		}

		return props;
	}
}
