using System.Collections.Generic;
using UnityEngine;

public class mycollision : MonoBehaviour
{
    //hardcode scale value
    Vector3 scale=new Vector3(0.2f, 0.2f, 0.2f);

    //build in call back function when an isTrigger object get hitted
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name + collision.transform.position);
        //create a sphere object
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cube.transform.localScale = scale;

        //pull the collider position
        cube.transform.position = collision.transform.position;
        cube.GetComponent<Collider>().enabled=false;
        cube.transform.SetParent(transform);
    }


}
