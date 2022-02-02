using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject food;
    public GameObject creature;
    [SerializeField, Range(5,600)]
    public float daytime;
    public float daytimeLeft;
    [SerializeField, Range(0,100)]
    public int foodCount = 10;
    [SerializeField, Range(0,100)]
    public int creatureCount = 10;
    

    void Start()
    {
        Spawn(foodCount, food, false);
        Spawn(creatureCount,creature, true);
        daytimeLeft=daytime;
        creature.GetComponent<CreatureController>().daytime=daytime;
    }

    void Update(){
        daytimeLeft-=Time.deltaTime;
        if(daytimeLeft <= 0){
            Spawn(foodCount, food, false);
            daytimeLeft=daytime;

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
}