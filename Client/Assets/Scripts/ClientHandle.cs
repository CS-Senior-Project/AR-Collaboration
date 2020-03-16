using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        //GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        //GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void OtherPlayerAudio(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float[] _samples = new float[_packet.ReadInt()];
        for (int i = 0; i < _samples.Length; i++)
        {
            _samples[i] = _packet.ReadFloat();
        }

        Debug.Log($"PlayerID: {Client.instance.myId}");
        Debug.Log($"ID: {_id}");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Finish");

        Debug.Log($"Receiving audio from server at {DateTime.Now}");

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].GetComponent<PlayerManager>().id == _id)
            {
                AudioSource audioSource = objects[i].GetComponent<AudioSource>();
                audioSource.clip = AudioClip.Create($"player{_id}_audio", 44100 * 2, 1, 44100, false);
                audioSource.clip.SetData(_samples, 0);
                audioSource.Play();
                audioSource.clip = null;
            }
        }
    }
}
