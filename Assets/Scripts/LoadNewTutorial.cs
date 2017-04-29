using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewTutorial : MonoBehaviour {

	public GameObject previousTutorial;
	public GameObject currentTutorial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0)
		if (previousTutorial == false) {
			currentTutorial.SetActive (true);
		}
	}
}
