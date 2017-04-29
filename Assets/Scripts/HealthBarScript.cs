using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {

    
    Camera mainCamera; //we need our camera to look at the health bars
    RectTransform hpTransform; //we will be changing the health bar
    public GameObject remainingHealth;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main; 
        EnemyHealthScript enemyHealth = gameObject.GetComponentInParent<EnemyHealthScript>(); //finding the script with that contains health variables
        enemyHealth.OnHealthChanged += HealthChanged; // calling the public event since the camera needs to keep track of the health
        hpTransform = (RectTransform)gameObject.transform.Find("RemainingHealth").transform; //finding the green bar with the remaining health
	}



    private void HealthChanged(float currentHealth, float health) //
    {
        // Debug.Log("The current health is" + currentHealth); Trying to see if it finds that the health is changed.
        if (hpTransform != null)
        {
            hpTransform.localScale = new Vector3(health / currentHealth, 1f); // setting the currentHealth to the green bar and the red bar to the remaining health of the enemy. If the positions were swapped (health / currentHealth) the green bar would increase after taking damage
            if (health <= 0)
            {
                Destroy(remainingHealth);
            }
        }
    }



    // Update is called once per frame
    void Update () {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up); //we need this for each frame to check the status of the bars and update us that they are moving in the correct order
       // remainingHealth.transform.localScale = new
    }
}
