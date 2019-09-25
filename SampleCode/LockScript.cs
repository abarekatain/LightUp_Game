using UnityEngine;
using System.Collections;

public class LockScript : MonoBehaviour {
    public GameObject LockObstacle;
    // Use this for initialization
    MusicController musicController;

    public Collider2D onTriggerPlayer;
	void Start () {
        musicController = MusicController.ControllerInstance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Player")
        {
            if(onTriggerPlayer == null)
            {
                musicController.PlayTempMusic(10);
                iTween.FadeTo(gameObject, 1, 0.2f);
                LockObstacle.SetActive(false);
                onTriggerPlayer = Col;
            }

        }

    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col == onTriggerPlayer)
        {
            iTween.FadeTo(gameObject, 0.5f, 0.2f);
            LockObstacle.SetActive(true);
            onTriggerPlayer = null;
        }

    }


}
