using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    
    private string VEHICLE_TAG = "Vehicle";
    

    private void OnTriggerEnter2D(Collider2D collission){
        if (collission.CompareTag(VEHICLE_TAG)){
            Destroy(collission.gameObject);
            Debug.Log("collector status: "+GameManager.instance.CharStatus);

        }
    }
}
