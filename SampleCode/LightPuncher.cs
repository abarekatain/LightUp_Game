using UnityEngine;
using System.Collections;

public class LightPuncher : MonoBehaviour {

    public float mag = 60f;
    public float lastOffset;
    public Material dl;
    

    void Start()
    {
        dl = GetComponent<Renderer>().material;
        lastOffset = dl.color.a;
        StartCoroutine(updateLoop());

    }

    IEnumerator updateLoop()
    {

        while (true)
        {
            yield return new WaitForSeconds(1f);
            float rnd = Random.Range(-1f, 1f) * mag;
            yield return null;

            Color32 col = dl.GetColor("_Color");
            col.a = (byte)(lastOffset + rnd);
            dl.SetColor("_Color", col);
            Debug.Log(col);
            yield return new WaitForEndOfFrame();
        }
    }
}
