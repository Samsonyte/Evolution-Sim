using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureController : MonoBehaviour
{
  NavMeshAgent agent;
  public GameObject sensor;
  public GameObject field;
  public int trackingNumber;
  public string state;
  public Vector3 randDest;
  public int foodEaten=0;
  public float speed; 
  
  [SerializeField, Range(0,100000)]
  public float startEnergy;
  public float energy;
  public float daytime;
  public float daytimeLeft;
  public float currentX;
  public float currentZ;

  void Start()
  {
    daytimeLeft=daytime;
    Transform loc=this.transform;
    Instantiate(sensor, new Vector3(loc.position.x, loc.position.y, loc.position.z), Quaternion.identity);
    agent=this.GetComponent<NavMeshAgent>();
    speed = GetComponent<NavMeshAgent>().speed;
    energy=startEnergy;
    state="searching";
    randDest = newDest();
  }

  void Update(){
    currentX=this.transform.position.x;
    currentZ=this.transform.position.z;
    daytimeLeft-=Time.deltaTime;
    if(energy<=0){
      Die();
    }
    if(state=="searching"){
      if(currentX-randDest.x <.5 & currentZ-randDest.z<.5){
        randDest=newDest();
      }
      float dx = 46 - Mathf.Abs(currentX);
      float dz = 46 - Mathf.Abs(currentZ);
      if(dx/speed > daytimeLeft-1 & foodEaten==1 || dz/speed > daytimeLeft -1 & foodEaten==1){
        goHome(currentX, currentZ);
      }
    }
    if(Mathf.Abs(currentX)>42 || Mathf.Abs(currentZ)>42){
      if(state=="goingHome"){
        state="home";

      }
    }
    if (state != "home"){
      energy--;
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
        goHome(currentX, currentZ);
      }
    }
  }

  void goHome(float x, float z){
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
      state="goingHome";
    }

  void Die(){
      agent.SetDestination(new Vector3(currentX,1,currentZ));
      Destroy(gameObject, 1);
  }

  public void newDayC(){
    Debug.Log("newDayC");
    energy=startEnergy;
    if(state != "home"){
      //Die();
      Destroy(gameObject);
      field.GetComponent<Spawner>().creatureCount--;
    }else if(foodEaten >= 2){
       Instantiate(this, new Vector3(currentX,1,currentZ), Quaternion.identity);
       field.GetComponent<Spawner>().creatureCount++;
       foodEaten=0;
       state="searching";
       randDest=newDest();
    }else if(foodEaten == 1){
      foodEaten=0;
      state="searching";
      randDest=newDest();
    }
    
    daytimeLeft=daytime;
  }
}