using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    private bool isPlayerHere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerOnArea();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone.");
            GameManager.instance.showDropZoneUI();
            isPlayerHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger zone.");
            GameManager.instance.hideDropZoneUI();
            isPlayerHere = false;
        }
    }

    private void handlePlayerOnArea(){
        if (isPlayerHere && Input.GetKey(KeyCode.Space)){
            GameManager.instance.checkTarget();
        }
    }
}
