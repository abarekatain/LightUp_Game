using UnityEngine;
using System.Collections;

public class SpriteFader : MonoBehaviour {

    //USAGE SAMPLE
    //StartCoroutine(SpriteFader.FadeOut(LockObstacle.GetComponent<SpriteRenderer>(), 0.1f));



    public static IEnumerator FadeIn(SpriteRenderer Rend, float FadeSpeed,bool ActivateBeforeTransition)
    {
        if (ActivateBeforeTransition)
            Rend.gameObject.SetActive(true);
        Rend.color = new Color(Rend.color.r, Rend.color.g, Rend.color.b, 0f);
        while (Rend.color.a < 0.7f)
        {
            Rend.color = Color.Lerp(Rend.color, new Color(Rend.color.r, Rend.color.g, Rend.color.b, 1f), FadeSpeed);
            yield return new WaitForEndOfFrame();
        }
        Rend.color = new Color(Rend.color.r, Rend.color.g, Rend.color.b, 1f);

    }


    public static IEnumerator FadeOut(SpriteRenderer Rend, float FadeSpeed,bool DeactivateAfterTransition)
    {
        Rend.color = new Color(Rend.color.r, Rend.color.g, Rend.color.b, 1f);
        while (Rend.color.a > 0.4f)
        {
            Rend.color = Color.Lerp(Rend.color, new Color(Rend.color.r, Rend.color.g, Rend.color.b, 0f), FadeSpeed);
            yield return new WaitForEndOfFrame();
        }
        if(DeactivateAfterTransition)
        Rend.gameObject.SetActive(false);
        Rend.color = new Color(Rend.color.r, Rend.color.g, Rend.color.b, 0f);

    }
}
