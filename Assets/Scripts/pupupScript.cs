using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pupupScript : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        transform.localScale = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.y < 1 && GetComponent<RawImage>().enabled == true)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 5 * Time.deltaTime, gameObject.transform.localScale.y + 5 * Time.deltaTime, gameObject.transform.localScale.z + 5 * Time.deltaTime);
        }
    }
}
