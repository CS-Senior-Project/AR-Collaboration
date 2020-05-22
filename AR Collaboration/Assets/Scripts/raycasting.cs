using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class raycasting : MonoBehaviour
{
    Vector3 scale = new Vector3(0.05f, 0.05f, 0.05f);
    //reference  to the action 
    public SteamVR_Action_Boolean Trigger;
    //reference of the input type
    public SteamVR_Input_Sources handType;
    //the mark object that indicate the cross point
    public GameObject mark;
    //The raycast object
    private RaycastHit hit;
    //The point that being hitted
    private Vector3 hit_point;
    //The keyboard reference
    public VRKeys.Keyboard keyboard;
    //The Prefab of 3d text to be created
    public GameObject textholder;
    //The null vector to distinguish two hands.
    private Vector3 none;

    private Transform parent;

    // called when script being enabled, link method to keyborad's callback listener
    private void OnEnable()
    {
        keyboard.SetPlaceholderMessage("Tap keys to begin typing...");

        keyboard.OnUpdate.AddListener(HandleUpdate);
        keyboard.OnSubmit.AddListener(HandleSubmit);
        keyboard.OnCancel.AddListener(HandleCancel);
        none =new Vector3 (0, 0, 0);
    }

    //called when script being disbaled , unlink method to keyborad's callback listener
    private void OnDisable()
    {
        keyboard.OnUpdate.RemoveListener(HandleUpdate);
        keyboard.OnSubmit.RemoveListener(HandleSubmit);
        keyboard.OnCancel.RemoveListener(HandleCancel);

        keyboard.Disable();
    }

    private void HandleCancel()
    {

    }
    
    //called when 'enter' key on the VRkeyboard being hitten, simply create new 3d text object with fixed postion and input string
    private void HandleSubmit(string text)
    {
        if (hit_point != none)
        {
            Debug.Log("submit:" + text);
            GameObject temp = Instantiate(textholder, parent);
            temp.transform.position = hit_point;
                if (temp == null)
            {
                Debug.Log("create texthold failed");
            }
            temp.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
            temp.GetComponent<TextMesh>().text = text;
            //create object and asign text

            Debug.Log("Updating Text");
            GameObject textHolder = GameObject.Find("TextHolder");
            textHolder.GetComponent<TextMesh>().text = text;    // Add text to the newly created game object
            textHolder.GetComponent<MeshRenderer>().enabled = true;

            keyboard.text = "";

            keyboard.Disable();
        }
    }

    private void HandleUpdate(string text)
    {
        Debug.Log("update:" + text);
    }


    //keep listening the raycasting result of the laser point, show the indicater object mark when cross with 'Cube',trigger to bring out the VRkey when crossing with one of the 'Cube's.
    void Update()
    {
        // the hit object which store the information of hitten object

        //The Raycast will return true with start position vector and diretion vector. iff the collide object is "Cube"
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity,1<<2))
        {
            if (hit.collider.gameObject.name == "Cube")
            {

                //Debug.Log("hit object:" + hit.collider.gameObject.name + "xyz:" + hit.point);
                mark.SetActive(true);
                mark.transform.position = hit.point;

                if ((Input.GetKeyDown(KeyCode.Tab) || GetTrigger()) && keyboard.disabled)
                {
                    parent = hit.transform;
                    hit_point = hit.point;
                    if(handType== SteamVR_Input_Sources.RightHand)
                    {
                        GameObject.FindWithTag("lefthand").GetComponent<raycasting>().setnull();
                    }
                    else
                    {
                        GameObject.FindWithTag("righthand").GetComponent<raycasting>().setnull();
                    }
                    keyboard.Enable();
                }
            }
            else
            {
             //   Debug.Log("non-cube object:" + hit.collider.gameObject.name + "xyz:" + hit.point);

            }

        }
        else
        {
           // Debug.Log("not hitten");
           mark.SetActive(false);
        }
    }


    public bool GetTrigger() 
    {
        return Trigger.GetState(handType);
    }

    public void setnull()
    {
        this.hit_point =new Vector3(0,0,0);
    }
}
