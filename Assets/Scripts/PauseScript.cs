using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

	public Transform pauseMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PauseButton () {
		if (Time.timeScale == 1) {
			pauseMenu.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else {
			pauseMenu.gameObject.SetActive (false);
			Time.timeScale = 1;
		}
	}
    public void QuitButton() {
        Application.Quit();

    }

    public void MainMenuButton() {
        Application.LoadLevel("MainMenuScene");
    }
}
