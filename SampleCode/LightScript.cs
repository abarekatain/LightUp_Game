using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {

    //Light Container which is The Child of This GameObject
    public GameObject Light;

    //A Trigger to Activate the Light Of This PLayer
    public bool LightActivationTrigger = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (LightActivationTrigger)
        {
            Light.SetActive(true);
            LightActivationTrigger = false;
        }
	}



    public void ActivateLight()
    {
        LightActivationTrigger = true;
    }
}
