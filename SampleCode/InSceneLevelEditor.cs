using UnityEngine;
using System.Collections;

public class InSceneLevelEditor : MonoBehaviour {


    public GameObject[] Obstacles;

    public GameObject[] Collectables;

    public GameObject[] Players;

    public GameObject[] Others;

    public Vector2 CurrentIndex;



    public Transform ObstacleContainer, CollectablesContainer, PlayersContainer,OthersContainer;

    public GameObject CurrentObject
    {

        get {
            try
            {
                if (CurrentIndex.x == 0)
                    return Obstacles[(int)CurrentIndex.y];
                else if (CurrentIndex.x == 1)
                    return Collectables[(int)CurrentIndex.y];
                else if (CurrentIndex.x == 2)
                    return Players[(int)CurrentIndex.y];
                else if (CurrentIndex.x == 3)
                    return Others[(int)CurrentIndex.y];
                else return null;
            }
            catch { return null; }

        }
    }

    public Vector2 BackgroundSize
    {
        get
        {
            return new Vector2(gameObject.GetComponent<SpriteRenderer>().bounds.size.x, gameObject.GetComponent<SpriteRenderer>().bounds.size.y);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
