using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

    public bool IsBorder = false;

    public GameObject Overlay;

    public LevelManager levelManager;

    MusicController musicController;

    float WaitTimeAfterLosing = 1f;
    // Use this for initialization
    void Start () {
        musicController = MusicController.ControllerInstance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Player" && !IsBorder)
        {
            StartCoroutine(Failed(Col));

        }

    }



    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "Player" && IsBorder)
        {
            StartCoroutine(Failed(Col));
        }

    }

    IEnumerator Failed(Collider2D Col)
    {
        levelManager.GamePaused = true;
        levelManager.AppearObjects(0.05f);
        musicController.PlayTempMusic(13);
        yield return new WaitForSeconds(WaitTimeAfterLosing);
        GameObject.FindGameObjectWithTag("Failure").GetComponent<TweenAlpha>().PlayForward();
        Col.gameObject.SetActive(false);
        iTween.FadeTo(Overlay, 1, 0.5f);
        levelManager.GamePaused = true;        
        yield return null;
    }
}
