using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this is the enemy class, it contains a gameobject, that point to a gameobject in the scene, and a health, that tracks their hp, so we know when they die
[System.Serializable]
public class Enemy
{
    public GameObject EnemyObject;
    public float health;
    public float distanceToUni;

    public Enemy(GameObject enemyIn, float hp, float dist)
    {
        EnemyObject = enemyIn;
        health = hp;
        distanceToUni = dist;
    }
}

public class TowerShootsScript : MonoBehaviour
{


    public Enemy target;
    public List<Enemy> targets; // this list will contain all the enemies in the range of the tower
    public float damage = 5;
    public float slowEnemyBy = 0;
    public float slowEnemyFor = 0;
    public float shootCoolDown = 0;
    public bool hitsFlying = false;
    public float fireRate = 0.3f; //how much time passes between shots.

    public int level = 0;
    public int cost = 100;

    public GameObject projectile;
    public GameObject nextLevelTower;

    public enum targeting { First, Strongest, Closest };
    public targeting myTargetingMethod = targeting.First;

    // Use this for initialization
    void Start()
    {
        targets = new List<Enemy>(); //run the constuctor of the list

    }

    // Update is called once per frame
    void Update()
    {
        CleanUpDestroyedTargets();
        UpdateDistancesFromUni();
        DetermineNewTarget();
        shootCoolDown += Time.deltaTime; //tick the cooldown
        if (targets.Count != 0 && shootCoolDown >= fireRate)
        {



            //selecting the target closest to the uni




            if (target.EnemyObject != null)
            {
                Shoot();
            }

        }

    }


    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enemy in range"); 
        if (other.gameObject.tag == "Enemy")
            if (other.gameObject.GetComponent<EnemyNavScript>().isFlying == true) //when something enters the range, we check if it is an enemy.
                if (hitsFlying == true)

                {
                    Enemy newEnemy = new Enemy(other.gameObject, other.gameObject.GetComponent<EnemyHealthScript>().health, other.gameObject.GetComponent<EnemyNavScript>().distanceToUni); //if it is an enemy, we add it to our list, allong with its health, and distance from the Uni
                    targets.Add(newEnemy);
                }

        if (other.gameObject.tag == "Enemy")
            if (other.gameObject.GetComponent<EnemyNavScript>().isFlying == false)
            {
                Enemy newEnemy = new Enemy(other.gameObject, other.gameObject.GetComponent<EnemyHealthScript>().health, other.gameObject.GetComponent<EnemyNavScript>().distanceToUni); //if it is an enemy, we add it to our list, allong with its health, and distance from the Uni
                targets.Add(newEnemy);
            }
    }

    public void OnTriggerExit(Collider other)
    {
        //when an enemy leaves the range, we cycle through our list, find it, and remove it from the list.
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].EnemyObject == other.gameObject)
            {
                targets.Remove(targets[i]);
                Debug.Log("enemy out of range");

                CleanUpDestroyedTargets();
                UpdateDistancesFromUni();
                DetermineNewTarget();
            }



        }

    }

    public void DetermineNewTarget()
    {
        //if there are no enemies in range, the empty the target variable
        if (targets.Count == 0)
        {
            target = null;
        }
        else
        {
            //if not, then just put the first enemy as our target for now
            target = targets[0];
        }

        if (myTargetingMethod == targeting.First)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                //Now we compare ever enemy's distance to the university. if it is closer than the target, then we put it as target. After the loop finishes, we will have the closest enemy as target
                if (target.distanceToUni > targets[i].distanceToUni)
                {
                    //Debug.Log("I changed the target");
                    if (targets[i].EnemyObject != null)
                    {
                        target = targets[i];
                    }
                }

            }
        }
        else if (myTargetingMethod == targeting.Strongest)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                //Now we compare ever enemy's distance to the university. if it is closer than the target, then we put it as target. After the loop finishes, we will have the closest enemy as target
                if (target.EnemyObject.GetComponent<EnemyHealthScript>().health > targets[i].EnemyObject.GetComponent<EnemyHealthScript>().health)
                {
                    //Debug.Log("I changed the target");
                    if (targets[i].EnemyObject != null)
                    {
                        target = targets[i];
                    }
                }
            }
        }
        else if (myTargetingMethod == targeting.Closest)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                //Now we compare ever enemy's distance to the university. if it is closer than the target, then we put it as target. After the loop finishes, we will have the closest enemy as target
                if (Vector3.Distance(target.EnemyObject.transform.position, transform.position) > Vector3.Distance(targets[i].EnemyObject.transform.position, transform.position))
                {
                    //Debug.Log("I changed the target");
                    if (targets[i].EnemyObject != null)
                    {
                        target = targets[i];
                    }
                }
            }
        }
    }

    public void CleanUpDestroyedTargets()
    {
        // loop through all targets, if it has been destroyed, remove it from the list
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].EnemyObject == null || targets[i].EnemyObject.GetComponent<EnemyHealthScript>().runningAway)
            {
                targets.Remove(targets[i]);
                i--;
                //Debug.Log("i fixed a removed target");
            }
        }
    }

    public void UpdateDistancesFromUni()
    {
        //loop through all targets, and get the fresh info on how far they are from the uni
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].distanceToUni = targets[i].EnemyObject.GetComponent<EnemyNavScript>().distanceToUni;
        }
    }

    public virtual void Shoot()
    {
        //target.EnemyObject.GetComponent<EnemyHealthScript>().TakeDamage(damage); //deal the damage
        target.health = target.EnemyObject.GetComponent<EnemyHealthScript>().health; //check in with the enemy to see how much health they actually have

        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject; //create the projectile
        newProjectile.GetComponent<ProjectileScript>().myTower = gameObject; //tell the projectile where it was shot from

        shootCoolDown = 0; //reset cooldown
    }


}
