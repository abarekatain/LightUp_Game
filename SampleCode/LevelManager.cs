using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    //Temp Variable To Store The Stars Gained By Player
    public int StarsGained;
    //Indicator For Starting And Finishing Game,This Variable Will Be Set to True from "PlayerScript"
    public bool StartEnd;
    //Maximum Distance That Player Should Move From its Primary Position to Disappear the Objects
    public float MoveLengthToStart;
    //Stop Recieve Touch Events When Paused;
    public bool GamePaused = false;

    //Store All Objects That Should Be Disappeared
    public Transform CollectablesConainer, ObstacleContainer, OthersContainer;
    GameObject[] Collectables, Obstacles,Others;

    //Speed Of Fading Transitions
    public float FadeTime = 1;

    //A Simple Trigger to Disable The Objects Only Once
    bool DisapparObjectsTrigger = true;

    public int ThisLevelIndex;


    void Awake()
    {
        var LevelName = Application.loadedLevelName;
        ThisLevelIndex = int.Parse(LevelName.Substring(5));
        
    }

	void Start () {
        //Get All Collectables And Obstacles In The Level
        Collectables = GameObject.FindGameObjectsWithTag("Collectable");
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        Others = GameObject.FindGameObjectsWithTag("Other");
    }
	
	
	void Update () {
        
        //Check For The Right Time to Disappear
        if(StartEnd && DisapparObjectsTrigger)
        {
            DisappearObjects();
            DisapparObjectsTrigger = false;
        }


	
	}

    
    //Function To Disappear Objects
    public void DisappearObjects()
    {
        if(Collectables.Length>0)
            foreach (var item in Collectables)
                //item.GetComponent<SpriteRenderer>().enabled = false;
                iTween.FadeTo(item, 0,FadeTime);
        if (Obstacles.Length>0)
            foreach (var item in Obstacles)
                //item.GetComponent<SpriteRenderer>().enabled = false;
                iTween.FadeTo(item, 0, FadeTime);
        if (Others.Length > 0)
            foreach (var item in Others)
                //item.GetComponent<SpriteRenderer>().enabled = false;
                iTween.FadeTo(item, 0.5f, FadeTime);
    }

    //Function To Appear Objects
    public void AppearObjects()
    {
        if (Collectables.Length > 0)
            foreach (var item in Collectables)
                //item.GetComponent<SpriteRenderer>().enabled = true;
                iTween.FadeTo(item, 1, FadeTime);

        if (Obstacles.Length > 0)
            foreach (var item in Obstacles)
                //item.GetComponent<SpriteRenderer>().enabled = true;
                iTween.FadeTo(item,1, FadeTime);

        if (Others.Length > 0)
            foreach (var item in Others)
                //item.GetComponent<SpriteRenderer>().enabled = true;
                iTween.FadeTo(item, 1, FadeTime);
    }

    //Overrride Function To Appear Objects With Custom Fade Time
    public void AppearObjects(float CustomFadeTime)
    {
        if (Collectables.Length > 0)
            foreach (var item in Collectables)
                //item.GetComponent<SpriteRenderer>().enabled = true;
                iTween.FadeTo(item, 1, CustomFadeTime);

        if (Obstacles.Length > 0)
            foreach (var item in Obstacles)
                //item.GetComponent<SpriteRenderer>().enabled = true;
                iTween.FadeTo(item, 1, CustomFadeTime);

        if (Others.Length > 0)
            foreach (var item in Others)
                //item.GetComponent<SpriteRenderer>().enabled = true;
                iTween.FadeTo(item, 1, CustomFadeTime);
    }
}
