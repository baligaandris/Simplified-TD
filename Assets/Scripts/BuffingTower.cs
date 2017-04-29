using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffingTower : MonoBehaviour {
    public GameObject [] Towers = new GameObject [16];
    public float buffingSpeed;
    public float buffingDamage;

	// Use this for initialization
	void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
       // Debug.Log("Buffing Tower" + buffedDamage);
        
        if (collider.gameObject.tag == "Tower")
        {
            TowerShootsScript dmg = collider.gameObject.GetComponentInChildren<TowerShootsScript>();
            dmg.damage += collider.gameObject.GetComponentInChildren<TowerShootsScript>().damage * buffingDamage;
            TowerShootsScript spd = collider.gameObject.GetComponentInChildren<TowerShootsScript>();
            spd.fireRate -= collider.gameObject.GetComponentInChildren<TowerShootsScript>().fireRate * buffingSpeed;

            
            Debug.Log(dmg.damage);
            Debug.Log(spd.fireRate);
            


           // Buff();

        }
    }
}

  

