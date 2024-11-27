using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    public float speed;
    // private string CAMERA_TAG = "MainCamera";
    private Rigidbody2D myBody;


    [SerializeField]
    public RoadGraph road = new RoadGraph();

    public List<Node> waypoints;
    public float rotationSpeed = 5f;
    private Node currentWaypoint;

    void Update()
    {
        if (waypoints.Count == 0) return; // If there are no waypoints, do nothing

        // Move the object towards the current waypoint
        Vector3 targetWaypoint = currentWaypoint.getPos();
        // Debug.Log("Target: " +targetWaypoint);

        Vector3 direction = targetWaypoint - transform.position;

        // Rotate or flip based on direction
        if (direction.x != 0) // Moving horizontally
        {
            // Flip the object based on horizontal movement
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        if (direction.y != 0) // Moving vertically
        {
            if (direction.y > 0) // Moving up
            {
                transform.eulerAngles = new Vector3(0, 0, -90); // Rotate 90 degrees counter-clockwise
            }
            else // Moving down
            {
                transform.eulerAngles = new Vector3(0, 0, 90); // Rotate 90 degrees clockwise
            }
        }

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

        // Check if the object reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint) == 0)
        {
            // Move to the next waypoint (looping back to the first one when finished)
            currentWaypoint = currentWaypoint.getRandomDest();
            // Debug.Log("Next Destination: " +currentWaypoint);

        }
    }

    void Start()
    {
        road.initCheckpoints();
        waypoints = road.getNodes();
        currentWaypoint = waypoints[0];
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
