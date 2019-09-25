using UnityEngine;
using System.Collections;

public class GuideScript : MonoBehaviour {
    TweenAlpha GuidePanelTweener;
    bool OneTimeTrigger = true;
    public LevelManager levelManager;
	// Use this for initialization
	void Start () {
        GuidePanelTweener = gameObject.GetComponent<TweenAlpha>();
	}
	
	// Update is called once per frame
	void Update () {

        if (levelManager.StartEnd && OneTimeTrigger)
        {
            GuidePanelTweener.from = 1;
            GuidePanelTweener.to = 0;
            GuidePanelTweener.duration = levelManager.FadeTime;
            GuidePanelTweener.PlayForward();
            OneTimeTrigger = false;
        }
	}
}
