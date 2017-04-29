using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClickScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>().activeTower = gameObject;
            }
        }
    }
}
