using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRKeys;
using UnityEngine.SceneManagement;


// This Script is the basics for how to get controller input! 
// Right now it is used to Make a gold sphere

public class CallKeyboard : MonoBehaviour
{
    // This is a reference to the action
    public SteamVR_Action_Boolean ActionBool;
    // This is a reference to the hand (Sphere)
    public SteamVR_Input_Sources handType_keyboardCall;

    // For Notes
    public Keyboard keyboard;

    void Start()
    {
        ActionBool.AddOnStateDownListener(TriggerDown_keyboardCall, handType_keyboardCall);
        ActionBool.AddOnStateUpListener(TriggerUp_keyboardCall, handType_keyboardCall);

    }

    public void TriggerUp_keyboardCall(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up - from CallKeyboard");
    }
    public void TriggerDown_keyboardCall(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down - from CallKeyboard");

        Debug.Log("Enabling Keyboard");
        keyboard.Enable();

    }

}
