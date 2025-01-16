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
            isPlayerHere = true; //checking if the player is in the drop zone
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger zone.");
            GameManager.instance.hideDropZoneUI();
            isPlayerHere = false; //checking if the players isn't in the drop zone
        }
    }

    private void handlePlayerOnArea(){
        if (isPlayerHere){
            GameManager.instance.checkTarget(); //so the item that the player holds can be dropped
        }
    }
}
