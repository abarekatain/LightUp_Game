using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {
    public LevelFinishScript FinishScript;

    public GameObject Overlay;
    public LevelManager levelManager;

    MusicController musicController;

    // Use this for initialization
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
            musicController.PlayTempMusic(12);
            FinishScript.UpdateGameData();
            iTween.FadeTo(gameObject, 1, 0.05f);

            Col.gameObject.GetComponent<TrailRenderer>().enabled = false;
            StartCoroutine(SuccessAnim(Col));
            
        }

    }

    IEnumerator SuccessAnim(Collider2D col)
    {
        iTween.MoveTo(col.gameObject, gameObject.transform.position + new Vector3(0,0.195f,0), 2f);
        iTween.RotateTo(col.gameObject, new Vector3(0, 0, 90), 2.5f);
        yield return new WaitForSeconds(2f);
        var animator = GetComponent<Animator>();
        animator.SetTrigger("AnimationTrigger");
        yield return new WaitForSeconds(1.75f);
        //Fade The Scene And Show The Results
        iTween.FadeTo(Overlay, 1, 0.5f);
        levelManager.GamePaused = true;
        var SuccessPanel = GameObject.FindGameObjectWithTag("Success");
        SuccessPanel.GetComponent<TweenAlpha>().PlayForward();
        SuccessPanel.GetComponent<TweenScale>().PlayForward();
        FinishScript.SetStarsVisual();
    }
}
