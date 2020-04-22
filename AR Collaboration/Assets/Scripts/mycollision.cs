using System.Collections.Generic;
using UnityEngine;

public class mycollision : MonoBehaviour
{
    Vector3 scale=new Vector3(0.2f, 0.2f, 0.2f);
    void OnTriggerEnter(Collider collision)
    {

            Debug.Log(collision.name + collision.transform.position);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cube.transform.localScale = scale;
        cube.transform.position = collision.transform.position;
        cube.GetComponent<Collider>().enabled=false;
        cube.transform.SetParent(transform);
        
    }

    private void Update()
    {
      
    }
}
