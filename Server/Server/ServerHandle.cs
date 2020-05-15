/*
Handles incoming packets from client.
WelcomeReceived is used to connect server to client.
PlayerMovement is test code, handles receiving updates on player movement values from client.
PlayerAudio receives new player audio from client, updating the player's audio samples.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now Player {_fromClient}: {_username}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            bool[] _inputs = new bool[_packet.ReadInt()];
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadBool();
            }

            Quaternion _rotation = _packet.ReadQuaternion();

            Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
        }

        public static void PlayerAudio(int _fromClient, Packet _packet)
        {
            Console.WriteLine($"Receiving audio at {DateTime.Now}");
            float[] _samples = new float[_packet.ReadInt()];
            for (int i = 0; i < _samples.Length; i++)
            {
                _samples[i] = _packet.ReadFloat();
            }

            Server.clients[_fromClient].player.SetAudio(_samples);
        }

        /*public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received packet via UDP. Contains message: {_msg}");
        }*/
    }
}
