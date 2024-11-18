using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 10f;
    private float movementX,movementY;


    private SpriteRenderer sr;
    private string VEHICLE_TAG = "Vehicle";
    private void Awake(){

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
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxis("Vertical");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveSpeed;
        transform.position += new Vector3(0f, movementY, 0f) * Time.deltaTime * moveSpeed;
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
