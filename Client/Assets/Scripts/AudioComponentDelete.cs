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
            if (device.Contains("Webcam"))
            {
                print("Found microphone");
                microphone = device;
            }
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        print("Starting recording");
        audioSource.clip = Microphone.Start(microphone, true, 1, 44100); //loops every 10 seconds and overwrites, send every something seconds?
        audioSource.loop = false;
        while (!(Microphone.GetPosition(null) > 0)) { }
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
