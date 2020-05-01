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

    // Update is called once per frame
    void Update()
    {
        // the hit object which store the information of hitten object
        RaycastHit hit;

        //The Raycast will return true with start position vector and diretion vector. iff the collide object is "Cube"
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) && hit.collider.gameObject.name == "Cube")
        {
        //handle the handler trigger input 
            if (GetTrigger()) { 
            Debug.Log("hit object:" + hit.collider.gameObject.name + "xyz:" + hit.point);
         //create a sphere object as the child object of object been hitten("Cube")
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = scale;
            sphere.transform.position = hit.point;
            sphere.GetComponent<Collider>().enabled = false;
            sphere.transform.SetParent(hit.collider.gameObject.transform);
            }
        }
    }


    public bool GetTrigger() 
    {
        return Trigger.GetState(handType);
    }
}
