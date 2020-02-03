using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


// This Script is the basics for how to get controller input! 
// Right now it is used to Make a gold sphere

public class MyActionScript : MonoBehaviour
{
    // This is a reference to the action
    public SteamVR_Action_Boolean SphereOnOff;

    // This is a reference to the hand (Sphere)
    public SteamVR_Input_Sources handType;

    // This is a reference to the sphere
    public GameObject Sphere;

    void Start()
    {
        SphereOnOff.AddOnStateDownListener(TriggerDown, handType);
        SphereOnOff.AddOnStateUpListener(TriggerUp, handType);
    }
    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
        Sphere.GetComponent<MeshRenderer>().enabled = false;
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        Sphere.GetComponent<MeshRenderer>().enabled = true;
    }

    void Update()
    {

    }
}
