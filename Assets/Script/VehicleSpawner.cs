using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] vehicleReference;

    [SerializeField]
    private int direction;

    private int[] speed = {10,5,7};
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
            yield return new WaitForSeconds(Random.Range(1,3));

            randomIndex = Random.Range(0, vehicleReference.Length);

            spawnedVehicle = Instantiate(vehicleReference[randomIndex]);
            spawnedVehicle.transform.position = transform.position;
            Vector3 scale = spawnedVehicle.transform.localScale;
            scale.x *= direction;
            spawnedVehicle.transform.localScale = scale;

            spawnedVehicle.GetComponent<Vehicle>().speed = - direction * speed[randomIndex];
        } // while loop
    }
    

} //class
