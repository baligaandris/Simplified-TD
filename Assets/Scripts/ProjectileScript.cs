using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileScript : MonoBehaviour {

    public GameObject myTower; //this is the tower the projectile is fired from. We need it. it is set by the tower that fires it. Which is kind of a problem, but I'll deal with it some other time.
    GameObject target;
    public float speed = 100;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //if there are no more targets within range of my tower, just destroy self
        if (myTower ==null ||myTower.GetComponent<TowerShootsScript>().targets.Count == 0|| myTower.GetComponent<TowerShootsScript>().target == null||myTower.GetComponent<TowerShootsScript>().target.EnemyObject==null)
        {
            Destroy(gameObject);
        }
        else //if there are some, fly towards it. getting the target from the tower
        {

            target = myTower.GetComponent<TowerShootsScript>().target.EnemyObject;
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            transform.rotation = Quaternion.LookRotation(target.transform.position-transform.position);
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            if (transform.position == target.transform.position){
                if (target.GetComponent<EnemyHealthScript>().health > 0)
                {
					gameObject.GetComponent<AudioSource> ().Play();
                    target.GetComponent<EnemyHealthScript>().TakeDamage(myTower.GetComponent<TowerShootsScript>().damage);
                    target.GetComponent<EnemyNavScript>().SlowMeDown(myTower.GetComponent<TowerShootsScript>().slowEnemyBy, myTower.GetComponent<TowerShootsScript>().slowEnemyFor);
                }
                Destroy(gameObject);
            }


            //if you reach your target, deal your tower's damage and destroy self.

        }
    }



}
