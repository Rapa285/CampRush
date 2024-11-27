using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 0.5f;
    private float movementX,movementY;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector3 movement;
    private string VEHICLE_TAG = "Vehicle";
    private Transform _transform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-13,-13.5f,0);
    }

    void Update(){
        movement = Movement();
        transform.position += movement * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key detected globally.");
        }
    }

    Vector2 Movement(){
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Calculate the new position based on input
        // Vector2 newPosition = rb.position + new Vector2(moveX, moveY) * moveSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(moveX, moveY, 0);
        return movement;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag(VEHICLE_TAG)){
            GameManager.instance.CharStatus = true;
            Debug.Log("ontrigger status: "+GameManager.instance.CharStatus);
            Destroy(gameObject);
            GameManager.instance.GameOver();
        }
    }
}
