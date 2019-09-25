using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {
    public Animator StarAnimator;
    public LevelManager levelManager;
    Collider2D col;
    MusicController musicController;
	// Use this for initialization
	void Start () {
        StarAnimator = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        musicController = MusicController.ControllerInstance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Player")
        {
            iTween.FadeTo(col.gameObject, 1, 0);
            StarAnimator.SetTrigger("AnimationTrigger");
            col.enabled = false;
            StarSound();
            levelManager.StarsGained += 1;
            var Rend = GetComponent<SpriteRenderer>();
            Rend.color = new Color(Rend.color.r, Rend.color.g, Rend.color.b, 0f);
        }
            
    }

    void StarSound()
    {
        switch (levelManager.StarsGained)
        {
            case 0:
                musicController.PlayTempMusic(6);
                break;
            case 1:
                musicController.PlayTempMusic(7);
                break;
            case 2:
                musicController.PlayTempMusic(8);
                break;
            default:
                break;
        }
    }
}
