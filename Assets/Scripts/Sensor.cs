using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public int trackingNumber;
    public GameObject[] creatArray;
    public float creatX;
    public float creatZ;

    void Update()
    {
        if(creatArray[trackingNumber] != null){
            creatX = creatArray[trackingNumber].transform.position.x;
            creatZ = creatArray[trackingNumber].transform.position.z;
            this.transform.position = new Vector3(creatX, 0, creatZ);
        } else{
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Food")){
            other.gameObject.tag = "Taken Food";
            creatArray[trackingNumber].GetComponent<CreatureController>().state="foundFood";
            float x = other.gameObject.transform.position.x;
            float z = other.gameObject.transform.position.z;
            creatArray[trackingNumber].GetComponent<CreatureController>().foodDest(x,z);
        }
    }
    public void sensorDie(){
        Destroy(gameObject);
    }
 }

