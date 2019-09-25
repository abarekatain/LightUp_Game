using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerSelectionSwitch : MonoBehaviour
{
    //The List Of Players Will Be Stored In This Variable By The Editor Script
    public List<GameObject> Players;

    int CurrentPlayerIndex = 0;

    public GameObject CurrentPlayer
    {
        get
        {
            return Players[CurrentPlayerIndex];
        }
    }

    void Update()
    {

        for (int i = 0; i < Players.Count; i++)
        {
            
            var hit = CheckForTouch();
            var playerScript = Players[i].GetComponent<PlayerScript>();
            //If a Player Has Been Touched
            if(hit != null)
            if (hit.tag == "Player")
            {
                    //Set The Hit Player to The Selected Player
                if (hit == Players[i].GetComponent<Collider2D>())
                {
                        CurrentPlayerIndex = i;
                    playerScript.IsSelected = true;
                }
                else
                {
                    playerScript.IsSelected = false;
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
}
