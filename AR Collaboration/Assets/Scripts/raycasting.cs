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
    public GameObject mark;
    private RaycastHit hit;
    private Vector3 hit_point;
    public VRKeys.Keyboard keyboard;
    public GameObject textholder;
    private Vector3 none;
    // Update is called once per frame

    private void OnEnable()
    {
        keyboard.SetPlaceholderMessage("Tap keys to begin typing...");

        keyboard.OnUpdate.AddListener(HandleUpdate);
        keyboard.OnSubmit.AddListener(HandleSubmit);
        keyboard.OnCancel.AddListener(HandleCancel);
        none =new Vector3 (0, 0, 0);
    }


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

    private void HandleSubmit(string text)
    {
        if (hit_point != none)
        {
            Debug.Log("submit:" + text);
            GameObject temp = Instantiate(textholder, hit.transform);
            temp.transform.position = hit_point;
            if (temp == null)
            {
                Debug.Log("create texthold failed");
            }
            temp.GetComponent<TextMesh>().text = text;
            //create object and asign text
            keyboard.Disable();
        }
    }

    private void HandleUpdate(string text)
    {
        Debug.Log("update:" + text);
    }

    void Update()
    {
        // the hit object which store the information of hitten object

        //The Raycast will return true with start position vector and diretion vector. iff the collide object is "Cube"
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity,1<<2))
        {
            //handle the handler trigger input 
            //  if (GetTrigger()) { 
            if (hit.collider.gameObject.name == "Cube")
            {

                //Debug.Log("hit object:" + hit.collider.gameObject.name + "xyz:" + hit.point);
                //create a sphere object as the child object of object been hitten("Cube")
                mark.SetActive(true);
                mark.transform.position = hit.point;
                //  }
                if (Input.GetKeyDown(KeyCode.Tab)&& keyboard.disabled)
                {
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
