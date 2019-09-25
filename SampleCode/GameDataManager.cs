using UnityEngine;
using System.Collections;

public class GameDataManager : MonoBehaviour {

    //Array to Store Stars Gained from Levels
    //0,1,2,3[Count of Stars]
    //9[Level incomplete]
    //Note--->Count of This Array Must be Initialized at First Time
    public int[] LevelsStars;

    //Variable to Store Current Unpassed Level
    public int CurrentLevel;

    //Check if It's the First Play Time
    public bool IsFirstPlay;
    public bool FirstPlayGuide = true;

    public bool IsFirstSlide = true;

    public bool IsFirstGuide = true;
    //Check the State of Music
    public bool MusicOnOff = true;

    ////Check The State of Sound
    //public bool SoundOnOff = true;

    public int Coins = 0;

    public int Credit = 0;

    public LevelsManager levelsManager;



    public static GameDataManager ManangerInstance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (ManangerInstance == null)
        {
            ManangerInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }

    }
    void Start ()
    {
        Initialize();
        Debug.Log("First Slide is " + IsFirstSlide);

    }

    public void Initialize()
    {
        //Initialize The LevelsStars Array By levelsManager.LevelsCount....That's Why We Called The LevelsManager Initialization Function In "Awake" Method

        LevelsStars = new int[levelsManager.LevelsCount];
        //Initializing Preferences Data
        FirstPlayCheck();
        GetSlideState();
        GetGuideState();
        GetLevelStars();
        GetCurrentLevel();
        GetMusicState();
        GetCoins();
        GetCredit();
        SetFlags();
        //-----------------------------
    }

    public void SetFlags()
    {
        GamePreferences.InitializationCompleted = true;
        GamePreferences.InitializationCompleted2 = true;
    }


    // Update is called once per frame
    void Update () {
	
	}


    public void GetLevelStars()
    {
        if (IsFirstPlay)
        {
            string temp = null;
            for (int i = 0; i < LevelsStars.Length; i++)
            {
                LevelsStars[i] = 9;
                temp += "9";                
            }
            PlayerPrefs.SetString(GamePreferences.LevelsStars, temp);
        }
        else
        {
            var Str = PlayerPrefs.GetString(GamePreferences.LevelsStars);
            for (int i = 0; i < Str.Length; i++)
            {
                LevelsStars[i] = Str[i] - '0';
            }
        }

    }

    public void SetLevelStars()
    {
        string str = null;
        foreach (var item in LevelsStars)
        {
            str += item.ToString();
        }
        PlayerPrefs.SetString(GamePreferences.LevelsStars, str);
    }




    public void GetCurrentLevel()
    {
        if (IsFirstPlay)
            CurrentLevel = 0;
        else
        {
            if (LevelsStars[LevelsStars.Length - 1] != 9) CurrentLevel = LevelsStars.Length -1;
            else
            for (int i = 0; i < LevelsStars.Length; i++)
            {
                if (LevelsStars[i] == 9)
                {
                    CurrentLevel = i;
                    break;
                }


            }
        }

    }

   

    public void FirstPlayCheck()
    {
        if (PlayerPrefs.HasKey(GamePreferences.FirstPlay))
            IsFirstPlay = false;
        else
        {
            IsFirstPlay = true;
            PlayerPrefs.SetInt(GamePreferences.FirstPlay, 0);
        }
    }

    public void GetSlideState()
    {
        if (IsFirstPlay)
        {
            PlayerPrefs.SetInt(GamePreferences.FirstSlide, 1);
            IsFirstSlide = true;
        }

        else
        {
            var value = PlayerPrefs.GetInt(GamePreferences.FirstSlide);
            if (value == 1)
                IsFirstSlide = true;
            else if (value == 0)
                IsFirstSlide = false;
        }
    }



    public void SetSlideState()
    {
        if (IsFirstSlide)
            PlayerPrefs.SetInt(GamePreferences.FirstSlide, 1);
        else
            PlayerPrefs.SetInt(GamePreferences.FirstSlide, 0);

    }


    public void GetGuideState()
    {
        if (IsFirstPlay)
        {
            PlayerPrefs.SetInt(GamePreferences.FirstGuide, 1);
            IsFirstGuide = true;
        }

        else
        {
            var value = PlayerPrefs.GetInt(GamePreferences.FirstGuide);
            if (value == 1)
                IsFirstGuide = true;
            else if (value == 0)
                IsFirstGuide = false;
        }
    }



    public void SetGuideState()
    {
        if (IsFirstGuide)
            PlayerPrefs.SetInt(GamePreferences.FirstGuide, 1);
        else
            PlayerPrefs.SetInt(GamePreferences.FirstGuide, 0);

    }


    public void GetMusicState()
    {
        if (IsFirstPlay)
        {
            PlayerPrefs.SetInt(GamePreferences.MusicOnOff, 1);
            MusicOnOff = true;
        }

        else
        {
            var value = PlayerPrefs.GetInt(GamePreferences.MusicOnOff);
            if (value == 1)
                MusicOnOff = true;
            else if (value == 0)
                MusicOnOff = false;
        }
    }

    public void SetMusicState()
    {
        if (MusicOnOff)
            PlayerPrefs.SetInt(GamePreferences.MusicOnOff, 1);
        else
            PlayerPrefs.SetInt(GamePreferences.MusicOnOff, 0);

    }





    public void GetCoins()
    {
        if (IsFirstPlay)
        {
            PlayerPrefs.SetInt(GamePreferences.Coins, 0);
            Coins = 0;
        }
        else
        {
            Coins = PlayerPrefs.GetInt(GamePreferences.Coins);
        }
    }

    public void SetCoins()
    {
        PlayerPrefs.SetInt(GamePreferences.Coins, Coins);
    }


    public void GetCredit()
    {
        if (IsFirstPlay)
        {
            PlayerPrefs.SetInt(GamePreferences.Credit, 1000);
            Credit = 1000;
        }
        else
        {
            Credit = PlayerPrefs.GetInt(GamePreferences.Credit);
        }
    }

    public void SetCredit()
    {
        PlayerPrefs.SetInt(GamePreferences.Credit, Credit);
    }

    public void Save()
    {
        SetLevelStars();
        SetSlideState();
        SetGuideState();
        SetMusicState();
        SetCoins();
        SetCredit();
        PlayerPrefs.Save();
    }


}
