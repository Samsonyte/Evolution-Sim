using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreatureController : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject sensor;
    public int eaten;
    public string state="searching";
    public Vector3 randDest;
    public int energy;
    void Start()
    {
      Transform loc=this.transform;
      Instantiate(sensor, new Vector3(loc.position.x, loc.position.y, loc.position.z), Quaternion.identity);
      agent=this.GetComponent<NavMeshAgent>();
      randDest = newDest();
      energy=10000;
    }

    void Update()
    {
      energy--;
      if(energy<=0){
        agent.SetDestination(new Vector3(this.transform.position.x,1,this.transform.position.z));
      }
        if(state=="searching"){
          if(this.transform.position.x-randDest.x <.5 & this.transform.position.z-randDest.z<.5){
            randDest=newDest();
          }

        }
    }
      Vector3 newDest(){
      float x= Random.Range(-41,41);
      float z= Random.Range(-41,41);
      Vector3 newRandDest= new Vector3(x,1,z);
      agent.SetDestination(newRandDest);
      return (newRandDest);
    }
}
