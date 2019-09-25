using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroPageScript : MonoBehaviour {
    public string SceneName;
	// Use this for initialization
	void Start () {
        StartCoroutine(Load());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Load()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneName);

    }
}
