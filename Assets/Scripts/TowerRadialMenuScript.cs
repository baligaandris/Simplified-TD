using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerRadialMenuScript : MonoBehaviour {

    private GameObject activeTower; //this is where we will temporarily store the active tower. it needs to be updated every frame, to make sure we do stuff to the right tower.
    private GameDataScript gameData; //this is where we will store a reference to our game data script

	public AudioSource artStudent;
	public AudioSource audioStudent;
	public AudioSource gymStudent;
	public AudioSource computerStudent;

    public TowerRadialMenuScript levelUpRadialMenu;
    private TowerRadialMenuScript targetingRadialMenu;

    public GameObject aoeTower;
    public GameObject sniperTower;
    public GameObject slowTower;
    public GameObject buffTower;

    public GameObject towerPrePlaced;

    public GameObject audioInfo;
    public GameObject compInfo;
    public GameObject sportsInfo;
    public GameObject artInfo;

    public Text priceText;
 

    // Use this for initialization
    void Start () {
        gameData = GameObject.FindWithTag("GameData").GetComponent<GameDataScript>();
        //we get the gamedata script for later reference
        levelUpRadialMenu = GameObject.FindGameObjectWithTag("LevelUpRadialMenu").GetComponent<TowerRadialMenuScript>();
        targetingRadialMenu = GameObject.FindGameObjectWithTag("TargetingRadialMenu").GetComponent<TowerRadialMenuScript>();
    }
	
	// Update is called once per frame
	void Update () {
        //we get the active tower to make sure it we are doing things to the right tower.

        //if there is an active tower, we make the menu visible. this means the circle and all the little buttons attached to it as children. 
        //if (activeTower != null)
        //{
        //    ActivateRadialMenu();
        //}
        //else {
        //    DeactivateRadialMenu();
        //}
        if (transform.localScale.y < 1 && GetComponent<RawImage>().enabled == true) {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 5 * Time.deltaTime, gameObject.transform.localScale.y + 5 * Time.deltaTime, gameObject.transform.localScale.z + 5 * Time.deltaTime);
        }
	}

    public void Button1Click() {
        Debug.Log("Button clicked");

    }

    public void CancelButtonClick() {
        gameData.activeTower = null;
        DeactivateRadialMenu();
    }
    public void ActivateRadialMenu()
    {
        activeTower = gameData.activeTower;
        GetComponent<RawImage>().enabled = true;
        int numberOfChildren = transform.childCount;
        for (int i = 0; i < numberOfChildren; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        //we also move it to sit on top of the active tower
        if (activeTower.GetComponentInChildren<TowerShootsScript>().level == 4) {
            if (transform.Find("Level Up button") != null)
            {
                transform.Find("Level Up button").gameObject.SetActive(false);
            }
        }
        transform.position = Camera.main.WorldToScreenPoint(activeTower.transform.position);
        if (priceText != null && activeTower.GetComponentInChildren<TowerShootsScript>().nextLevelTower!=null) {
            priceText.GetComponent<Text>().text = "£" + activeTower.GetComponentInChildren<TowerShootsScript>().nextLevelTower.GetComponentInChildren<TowerShootsScript>().cost.ToString();
        }
        
        transform.localScale = new Vector3(0,0,0);

    }

    public void DeactivateRadialMenu() {
        //if there is no active tower, we turn this menu, and all children of it off
        int numberOfChildren = transform.childCount;
        GetComponent<RawImage>().enabled = false;
        for (int i = 0; i < numberOfChildren; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void OpenLevelUpMenu() {
        levelUpRadialMenu.ActivateRadialMenu();
        DeactivateRadialMenu();
    }

    public void OpenTargetingMenu() {
        targetingRadialMenu.ActivateRadialMenu();
        DeactivateRadialMenu();
    }

    public void LevelUpButtonClicked() {

        if (activeTower.GetComponentInChildren<TowerShootsScript>().level == 0)
        {
            OpenLevelUpMenu();
        }
        else {
            GameObject nextLevelTower = activeTower.GetComponentInChildren<TowerShootsScript>().nextLevelTower;
            
            UpgradeTo(nextLevelTower);
            
        }
        
    }

    private void UpgradeTo(GameObject HigherLevelTower) {
        if (gameData.usac >= HigherLevelTower.GetComponentInChildren<TowerShootsScript>().cost)
        {
            gameData.ChangeUsac(-HigherLevelTower.GetComponentInChildren<TowerShootsScript>().cost);
            GameObject newTower = Instantiate(HigherLevelTower, activeTower.transform.position, Quaternion.identity);
            newTower.transform.Find("Tower sprite").GetComponent<SpriteRenderer>().sortingOrder = activeTower.transform.Find("Tower sprite").GetComponent<SpriteRenderer>().sortingOrder;
            Debug.Log(activeTower.transform.Find("Tower sprite").GetComponent<SpriteRenderer>().sortingOrder.ToString());

            Destroy(activeTower);
            DeactivateRadialMenu();
        }
    }

    public void AOEButtonClicked() {
		audioStudent.Play ();
        UpgradeTo(aoeTower);
    }

    public void SniperButtonClicked()
    {
		computerStudent.Play ();
        UpgradeTo(sniperTower);
    }

    public void SlowingButtonClicked()
    {
		artStudent.Play ();
        UpgradeTo(slowTower);
    }

    public void BuffingButtonClicked()
    {
		gymStudent.Play ();
        UpgradeTo(buffTower);
    }

    public void SellButtonClicked() {
        gameData.ChangeUsac(activeTower.GetComponentInChildren<TowerShootsScript>().cost);
        Instantiate(towerPrePlaced, activeTower.transform.position, Quaternion.identity);
        Destroy(activeTower);
        
        DeactivateRadialMenu();
    }

    public void TargetingFirstClicked() {
        activeTower.GetComponentInChildren<TowerShootsScript>().myTargetingMethod = TowerShootsScript.targeting.First;
        DeactivateRadialMenu();
        gameData.activeTower = null;
    }
    public void TargetingStrongClicked()
    {
        activeTower.GetComponentInChildren<TowerShootsScript>().myTargetingMethod = TowerShootsScript.targeting.Strongest;
        DeactivateRadialMenu();
        gameData.activeTower = null;
    }
    public void TargetingCloseClicked()
    {
        activeTower.GetComponentInChildren<TowerShootsScript>().myTargetingMethod = TowerShootsScript.targeting.Closest;
        DeactivateRadialMenu();
        gameData.activeTower = null;
    }
    public void InfoButtonClicked() {
        if (audioInfo.activeInHierarchy)
        {
            audioInfo.SetActive(false);
            compInfo.SetActive(false);
            sportsInfo.SetActive(false);
            artInfo.SetActive(false);
        }
        else {
            audioInfo.SetActive(true);
            compInfo.SetActive(true);
            sportsInfo.SetActive(true);
            artInfo.SetActive(true);
        }
        

    }
}
