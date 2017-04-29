using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeTowerProjectileScript : MonoBehaviour {

    public float speed = 1;
    public float howLongShouldItExist = 1;

	// Use this for initialization
	void Start () {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(WaitAndDestroy());
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + speed * Time.deltaTime, gameObject.transform.localScale.y + speed * Time.deltaTime, gameObject.transform.localScale.z + speed * Time.deltaTime);
	}

    IEnumerator WaitAndDestroy() {
        yield return new WaitForSeconds(howLongShouldItExist);
        Destroy(gameObject);
    }
}
