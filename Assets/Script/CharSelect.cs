using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void SelectCharacter(int characterIndex)
    {

        Debug.Log("character id :"+characterIndex);
        GameManager.instance.CharIndex = characterIndex; // Set selected character
            
        // SceneManager.LoadScene("Gameplay"); // Load the gameplay scene
    }
}