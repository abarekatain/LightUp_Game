using UnityEngine;
using System.Collections;

public class RadarScript : MonoBehaviour {
    public PlayerSelectionSwitch PlayerSwitcher;
    public float CastRadius;
    public float DistanceTolerance;
    RaycastHit2D[] hit;
    Transform CurrentPlayerPosition;

    public float DistanceFactor;
    RaycastHit2D NearestObj;
    float tempVar;
	// Use this for initialization
	void Start () {
        CurrentPlayerPosition = PlayerSwitcher.CurrentPlayer.transform;

    }
	
	// Update is called once per frame
	void Update () {

        hit = Physics2D.CircleCastAll(CurrentPlayerPosition.position, CastRadius, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Obstacle"));
        if (hit.Length > 0)
        {
            NearestObj = hit[0];
            for (int i = 0; i < hit.Length; i++)
            {
                if (Distance(NearestObj) > Distance(hit[i]))
                    NearestObj = hit[i];
            }
            //Debug.DrawRay(CurrentPlayerPosition.position, new Vector3(NearestObj.point.x, NearestObj.point.y) - CurrentPlayerPosition.position);
            //Debug.Log(Distance(NearestObj));
            DistanceFactor = (Distance(NearestObj) / (CastRadius - DistanceTolerance));

        }
        else
        {
            DistanceFactor = 1;
        }
        

    }


    public float Distance(RaycastHit2D item)
    {
        return Mathf.Abs(Mathf.Abs(Vector2.Distance(CurrentPlayerPosition.position, item.point))-DistanceTolerance);

    }
}
