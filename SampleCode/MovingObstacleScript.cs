using UnityEngine;
using System.Collections;


public class MovingObstacleScript : MonoBehaviour {


    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    private Vector3 startPos;
    public enum Direction
    {
        Vertical,Horizontal
    }
    public Direction direction;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        Vector3 v = startPos;
        if(direction == Direction.Vertical)
        v.y += delta * Mathf.Sin(Time.time * speed);
        else
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.localPosition = v;
    }
}
