/**
 * 
 * Files created by the OSU ARC Senior Project Team
 * Carson Pemble
 * April 20, 2020
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRKeys;
using UnityEngine.SceneManagement;

public class PlaceText : MonoBehaviour { 

    // This is a reference to the action
    public SteamVR_Action_Boolean ActionBool;
    // This is a reference to the hand (Sphere)
    public SteamVR_Input_Sources handType_placeText;

    GameObject txtToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        ActionBool.AddOnStateDownListener(TriggerDown_keyboardCall, handType_placeText);
        ActionBool.AddOnStateUpListener(TriggerUp_keyboardCall, handType_placeText);
    }

    public void TriggerUp_keyboardCall(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up - from PlaceText");                    // Just a test to see if controller input is working.
    }
    public void TriggerDown_keyboardCall(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down - from PlaceText");                  // Just a test to see if controller input is working.

        GameObject hand = GameObject.Find("Left Hand");                 // Left Hand is the name of the game object we are using to place text.
        Transform handTransform = hand.GetComponent<Transform>();       // This gets a transform (the position) of the hand.

        txtToSpawn = new GameObject("User Text");                       // Create a new game object to hold the text
        txtToSpawn.AddComponent<TextMesh>().text = "";

        txtToSpawn.transform.position = handTransform.position + new Vector3(0f, 0.0f, 0.0f);     // Place on the controller

        txtToSpawn.GetComponent<TextMesh>().characterSize = 0.1f;           //
        txtToSpawn.GetComponent<TextMesh>().fontSize = 10;                  // Settings that seem to give the best results
        Color textColor = new Color(0f, 122f, 204f);                        //
        txtToSpawn.GetComponent<TextMesh>().color = textColor;

        Debug.Log("Updating Text");
        GameObject textHolder = GameObject.Find("TextHolder");
        txtToSpawn.GetComponent<TextMesh>().text = textHolder.GetComponent<TextMesh>().text;    // Add text to the newly created game object

    }

}
