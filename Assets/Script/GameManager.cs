using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;


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
    private GameObject StoreUI;

    [SerializeField]
    private Text TimerText; 
    [SerializeField]
    private int StartSeconds;   
    private float Countdown;
    private bool TimerStatus = false;

    private void Start()
    {
        StartTimer();
    }
    void Update()
    {
        Timer();

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
            StoreUI = GameObject.Find("Store");
            StoreUI.SetActive(false);
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

    public void showStoreUI(Sprite item_sprite){
        StoreUI.SetActive(true);
        GameObject item = GameObject.Find("ItemSprite").gameObject;
        SpriteRenderer item_sr = item.GetComponent<SpriteRenderer>();
        item_sr.sprite = item_sprite;
        Debug.Log("item sprite have been changed with:"+ item_sprite.name);
    }

    public void hideStoreUi(){
        StoreUI.SetActive(false);
    }

    public void Timer()
    {
        if (TimerStatus == true && Countdown > 0)
        {
            Countdown -= Time.deltaTime;
            UpdateTimerDisplay();

        }
        else if (Countdown <= 0 && TimerStatus)
        {
            TimerStatus = false;
            Countdown = 0;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (TimerText != null)
        {
            TimerText.text = Mathf.CeilToInt(Countdown) + " Seconds Left";
        }
    }

    public void StartTimer()
    {
        TimerStatus = true;
        Countdown = StartSeconds;
    }
}
