using UnityEngine;
using System.Collections;

public class FitToScreenSize : MonoBehaviour {

   
     void Start()
    {
        FitToScreen();
    }

    //Function That Fits Sprite to Screen Size(Editor or Scene View)
    private void FitToScreen()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        var temp = transform.localScale;
        temp.x = worldScreenWidth / width;
        temp.y = worldScreenHeight / height;
        transform.localScale = temp;

        temp = Camera.main.transform.position;
        temp.x = transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        temp.y = -transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        Camera.main.transform.position = temp;
    }

}
