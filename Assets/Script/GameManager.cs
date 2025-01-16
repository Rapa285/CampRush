using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

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
    private GameObject timerUI;

    private GameObject GameOverUI;
    private GameObject StoreUI;
    private GameObject DropZone;
    private GameObject ObjectiveUI;
    private GameObject StageDoneUI;
    private List<Sprite> inventory = new List<Sprite>();
    private List<Sprite> objective;

    [SerializeField]
    private Text TimerText; 
    [SerializeField]
    private int StartSeconds;   
    private float Countdown;
    private bool TimerStatus = false;
    // public StageManager.instance StageManager.instance;

    // private SpriteListComparer src = new SpriteListComparer();

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
    } // Pause the game

    private int _charIndex;
    public int CharIndex{
        get{return _charIndex;}
        set { _charIndex = value;}
    }
    private bool isCharacterDead;

    public bool CharStatus{
        get{return isCharacterDead;}
        set { isCharacterDead = value;}
    } //Check if the character is dead

    private void Awake(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate GameManager instances
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
            Instantiate(characters[CharIndex]);
            CharStatus = false;
            score = 0;
            resumeGame();
            innitUI();
            innitStage();
            inventory = new List<Sprite>();
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
        GameOverUI.SetActive(true);
    }

    public void StageDone(){
        pauseGame();
        showStageDoneUI();
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

    public void addItemToInventory(Sprite item){
        if (!inventory.Contains(item)){
            inventory.Add(item);
            addToInventoryUI(item);
        }
    }

    public void addToInventoryUI(Sprite item){
        GameObject inventory = GameObject.Find("Inv_Outline");
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            GameObject child = inventory.transform.GetChild(i).gameObject;
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (sr.sprite != null)
                {
                    Debug.Log(child.name + " has a sprite: " + sr.sprite.name);
                }else{
                    sr.sprite = item;
                    return;
                }
            }
        }
    }

    public void addObjectiveToUI()
    {
        if (objective == null || objective.Count == 0)
        {
            Debug.LogWarning("Objective list is empty or null.");
            return;
        }

        GameObject inventory = GameObject.Find("Obj_Outline");
        if (inventory == null)
        {
            Debug.LogError("Obj_Outline GameObject not found.");
            return;
        }

        int objectiveIndex = 0;

        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            if (objectiveIndex >= objective.Count) return;

            GameObject child = inventory.transform.GetChild(i).gameObject;
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                if (sr.sprite != null)
                {
                    Debug.Log(child.name + " already has a sprite: " + sr.sprite.name);
                }
                else
                {
                    sr.sprite = objective[objectiveIndex];
                    objectiveIndex++;
                }
            }
        }
    }


    public void showDropZoneUI(){

        DropZone.SetActive(true);
    }

    public void hideDropZoneUI(){

        DropZone.SetActive(false);

    }

    
    public void showStageDoneUI(){

        StageDoneUI.SetActive(true);
    }

    public void hideStageDoneUI(){

        StageDoneUI.SetActive(false);

    }

    public void checkTarget(){
        Debug.Log("Checking Target");
        Debug.Log("obj:"+objective);
        SpriteListPrinter.PrintSpriteList(objective);
        Debug.Log("inv:"+inventory);

        SpriteListPrinter.PrintSpriteList(inventory);
        
        Debug.Log(SpriteListComparer.AreListsEqual(objective,inventory));
        if (SpriteListComparer.AreListsEqual(objective,inventory)){
            StageDone();
            incrScore();
            Debug.Log("Stage Over");
        }
    }

    private void innitUI(){
        scoreBoard = GameObject.Find("ScoreText").GetComponent<Text>();
        GameOverUI = GameObject.Find("Game Over");
        GameOverUI.SetActive(false);
        StoreUI = GameObject.Find("Store");
        StoreUI.SetActive(false);
        DropZone = GameObject.Find("DropZone");
        DropZone.SetActive(false);
        ObjectiveUI = GameObject.Find("Objective");
        StageDoneUI = GameObject.Find("Stage Done");
        timerUI = GameObject.Find("TimerText");
        TimerText = timerUI.GetComponent<Text>();
        StageDoneUI.SetActive(false);
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

    public void innitStage(){
        // StageManager.instance.nextStage();
        Stage stage = StageManager.instance.generateStage();
        Debug.Log(stage);
        objective = stage.objectives;
        addObjectiveToUI();
    }

    private bool AreListsEqual<T>(List<T> list1, List<T> list2)
    {
        // Check if counts match
        if (list1.Count != list2.Count)
            return false;

        // Compare sorted lists
        return list1.OrderBy(item => item).SequenceEqual(list2.OrderBy(item => item));
    }
}
