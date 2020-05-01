using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycasting : MonoBehaviour
{
    Vector3 scale = new Vector3(0.05f, 0.05f, 0.05f);

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) && hit.collider.gameObject.name == "Cube")
        {
                Debug.Log("hit object:" + hit.collider.gameObject.name + "xyz:" + hit.point);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.localScale = scale;
                sphere.transform.position = hit.point;
                sphere.GetComponent<Collider>().enabled = false;
                sphere.transform.SetParent(hit.collider.gameObject.transform); 
        }
    }
}
