using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETowerScript : TowerShootsScript
{
    

	// Use this for initialization
	void Start () {
        targets = new List<Enemy>(); //run the constuctor of the list
    }
	
	// Update is called once per frame
	void Update () {
        CleanUpDestroyedTargets();
        
        shootCoolDown += Time.deltaTime; //tick the cooldown
        if (targets.Count != 0 && shootCoolDown >= fireRate)
        {



            //selecting the target closest to the uni




                Shoot();


        }
    }

    public override void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
        
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].EnemyObject.GetComponent<EnemyHealthScript>().TakeDamage(damage);
        }
        

        shootCoolDown = 0; //reset cooldown
    }

}
