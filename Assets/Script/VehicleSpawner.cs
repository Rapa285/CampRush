using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] vehicleReference;

    [SerializeField]
    private Vector3 spawnerPos = new Vector3(10, 3, 0);

    private GameObject spawnedVehicle;

    private int randomIndex;

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVehicles());
    }

    
    IEnumerator SpawnVehicles(){
        while (!GameManager.instance.CharStatus){
            // Debug.Log("Player alive? :" + GameManager.instance.CharStatus);
            yield return new WaitForSeconds(Random.Range(1,5));

            randomIndex = Random.Range(0, vehicleReference.Length);

            spawnedVehicle = Instantiate(vehicleReference[randomIndex]);
            spawnedVehicle.transform.position = spawnerPos;
            spawnedVehicle.GetComponent<Vehicle>().speed = - Random.Range(4,10);
        } // while loop
    }
    

} //class
