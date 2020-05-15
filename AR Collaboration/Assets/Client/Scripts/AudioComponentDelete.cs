/*
This class handles grabbing audio from the microphone to send to other users.
Saves audio from microphone into Audio Clip object to be played from an Audio Source.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponentDelete : MonoBehaviour
{
    void Start()
    {
        string microphone = "Built-in Microphone";

        foreach (var device in Microphone.devices)
        {
            print(device);
            if (device.Contains("Webcam")) //for testing, to use with HTC Vive change search string to HTC
            {
                print("Found microphone");
                microphone = device;
            }
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        print("Starting recording");
        audioSource.clip = Microphone.Start(microphone, true, 1, 44100); //loops every 1 second, capturing the audio and overwriting the previous clip
        audioSource.loop = false; //playback loop is set to false so the same clip isn't looped
        while (!(Microphone.GetPosition(null) > 0)) { }
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
