using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void SelectCharacter(int characterIndex)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.CharIndex = characterIndex; // Set selected character
            
        }
        SceneManager.LoadScene("Gameplay"); // Load the gameplay scene
    }
}