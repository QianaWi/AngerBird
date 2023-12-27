using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution(1600, 1000, false);

		Invoke("LoadAsync", 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadAsync()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
