using UnityEngine;
using System.Collections;

public class LanternScript : MonoBehaviour {
    public bool LanternActivationTrigger = false;

    public float WaitTime = 3f;

    public LevelManager LM;

    MusicController musicController;
    // Use this for initialization
    void Start () {
        musicController = MusicController.ControllerInstance;
    }
	
	// Update is called once per frame
	void Update () {

        if (LanternActivationTrigger)
        {
            musicController.PlayTempMusic(11);
            StartCoroutine("StartLantern");
            LanternActivationTrigger = false;
        }
	}

    IEnumerator StartLantern()
    {
        LM.AppearObjects();
        yield return new WaitForSeconds(WaitTime);
        LM.DisappearObjects();
    }



    public void ActivateLantern()
    {
        LanternActivationTrigger = true;
    }
}
