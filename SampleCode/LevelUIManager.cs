using UnityEngine;
using System.Collections;
using System;

public class LevelUIManager : MonoBehaviour {


    public BombScript bombScript;
    public LanternScript lanternScript;
    public PlayerSelectionSwitch playerSwitcher;

    public GameObject LanternButton;
    public GameObject BombButton;
    public GameObject LightButton;

    public int AmbCost = 4, LightCost = 10,bombCost = 5;

    public UILabel StarLabel;
    public UIToggle MusicToggler;

    GameDataManager DataManager;

    public TweenAlpha CreditWarning;

    //Async Operation is used to Load The Level Scene In the Background
    AsyncOperation async;

    //Objects Of Menu That Should Disappear While Loading Animation
    public GameObject[] Overlays;
    public GameObject[] MenuObjects;
    //Duration Values of Loading Transition
    public float LoadingFadeTime;


    //Trigger Temp vars
    bool LanternCoroutineRunning = false;
    bool BombCoroutineRunning = false;
    bool LightActivated = false;

    public LevelManager levelManager;

    MusicController musicController;
    void Awake()
    {
        foreach (var item in MenuObjects)
        {
            TweenAlpha.Begin(item, 0, 0f);
        }
        foreach (var item in Overlays)
        {
            iTween.FadeTo(item,1, 0);
        }
        musicController = MusicController.ControllerInstance;
        DataManager = GameDataManager.ManangerInstance;
        MusicToggler.value = DataManager.MusicOnOff;

    }
    // Use this for initialization
    void Start () {
        StartCoroutine("StartingTransition");
        MusicToggler.value = DataManager.MusicOnOff;
        StarLabel.text = DataManager.Credit.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Return2MainMenu()
    {
        if(!GamePreferences.IsProcessing)
        {
            musicController.StopAllTempSounds();
            StartLoading("MainMenu");
            DataManager.SetFlags();
        }

    }


    public void StartLoading(string levelName)
    {
        //Coroutine That Loads the Level In Background

        //Start the Loading Animation
        GamePreferences.IsProcessing = true;
        StartCoroutine(Loading(levelName));
    }

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

            yield return new WaitForSeconds(0.2f);
            foreach (var item in MenuObjects)
            {
                TweenAlpha.Begin(item, LoadingFadeTime, 0f);
            }
            foreach (var item in Overlays)
            {
                iTween.FadeTo(item, 1, LoadingFadeTime);
            }
            //Wait For transition Finish
            yield return new WaitForSeconds(LoadingFadeTime);

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
        yield return new WaitForSeconds(LoadingFadeTime);
        foreach (var item in Overlays)
        {
            iTween.FadeTo(item, 0f, LoadingFadeTime);
        }
        //Wait For transition Finish
        yield return new WaitForSeconds(LoadingFadeTime);
        GamePreferences.IsProcessing = false;
    }


    public bool UpdateCredit(int Cost)
    {
        DataManager.Credit -= Cost;
        if (DataManager.Credit < 0)
        {
            DataManager.Credit += Cost;
            CreditWarning.PlayForward();
            return false;
        }            
        StarLabel.text = DataManager.Credit.ToString();
        DataManager.SetCredit();
        return true;

    }

    public void ActivateLantern()
    {
        if (!LanternCoroutineRunning && !GamePreferences.IsProcessing)
        {
            if(UpdateCredit(AmbCost))
            StartCoroutine(LanternActivationENUM());
        }   
    }

    public IEnumerator LanternActivationENUM()
    {
        LanternCoroutineRunning = true;
        var AlphaChanger =  LanternButton.GetComponent<TweenAlpha>();
        AlphaChanger.PlayForward();
        lanternScript.ActivateLantern();
        yield return new WaitForSeconds(lanternScript.WaitTime);
        AlphaChanger.PlayReverse();
        LanternCoroutineRunning = false;
    }


    public void ActivateLight()
    {
        if (!LightActivated && !GamePreferences.IsProcessing)
        {
            if (UpdateCredit(LightCost))
            {
                var AlphaChanger = LightButton.GetComponent<TweenAlpha>();
                AlphaChanger.PlayForward();
                playerSwitcher.CurrentPlayer.GetComponentInChildren<LightScript>().ActivateLight();
                LightActivated = true;
            }
        }

    }

    public void ActivateBomb()
    {
        if (!BombCoroutineRunning && !GamePreferences.IsProcessing)
        {
            if(UpdateCredit(bombCost))
            StartCoroutine(BombActivationENUM());
        }
        
    }

    IEnumerator BombActivationENUM()
    {
        BombCoroutineRunning = true;
        var AlphaChanger = BombButton.GetComponent<TweenAlpha>();
        AlphaChanger.PlayForward();
        bombScript.ActivateBomb();
        while (bombScript.BombActivationTrigger == true)
        {
            yield return null;
        }
        AlphaChanger.PlayReverse();
        BombCoroutineRunning = false;

    }


    public void Pause()
    {
        levelManager.GamePaused = true;
    }
    public void Resume()
    {
        levelManager.GamePaused = false;
    }

    public void MusicStateSwitch()
    {
        var tweener = MusicToggler.GetComponent<TweenAlpha>();
        DataManager.MusicOnOff = MusicToggler.value;
        DataManager.SetMusicState();
        if (MusicToggler.value)
        {
            tweener.PlayForwardBypassProccess();
            musicController.PlayMusic(musicController.BGPlayDelayTime);
        }

        else
        {
            tweener.PlayReverseBypassProccess();
            musicController.StopMusic();
        }
        


    }



}
