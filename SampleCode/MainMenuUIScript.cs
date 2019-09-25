using UnityEngine;
using System.Collections;

public class MainMenuUIScript : MonoBehaviour {
    public GameDataManager DataManager;
    public UIToggle MusicToggler;


    MusicController musicController;
	// Use this for initialization
	void Start () {
        musicController = MusicController.ControllerInstance;
        DataManager = GameDataManager.ManangerInstance;
    }
	
	// Update is called once per frame
	void Update () {
        if (GamePreferences.InitializationCompleted2)
        {
            GamePreferences.InitializationCompleted2 = false;
            MusicToggler.value = DataManager.MusicOnOff;
            musicController.PlayTempMusic(0);
            EventDelegate del = new EventDelegate(this, "MusicStateSwitch");
            MusicToggler.onChange.Add(del);


            EventDelegate.Execute(MusicToggler.onChange);
        }
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

    public void Exit()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        DataManager.Save();
    }



}
