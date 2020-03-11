using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour

{
    void Start() {
        string microphone = "Built-in Microphone";

        foreach(var device in Microphone.devices) {
            print(device);
            if (device.Contains("HTC Vive")) {
                print("Found microphone");
                microphone = device;
            }
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        print("Starting recording");
        audioSource.clip = Microphone.Start(microphone, true, 10, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) {}
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
