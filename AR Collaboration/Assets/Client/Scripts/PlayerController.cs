/*
Controls sending player information.
FixedUpdate runs on ticks, will continue sending data to the server.
SendInputToServer sends player movement values to server, from test code.
SendAudioToServer grabs latest audio clip and sends it to the server every 1 second.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    DateTime lastAudioSend = DateTime.Now.AddSeconds(1);

    private void FixedUpdate()
    {
        SendInputToServer();
        if (lastAudioSend < DateTime.Now)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            lastAudioSend = lastAudioSend.AddSeconds(1);
            SendAudioToServer();
            Debug.Log($"Sending Audio to Server at {DateTime.Now}");
        }
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A)
        };

        ClientSend.PlayerMovement(_inputs);
    }

    private void SendAudioToServer()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        ClientSend.PlayerAudio(samples);
    }
}
