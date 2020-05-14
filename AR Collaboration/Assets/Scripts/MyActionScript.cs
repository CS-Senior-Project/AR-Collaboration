/**
 * 
 * Files created by the OSU ARC Senior Project Team
 * Carson Pemble
 * May 12, 2020
 * 
 * This Script is the template for how to get controller input!
 * Right now it is used to Make a gold sphere 
 * (Not used within the project, just to help the understanding of how to use the controller input)
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


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
        Sphere.GetComponent<MeshRenderer>().enabled = false;            // Leave the sphere meshless
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        Sphere.GetComponent<MeshRenderer>().enabled = true;            // Make the sphere gold
    }

    void Update()
    {

    }
}
