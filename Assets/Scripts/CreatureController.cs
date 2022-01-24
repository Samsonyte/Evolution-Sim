using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreatureController : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject sensor;
    public string state="searching";
    public Vector3 randDest;
    public int foodEaten=0;
    public float speed; 
    [SerializeField, Range(5,600)]
    public float daytime;
    [SerializeField, Range(0,100000)]
    public int energy;

    void Start()
    {
      Transform loc=this.transform;
      Instantiate(sensor, new Vector3(loc.position.x, loc.position.y, loc.position.z), Quaternion.identity);
      agent=this.GetComponent<NavMeshAgent>();
      speed = GetComponent<NavMeshAgent>().speed;
      randDest = newDest();
    }

    void Update(){
      energy--;
      daytime-=Time.deltaTime;
      if(energy<=0){
       Die();
      }
        if(state=="searching"){
          if(this.transform.position.x-randDest.x <.5 & this.transform.position.z-randDest.z<.5){
            randDest=newDest();
            //float d = 46- Mathf.Sqrt()
          }
        }
        if (state != "home"){
          if(daytime<=0){
            Die();
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

    void OnTriggerEnter(Collider other){
      if(other.gameObject.CompareTag("Food")){
        other.gameObject.SetActive(false);
        energy += 1000;
        foodEaten++;
        if(foodEaten>= 2){
          state="goHome";
          goHome(this.transform.position.x, this.transform.position.z);
        }
      }
    }

void goHome(x,z){
  if(Mathf.Abs(x)>Mathf.Abs(z)){
                if(x<0){
                    x=-46;
                }else{
                    x=46;
                }
            }else{
                if(z<0){
                    z=-46;
                }else{
                    z=46;
                } 
            }
      Vector3 newHomeDest= new Vector3(x,1,z);
      agent.SetDestination(newHomeDest);
      
    }

  //boolean shouldGoHome(x, z);
    void Die(){
       agent.SetDestination(new Vector3(this.transform.position.x,1,this.transform.position.z));
       Destroy(gameObject, 4);
    }

}
