using UnityEngine;
using System.Collections;

public class BrushScript : MonoBehaviour {


    public SpriteRenderer renderer2D;

    public void UpdateBrush(Sprite sprite)
    {
        renderer2D.sprite = sprite;
    }
}
