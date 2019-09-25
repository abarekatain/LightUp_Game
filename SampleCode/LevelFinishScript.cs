using UnityEngine;
using System.Collections;

public class LevelFinishScript : MonoBehaviour {
    public GameObject[] Stars;
    public LevelManager LM;
    public LevelUIManager UIManager;
    GameDataManager DataManager;
    MusicController musicController;



    void Start () {
        DataManager = GameDataManager.ManangerInstance;
        musicController = MusicController.ControllerInstance;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetStarsVisual()
    {
        for (int i = 0; i < LM.StarsGained; i++)
        {
            Stars[i].GetComponent<TweenAlpha>().PlayForward();
            Stars[i].GetComponent<TweenRotation>().PlayForward();
            Stars[i].GetComponent<TweenScale>().PlayForward();

        }
    }



    public void NextLevel()
    {
        var index = LM.ThisLevelIndex + 1;
        var CurrentSeason = Mathf.FloorToInt(index / (GamePreferences.EachSeasonLevel*2));
        if (DataManager.Coins >= GamePreferences.SeasonsStarLimit[CurrentSeason])
        if (!GamePreferences.IsProcessing)
        {
            musicController.StopAllTempSounds();
            RemoveTrails();
            UIManager.StartLoading("Level" + (LM.ThisLevelIndex + 1).ToString());
        }
    }

    public void RetryLevel()
    {
        if (!GamePreferences.IsProcessing)
        {
            musicController.StopAllTempSounds();
            RemoveTrails();
            UIManager.StartLoading("Level" + (LM.ThisLevelIndex).ToString());
        }
    }

    public void Return2MainMenu()
    {
        if (!GamePreferences.IsProcessing)
        {
            RemoveTrails();
            UIManager.Return2MainMenu();
        }
    }

    public void RemoveTrails()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            item.GetComponent<TrailRenderer>().enabled = false;
        }
    }


    public void UpdateGameData()
    {
        var OldStars = DataManager.LevelsStars[LM.ThisLevelIndex];
        if(OldStars == 9) //It Means That Level Has not Been Passed
        {
            DataManager.Coins += LM.StarsGained;
            DataManager.LevelsStars[LM.ThisLevelIndex] = LM.StarsGained;
        }
        else if ( OldStars < LM.StarsGained) // Check If Current Stars Are More Than Old Stars
        {
            DataManager.Coins += LM.StarsGained - OldStars;
            DataManager.LevelsStars[LM.ThisLevelIndex] = LM.StarsGained;
            
        }
        if (DataManager.CurrentLevel == LM.ThisLevelIndex && DataManager.CurrentLevel != DataManager.LevelsStars.Length-1)
            DataManager.CurrentLevel++;
        DataManager.Save();
    }


    
}
