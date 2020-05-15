/*
Handles sending packets to the server.
Defines base send functions for both TCP/UDP.
WelcomeReceived is used to create the connection between client and server.
PlayerMovement sends player movement values from testing code.
PlayerAudio sends most recent audio clip captured from microphone to all other connected players.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{

    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }

    public static void PlayerAudio(float[] _samples)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerAudio))
        {
            _packet.Write(_samples.Length);
            foreach (float _sample in _samples)
            {
                _packet.Write(_sample);
            }
            SendTCPData(_packet);
        }
    }
}
