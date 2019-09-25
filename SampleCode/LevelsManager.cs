using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsManager : MonoBehaviour {
    //Level Buttons Contain 4 Important GameObjects
    //Their Names Should Be "Star1" , "Star2" , "Star3" ,"Lock"
    //In this Script,They're Called with These Names;Be Careful!


        //Game Data Manager Script
    public GameDataManager DataManager;

    //Reference All LevelContainers in This Variable
    public GameObject[] LevelContainers;
    //Levels Count Will Be Calculated According to Levels Included in Level Containers
    public int LevelsCount;
    //Struct Object That Level Buttons Objects Store in it
    [System.Serializable]
    public struct Level
    {
        public UIButton Button;
        public GameObject Star1, Star2, Star3;
        public GameObject LockSprite;
    }

    //Dynamic Array of Level Objects
    public List<Level> Levels;

    public UILabel StarLabel;
    public UILabel CreditLabel,ShopCreditLabel;

    MusicController musicController;
    
    //Async Operation is used to Load The Level Scene In the Background
    AsyncOperation async;
    
    //Objects Of Menu That Should Disappear While Loading Animation
    public GameObject[] MenuObjects;
    //Duration Values of Loading Transition
    public float LoadingFadeTime, LoadingFadeTime1,LoadingFadeTime2,LoadingFadeTime3;

    // Use this for initialization
    void Awake()
    {

        GamePreferences.IsProcessing = true;

        //First of All,Get Level Buttons Info,Because Levels Count Will Be Used To initialize the array in "GameDataManger" Script.So it should Be Called in Awake Function
        GetMenuLevelsInfo();



        foreach (var item in MenuObjects)
        {
            TweenAlpha.Begin(item, 0, 0f);
        }
    }

    void Start()
    {
        //Get GameDataManager Controller Main Instance
        DataManager = GameDataManager.ManangerInstance;

        //Get Music Controller Main Instance
        musicController = MusicController.ControllerInstance;

        //Set the EventDelegate for All of The Level Buttons To Load the Right Level
        //Level Scene Names Should be Like These => "Level0","Level1",.....,"Level47"
        for (int i = 0; i < Levels.Count; i++)
        {
            EventDelegate del = new EventDelegate(this, "StartLoading");
            del.parameters[0].value = "Level" + (i);
            Levels[i].Button.onClick.Add(del);
        }
        StartCoroutine(StartingTransition());
        
    }

    // Update is called once per frame
    void Update()
    {
        //Here First We Check if GameDataManager Has Finished Initialization,And Then Adjust Stars And Lock Sprites on Level Buttons
        if (GamePreferences.InitializationCompleted)
        {
            StarLabel.text = DataManager.Coins.ToString();
            UpdateCreditVisual();
            AdjustLevelStarsVisual();
            LevelLockUnlock();
            StarLabel.text = DataManager.Coins.ToString();
            GamePreferences.InitializationCompleted = false;

        }


    }

    public  void UpdateCreditVisual()
    {
        CreditLabel.text = DataManager.Credit.ToString();
        ShopCreditLabel.text = DataManager.Credit.ToString();
    }

    public void GetMenuLevelsInfo()
    {

        //Initualize The LevelCount Value
        LevelsCount = 0;

        //For Each Level Container
        for (int i = 0; i < LevelContainers.Length; i++)
        {
            //Extract All of The Levels In The LevelContainer
            for (int j = 0; j < LevelContainers[i].transform.childCount; j++)
            {
                LevelsCount++;
                Transform temp = LevelContainers[i].transform.GetChild(j);
                Level l = new Level();
                l.Button = temp.GetComponent<UIButton>();
                l.Star1 = temp.Find("Star1").gameObject;
                l.Star2 = temp.Find("Star2").gameObject;
                l.Star3 = temp.Find("Star3").gameObject;
                //LockSprite
                Levels.Add(l);

            }
        }
    }

    public void AdjustLevelStarsVisual()
    {
        //First,Get StarsCount That Stores in GameDataManager
        var Stars = DataManager.LevelsStars;
        for (int i = 0; i < LevelsCount; i++)
        {
            switch (Stars[i])
            {
                //Level Has Not been Unlocked Yet
                case 9:
                    Levels[i].Star1.SetActive(false);
                    Levels[i].Star2.SetActive(false);
                    Levels[i].Star3.SetActive(false);
                    break;
                //0 Stars
                case 0:
                    Levels[i].Star1.SetActive(false);
                    Levels[i].Star2.SetActive(false);
                    Levels[i].Star3.SetActive(false);
                    break;
                //1 Star
                case 1:
                    Levels[i].Star1.SetActive(true);
                    Levels[i].Star2.SetActive(false);
                    Levels[i].Star3.SetActive(false);
                    break;
                //2 Stars
                case 2:
                    Levels[i].Star1.SetActive(true);
                    Levels[i].Star2.SetActive(true);
                    Levels[i].Star3.SetActive(false);
                    break;
                //3 Stars
                case 3:
                    Levels[i].Star1.SetActive(true);
                    Levels[i].Star2.SetActive(true);
                    Levels[i].Star3.SetActive(true);
                    break;

                default:
                    break;
            }
        }

    }

    public void LevelLockUnlock()
    {
        
        //Unlock The Levels To CurrentLevel
        for (int i = 0; i < DataManager.CurrentLevel + 1; i++)
        {
            Levels[i].LockSprite.SetActive(false);
        }
        
    }



    public void StartLoading(string levelName)
    {
        if (!GamePreferences.IsProcessing)
        {
            musicController.PlayTempMusic(5);
            //Start the Loading Animation
            StartCoroutine(Loading(levelName));
        }

    }

    //Load The Level
    IEnumerator load(string levelName)
    {
        async = Application.LoadLevelAsync(levelName);
        //Don't Let The Scene Be Shown Until the Right Time
        async.allowSceneActivation = false;
        yield return new WaitForEndOfFrame();
    }

    public void ActivateScene()
    {
        StopCoroutine("load");
        //Show The Scene
        async.allowSceneActivation = true;
    }


    IEnumerator Loading(string levelName)
    {
        GamePreferences.IsProcessing = true;
        yield return new WaitForSeconds(0.2f);
        foreach (var item in MenuObjects)
        {
            TweenAlpha.Begin(item, LoadingFadeTime, 0f);
        }
        //Wait For transition Finish
        yield return new WaitForSeconds(LoadingFadeTime);
        //Coroutine That Loads the Level In Background
        StartCoroutine("load", levelName);
        yield return new WaitForSeconds(1f);

        while (async.progress < 0.8f)
        {
            yield return new WaitForEndOfFrame();
        }

        if (async.progress >= 0.8f)  //it Means that If the level is Loaded in Background
        {
            ActivateScene();
        }
        
    }



    IEnumerator StartingTransition()
    {
        
        foreach (var item in MenuObjects)
        {
            TweenAlpha.Begin(item, 0, 0f);
            TweenAlpha.Begin(item, LoadingFadeTime, 1f);
        }
        //Wait For transition Finish
        yield return new WaitForSeconds(LoadingFadeTime);
        yield return new WaitForSeconds(1f);
    }
}
