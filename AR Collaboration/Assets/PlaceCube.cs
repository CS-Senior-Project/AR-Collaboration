using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCube : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenpos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 worldpos;

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(1, 1, 1);

            Rigidbody cubeBody = cube.GetComponent<Rigidbody>();
            cubeBody.useGravity = true;

            /*            if (ZEDSupportFunctions.GetWorldPositionAtPixel(screenpos, ZEDManager.Instance.GetLeftCameraTransform().GetComponent<Camera>(), out worldpos))
                        {
                            GetComponent<Rigidbody>().velocity = Vector3.zero;
                            transform.position = worldpos + Vector3.up;
                        }*/
        }
    }
}