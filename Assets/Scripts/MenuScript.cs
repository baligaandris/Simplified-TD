using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuScript : MonoBehaviour {

public GameObject narrativeCanvas;

	IEnumerator DelayQuitGame(){
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
		Application.Quit ();
	}

	IEnumerator DelayLoadStartMenu(){
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
		Application.LoadLevel ("MainMenuScene");
	}

	IEnumerator LoadTutorialLevelye(){
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
		Application.LoadLevel ("TutorialScene");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadTutorialLevel(){
		StartCoroutine (LoadTutorialLevelye ());
	}

	public void QuitGame () {
	
		StartCoroutine (DelayQuitGame ());
	}

	public void LoadStartMenu() {
	
		StartCoroutine (DelayLoadStartMenu ());
	}



}
