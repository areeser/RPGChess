using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI() {
        if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 200, 150, 100), "Replay"))
        {
            SceneManager.LoadScene("Board");
        }
    }
}
