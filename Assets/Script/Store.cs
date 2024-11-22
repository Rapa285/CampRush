using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Sprite item_sprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }


}
