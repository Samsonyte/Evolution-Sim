using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject food;
    public GameObject creature;
    [SerializeField, Range(0,100)]
    int foodCount = 10;
    [SerializeField, Range(0,100)]
    int creatureCount = 10;

    void Start()
    {
        Spawn(foodCount, food);
        Spawn(creatureCount, creature);
    }
    void Spawn(count, object)
    {
        for(int i=0; i<count; i++){
            float x= Random.Range(8,92);
            float z= Random.Range(8,92);
            Instantiate(object, new Vector3(x,1,z), Quaternion.identity);
        }
    }
    
}
