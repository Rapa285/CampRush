using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 10f;
    private float movementX,movementY;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private string VEHICLE_TAG = "Vehicle";
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-3,-4.5f,0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement() {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Calculate the new position based on input
        Vector2 newPosition = rb.position + new Vector2(moveX, moveY) * moveSpeed * Time.deltaTime;

        // Move the Rigidbody2D (this respects collisions)
        rb.MovePosition(newPosition);
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
