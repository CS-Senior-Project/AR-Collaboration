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
        Debug.Log("Trigger is up - from PlaceText");
    }
    public void TriggerDown_keyboardCall(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down - from PlaceText");

        GameObject hand = GameObject.Find("Left Hand");
        Transform handTransform = hand.GetComponent<Transform>();

        txtToSpawn = new GameObject("User Text");
        txtToSpawn.AddComponent<TextMesh>().text = "";

        txtToSpawn.transform.position = handTransform.position + new Vector3(0f, 0.0f, 0.0f);     // Place on the controller

        txtToSpawn.GetComponent<TextMesh>().characterSize = 0.1f;
        txtToSpawn.GetComponent<TextMesh>().fontSize = 10;
        Color textColor = new Color(0f, 122f, 204f);
        txtToSpawn.GetComponent<TextMesh>().color = textColor;

        Debug.Log("Updating Text");
        GameObject textHolder = GameObject.Find("TextHolder");
        txtToSpawn.GetComponent<TextMesh>().text = textHolder.GetComponent<TextMesh>().text;

    }

}
