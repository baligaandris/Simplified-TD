using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameDataScript : MonoBehaviour {

    public float uniHealth = 100;
    public int usac = 500;

    public Text healthDisplay;
    public Text usacDisplay;
    public Transform loseMenu;

    public TowerRadialMenuScript radialMenu;
    public GameObject activeTower;
    private RaycastHit hit;

    // Use this for initialization
    void Start () {
        UpdateUI();
        hit = new RaycastHit();
        radialMenu = GameObject.FindGameObjectWithTag("RadialMenu").GetComponent<TowerRadialMenuScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {

            //we raycast to where we click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (activeTower == null)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {

                    if (hit.collider.gameObject.tag == "Tower")
                    {
                        //if we click on a tower, we change that to be the active tower
                        //StartCoroutine(SlowlyChangeActiveTower());
                        activeTower = hit.collider.gameObject;
                        if (activeTower.GetComponentInChildren<TowerShootsScript>().level == 0)
                        {
                            radialMenu.levelUpRadialMenu.ActivateRadialMenu();
                        }
                        else
                        {
                            radialMenu.ActivateRadialMenu();
                        }
                    }
                    //else
                    //{
                    //    //if we don't hit a tower, we just close the menu
                    //    StartCoroutine(SlowlyCloseRadialMenu());

                    //}
                }
            }
        }
            

        GameOver();
	}

    //we call this script, when an enemy walks into the Uni, to deal damage to it.
    public void TakeDamage(float damage) {
        uniHealth -= damage;
        UpdateUI();

    }

    public void ChangeUsac(int value)
    {
        usac += value;
        UpdateUI();
    } 

    //we will have more things to display, we can keep updating this method, to use it to update all UI elements.
    void UpdateUI() {
        //this just updates all the values in our health and USAC displays
        healthDisplay.GetComponent<Text>().text = uniHealth.ToString();
        usacDisplay.GetComponent<Text>().text = usac.ToString();
    }

	public void GameOver (){
		if (uniHealth <= 0) {
			if (loseMenu.gameObject.activeInHierarchy == false) {
			        loseMenu.gameObject.SetActive (true);
            }
		}
	}
    //these two coroutines delay changing the active tower by a little to make sure the button click goes through
    IEnumerator SlowlyCloseRadialMenu() {
        yield return new WaitForSeconds(0.1f);
        activeTower = null;
    }

    IEnumerator SlowlyChangeActiveTower()
    {
        yield return new WaitForSeconds(0.1f);
        activeTower = hit.collider.gameObject;
    }
}
