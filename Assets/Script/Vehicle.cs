using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    public float speed;
    // private string CAMERA_TAG = "MainCamera";
    private Rigidbody2D myBody;


    [SerializeField]
    public List<GameObject> waypoints;

    public float rotationSpeed = 5f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Count == 0) return; // If there are no waypoints, do nothing

        // Move the object towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex].transform;
        Debug.Log("Target: " +waypoints[currentWaypointIndex]);
        // Vector3 direction = targetWaypoint.position - transform.position;
        
        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Check if the object reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            // Move to the next waypoint (looping back to the first one when finished)
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            Debug.Log("Next index: " +currentWaypointIndex);

        }
    }

    void Start()
    {
        GameObject checkpoint = GameObject.Find("Checkpoint");
        if (checkpoint != null)
        {
            // Iterate through all children of the "checkpoint" GameObject
            foreach (Transform child in checkpoint.transform)
            {
                // Add each child GameObject to the list
                waypoints.Add(child.gameObject);
            }

            // Optionally, print out the names of all children
            foreach (GameObject child in waypoints)
            {
                Debug.Log("Checkpoint Child: " + child.name);
            }
        }
        else
        {
            Debug.LogError("Checkpoint GameObject not found!");
        }
        myBody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        // myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }

    // void OnTriggerExit2D(Collider2D other){
    //     Debug.Log("entering camera");
    //     if(other.gameObject.CompareTag(CAMERA_TAG)){
    //         Destroy(gameObject);
    //     }
            
    // }
}
