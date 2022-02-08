using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject food;
    public GameObject creature;
    public Light sun;
    [SerializeField, Range(5,600)]
    public float daytime;
    public float daytimeLeft;
    [SerializeField, Range(0,100)]
    public int foodCount = 10;
    [SerializeField, Range(0,100)]
    public int creatureCount = 10;
    
    public List<GameObject> creatures = new List<GameObject>();
    public List<GameObject> foods = new List<GameObject>();

    void Start()
    {
        creature.GetComponent<CreatureController>().daytime=daytime;
        Spawn(foodCount, food, false, foods);
        Spawn(creatureCount,creature, true, creatures);
        daytimeLeft=daytime;
        for(int j=0; j < creatures.Count; j++){
            creatures[j].GetComponent<CreatureController>().trackingNumber=j;
        }
    }

    void Update(){
        daytimeLeft-=Time.deltaTime;
        if(daytimeLeft <= 5){
            StartCoroutine(dayEnd(daytimeLeft));
        }
    }

    void Spawn(int obcount, GameObject obid, bool edge, List<GameObject> addto){
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
            addto.Add(Instantiate(obid, new Vector3(x,1,z), Quaternion.identity));
        }
    }
    IEnumerator dayEnd(float t){
        t-=Time.deltaTime;
            if(t>0){
                sun.intensity = t/5;
                Debug.Log("t is "+t);
                yield return null;
            }
            if(t <=-1 ){
                Debug.Log("Success, t is " + t);
                newDayS();
            }
            
        }
    
    void newDayS(){
        sun.intensity=1;
        StopCoroutine(dayEnd(0));
        Spawn(foodCount, food, false, foods);
        daytimeLeft=daytime;
        for(int i=0;i<creatures.Count;i++){
            creatures[i].GetComponent<CreatureController>().newDayC();
        }
        
    }
}