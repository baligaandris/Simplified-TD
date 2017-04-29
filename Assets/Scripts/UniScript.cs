using UnityEngine;
using System.Collections;

public class UniScript : MonoBehaviour {

    private GameObject gameData; //our game data object

	// Use this for initialization
	void Start () {
        gameData = GameObject.FindGameObjectWithTag("GameData"); //we get the game data object for later reference
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //when an enemy walks in, deal damage to the uni, and then destroy that enemy.
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            gameData.GetComponent<GameDataScript>().TakeDamage(other.gameObject.GetComponent<EnemyHealthScript>().damage);
            Destroy(other.gameObject);
        }
    }
}
