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
    GameObject txtToSpawn;

    void Start()
    {
        ActionBool.AddOnStateDownListener(TriggerDown_keyboardCall, handType_keyboardCall);
        ActionBool.AddOnStateUpListener(TriggerUp_keyboardCall, handType_keyboardCall);

    }

    public void TriggerUp_keyboardCall(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
    }
    public void TriggerDown_keyboardCall(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");

        GameObject hand = GameObject.Find("Right Hand");
        Transform handTransform = hand.GetComponent<Transform>();

        txtToSpawn = new GameObject("User Text");
        txtToSpawn.AddComponent<TextMesh>().text = "";

        txtToSpawn.transform.position = handTransform.position + new Vector3(-0.5f, 0.0f, 0.0f);     // Place on the controller

        txtToSpawn.GetComponent<TextMesh>().characterSize = 0.1f;
        txtToSpawn.GetComponent<TextMesh>().fontSize = 10;
        Color textColor = new Color(0f, 122f, 204f);
        txtToSpawn.GetComponent<TextMesh>().color = textColor;


        Debug.Log("Enabling Keyboard");
        keyboard.Enable();

        Debug.Log("Updating Text");
        GameObject textHolder = GameObject.Find("TextHolder");
        txtToSpawn.GetComponent<TextMesh>().text = textHolder.GetComponent<TextMesh>().text;



    
    }

    void Update()
    {

    }
}
