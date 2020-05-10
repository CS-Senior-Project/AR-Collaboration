using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readkeyboard : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject vrkeys;
    private void Awake()
    {
        Instantiate(vrkeys);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
