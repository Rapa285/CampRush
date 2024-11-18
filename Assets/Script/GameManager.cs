using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField]
    private GameObject[] characters;

    [SerializeField]
    private Text scoreBoard ;

    [SerializeField]
    private int score;

    private GameObject GameplayUI;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P)) 
        // {
        //     if (Time.timeScale == 0f)
        //         resumeGame();
        //     else
        //         pauseGame();
        // }
    }

    private int _charIndex;
    public int CharIndex{
        get{return _charIndex;}
        set { _charIndex = value;}
    }
    private bool isCharacterDead;

    public bool CharStatus{
        get{return isCharacterDead;}
        set { isCharacterDead = value;}
    }

    private void Awake(){
        if (instance == null){
            instance = this;
            // GameplayUI = GameObject.Find("Game Over");
            // GameplayUI.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable(){
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
        if(scene.name == "Gameplay"){
            // scoreBoard = GameObject.Find("scoreBoard").GetComponent<Text>();
            Instantiate(characters[CharIndex]);
            CharStatus = false;
            score = 0;
            resumeGame();
            GameplayUI = GameObject.Find("Game Over");
            GameplayUI.SetActive(false);
            Debug.Log("instantiate status: "+GameManager.instance.CharStatus);
        }
    }

    public void incrScore(){
        score++;
        scoreBoard.text = score.ToString();
    }

    public int getScore(){
        return score;
    }

    public void pauseGame(){
        Time.timeScale = 0f;
    }

    public void resumeGame(){
        Time.timeScale = 1f;
    }

    public void GameOver(){
        pauseGame();
        GameplayUI.SetActive(true);
    }

}
