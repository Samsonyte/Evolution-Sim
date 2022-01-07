using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;
    [SerializeField, Range(0,100)]
    int foodCount;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<foodCount; i++){
          //  float x= new Random(10,110);
          //  float z= new Random(10,110);
           // Instantiate(food, new Vector3(x,1,z));
        }
    }

    
}
