using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Sprite item_sprite;
    private bool isPlayerHere;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerOnArea();
    }

    public Sprite getSprite(){
        return item_sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone.");
            GameManager.instance.showStoreUI(item_sprite);
            isPlayerHere = true;
            // Add additional logic for when the Player enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger zone.");
            GameManager.instance.hideStoreUi();
            // Add additional logic for when the Player exits the trigger
            isPlayerHere = false;
        }
    }

    private void handlePlayerOnArea(){
        if (isPlayerHere){
            if (Input.GetKey(KeyCode.E)){
                GameManager.instance.addItemToInventory(item_sprite);
                Debug.Log("Added "+item_sprite.name+" to the players inventory");
            }
        }
    }
}
