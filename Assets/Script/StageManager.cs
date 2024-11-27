using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private int current_stage = 1;

    // private bool mustDropAll;

    [SerializeField]
    private List<Sprite> item_list;

    private List<Sprite> current_objective;

    [SerializeField]
    private List<GameObject> store_list;

    public static StageManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Stage generateStage(){
        int stageId = current_stage;
        if (stageId > 0 && stageId <= 8){
            List<Sprite> obj = item_list.GetRange(0,stageId);
            List<GameObject> stores = store_list.GetRange(0,stageId);
            return new Stage(obj,stores);;
        }else{
            return generateRandomStage(stageId);
        }
    }

    private Stage generateRandomStage(int dificulty){
        return new Stage();
    }

    public void nextStage(){
        current_stage++;
    }

    public void setStage(int stage){
        current_stage = stage;
    }
}

public struct Stage{
    public List<Sprite> objectives;
    public List<GameObject> stores;

    public Stage(List<Sprite> objectives,List<GameObject> stores)
    {
        this.objectives = objectives;
        this.stores = stores;
    }

}