using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject food;
    public GameObject creature;
    public GameObject sensor;
    public Light sun;
    [SerializeField, Range(5,600)]
    public float daytime;
    public float daytimeLeft;
    [SerializeField, Range(0,100)]
    public int foodCount = 10;
    [SerializeField, Range(0,100)]
    public int creatureCount = 10;
    
    public GameObject[] creatures;
    public GameObject[] foods;
    public GameObject[] sensors;

    void Start()
    {
        creature.GetComponent<CreatureController>().daytime=daytime;
        Spawn(foodCount, food, false);
        Spawn(creatureCount,creature, true);
        newArrays();
        daytimeLeft=daytime;
    }

    void Update(){
        daytimeLeft-=Time.deltaTime;
        if(daytimeLeft <= 5){
            StartCoroutine(dayEnd(daytimeLeft));
        }
    }

    void Spawn(int obcount, GameObject obid, bool edge){
         for(int i=0; i<obcount; i++){
                float x= Random.Range(-42,42);
                float z= Random.Range(-42,42);
                if(edge){
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
                }
            Instantiate(obid, new Vector3(x,1,z), Quaternion.identity);
        }
    }
    IEnumerator dayEnd(float t){
        t-=Time.deltaTime;
            if(t>0){
                sun.intensity = t/5;
              //  Debug.Log("t is "+t);
                yield return null;
            }
            if(t <=-1 ){
                newArrays();
                newDayS();
            }
        }
    
    void newDayS(){
        sun.intensity=1;
        StopCoroutine(dayEnd(0));
        Spawn(foodCount, food, false);
        daytimeLeft=daytime;
        for(int i=0;i<creatures.Length;i++){
            creatures[i].GetComponent<CreatureController>().newDayC();
        }
        
    }
    void newArrays(){
        creatures = GameObject.FindGameObjectsWithTag("Creature");
        foods = GameObject.FindGameObjectsWithTag("Food"); 
        for(int j=0; j < creatures.Length; j++){
            creatures[j].GetComponent<CreatureController>().trackingNumber=j;
        }
    }
}