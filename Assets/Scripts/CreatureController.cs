using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreatureController : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject self;
    public GameObject sensor;
    public int eaten;
    public string state="searching";
    void Start()
    {
      // Instantiate(sensor, new Vector3(self.Transform.x,1,self.Transform.z), Quaternion.identity); 
    }

    // Update is called once per frame
    void Update()
    {
        if(state=="searching"){

        }
    }
}
