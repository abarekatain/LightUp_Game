using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    Vector3 PrimaryTouch = Vector3.zero;
    Vector3 touchPosition;
    Vector3 Distance;

    Sprite thisSprite;
    public LevelManager LM;

    public Sprite ActiveSprite, DeactiveSprite;
     
    //Maximum Distance To Move In One Frame, To avoid Jumping The pLayer
     public float MaximumDistance;

     //Trigger to Initialize the primary Touch Position In the First Frame of touch Movement
     bool OneTimeTrigger = false;

     //Is this Player Selected to move or not!
     public bool IsSelected = true;

     //In order that Player Rotate Smoothly toward Movement Direction, An interpoplation(Lerp) Has been apllied
     //,The Lerp Factor 't' Depends on The Speed Of Player Movement at Frame,These two Variables Will connfigure the Rate of Smoothness Change per Frame 
     public float LerpFactorMinimumDistance,LerpFactorMaximumDistance;

    
    Vector3 PlayerFirstPosition;

    //Trigger To Start The Level When Player Moved a Distance
    bool LevelStartTrigger = true;


    // Use this for initialization
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>().sprite;
        PlayerFirstPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LM.GamePaused)
        {
            if (IsSelected)
            {
                if (GetComponent<SpriteRenderer>().sprite != ActiveSprite)
                    GetComponent<SpriteRenderer>().sprite = ActiveSprite;
                if (Input.touchCount > 0)
                {
                    // The screen has been touched so store the touch
                    Touch touch = Input.GetTouch(0);

                    //Initialize the First Touch Position
                    if (touch.phase == TouchPhase.Stationary)
                        PrimaryTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));


                    if (touch.phase == TouchPhase.Moved)
                    {
                        //Checking The Conditions for Starting The Level
                        if (LevelStartTrigger)
                        {
                            //Check if Player has Moved a Distance,Then Start the Level
                            if (Vector3.Distance(PlayerFirstPosition, transform.position) > LM.MoveLengthToStart)
                            {
                                //Tell The Level Manager That Game Has Been Started
                                LM.StartEnd = true;
                                LevelStartTrigger = false;
                            }
                        }

                        //One Time Initialize the First Touch Position to Avoid Player Jumping
                        if (OneTimeTrigger)
                        {

                            PrimaryTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                            OneTimeTrigger = false;
                        }

                        //And Finally Move!!
                        Move(touch);
                    }
                }
                else
                {
                    //Reset The Initialization Trigger
                    OneTimeTrigger = true;
                }
            }
            else
            {
                if (GetComponent<SpriteRenderer>().sprite != DeactiveSprite)
                    GetComponent<SpriteRenderer>().sprite = DeactiveSprite;
            }
        }
            

    }

    private void Move(Touch touch)
    {


        touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -0.01f));
        touchPosition.z = -0.01f;
        PrimaryTouch.z = -0.01f;

        Distance = touchPosition - PrimaryTouch;
        PrimaryTouch = touchPosition;

        if (Distance.magnitude < MaximumDistance)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Distance, 1);
        }
        if (Distance.magnitude > LerpFactorMinimumDistance)
        {
            var dir = Distance;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle + 180, new Vector3(0, 0, 1)), (Distance.magnitude - LerpFactorMinimumDistance)/(LerpFactorMaximumDistance - LerpFactorMinimumDistance));

        }
    }


}
