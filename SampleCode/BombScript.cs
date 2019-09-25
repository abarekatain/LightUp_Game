using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {
    //Trigger To Activate Bomb Module From Another Script
    public bool BombActivationTrigger = false;
    //Bomb Animation Controller
    Animator animator;
    //Collider of The Object Been Hit
    Collider2D col;
    //Hit Collider Sprite Renderer
    SpriteRenderer Rend;
    //Trigger to Start Exploding The Object
    bool ExplosionTrigger = false;
    //Variable Between 0 & 1 To Control The Fading And Explosion Speed
    public float FadeSpeed = 0.05f;

    public LevelManager levelManager;

    MusicController musicController;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        musicController = MusicController.ControllerInstance;
	}
	
	// Update is called once per frame
	void Update () {
        if (!levelManager.GamePaused)
        {
            if (BombActivationTrigger)
            {
                col = CheckForTouch();
                //If There's a Collider Under The Touch Position
                if (col != null)
                    //If The Collider is an Obstacle
                    if (col.tag == "Obstacle")
                    {
                        iTween.FadeTo(col.gameObject, 1, 0);
                        Rend = col.gameObject.GetComponent<SpriteRenderer>();
                        //Start Explosion
                        ExplosionTrigger = true;
                        BombActivationTrigger = false;
                        //Even if The object is Hidden,It Should Be reEnabled
                        Rend.enabled = true;
                    }
            }
            //if Everything is OK for Exploding
            else if (ExplosionTrigger)
            {
                //Make The Sound!
                musicController.PlayBombTempMusic(9);
                //Fade The Col Renderer By Lerping its Alpha Value
                Rend.color = Color.Lerp(Rend.color, new Color(Rend.color.r, Rend.color.g, Rend.color.b, 0f), FadeSpeed);
                //When it's Almost Disappeared (Alpha<0.5)
                if (Rend.color.a < 0.5f)
                {
                    //Disable The Object
                    Rend.gameObject.SetActive(false);
                    //Move The Bomb to The Col Position
                    transform.position = col.transform.position;
                    //Start Animating The Bomb
                    animator.SetTrigger("AnimationTrigger");
                    //And Finally Disable The Collider And Renderer;Now it's Done :-)
                    col.enabled = false;
                    Rend.enabled = false;
                    ExplosionTrigger = false;
                }
            }
        }
	}

    //Function To Check which collider hits the Touch
    public Collider2D CheckForTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.touches[0];
            if (t.phase == TouchPhase.Began)
            {
                Vector3 WorldPoint = Camera.main.ScreenToWorldPoint(t.position);
                Vector2 TouchPos = new Vector2(WorldPoint.x, WorldPoint.y);
                return Physics2D.OverlapPoint(TouchPos);
            }
            else return null;
        }
        else return null;
    }


    public void ActivateBomb()
    {
        BombActivationTrigger = true;
    }
}
