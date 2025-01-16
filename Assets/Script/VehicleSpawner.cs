using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour  // Array untuk menyimpan referensi prefab kendaraan
{

    [SerializeField]  // Arah gerakan kendaraan (positif atau negatif)
    private GameObject[] vehicleReference;

    [SerializeField] // Array untuk menyimpan kecepatan masing-masing jenis kendaraan
    private int direction;

    [SerializeField] // Referensi untuk kendaraan yang baru saja di-spawn
    private int[] speed = {7,5,6};
    private GameObject spawnedVehicle;

    private int randomIndex; // Indeks acak untuk memilih kendaraan dan kecepatannya

    

    // Fungsi Start dipanggil sekali saat game dimulai
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
            spawnedVehicle.transform.position = transform.position;
            // Vector3 scale = spawnedVehicle.transform.localScale;
            // scale.x *= direction;
            // spawnedVehicle.transform.localScale = scale;
            
            // Tetapkan kecepatan gerakan kendaraan berdasarkan arah dan kecepatan yang sudah ditentukan
            spawnedVehicle.GetComponent<Vehicle>().speed = - direction * speed[randomIndex];
        } // while loop
    }
    

} //class
