using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    void Start()
    {
        transform.Rotate( new Vector3 (0,0,90));
    }
    void Update()
    { 
        transform.Rotate (new Vector3  (25,0,0)*Time.deltaTime);
    }
}
