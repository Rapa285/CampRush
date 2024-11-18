using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    public float speed;
    private string CAMERA_TAG = "MainCamera";
    private Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }

    void OnTriggerExit2D(Collider2D other){
        Debug.Log("entering camera");
        if(other.gameObject.CompareTag(CAMERA_TAG)){
            Destroy(gameObject);
        }
            
    }
}
