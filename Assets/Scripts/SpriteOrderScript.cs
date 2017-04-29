using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.GetComponentInChildren<SpriteRenderer>().sortingOrder -= 2;
            collider.GetComponentInChildren<Canvas>().sortingOrder -= 2;

        }
    }
}
