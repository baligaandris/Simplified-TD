using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class WavePart {
    public GameObject enemyType;
    public int numberOfEnemies;
    public float howLongTowaitAfter;
    

    public WavePart(GameObject enemyIn,int howMany, float waitTime) {
        enemyType = enemyIn;
        numberOfEnemies = howMany;
        howLongTowaitAfter = waitTime;    
    }
}
[System.Serializable]
public class Wave {
    public WavePart[] wave;
    public Canvas tutorialbeforeWave;
    public Wave(WavePart[] WavePartsIn, Canvas tutorialCanvasIn) {
        wave = WavePartsIn;
        tutorialbeforeWave = tutorialCanvasIn;
    } 
}


public class SpawnEnemy : MonoBehaviour {
    public Button waveStarterButton;
    public Text waveCounterText;
    public GameObject firstWaypoint;
    public GameObject enemyToSpawn;
    public int numberOfEnemiesToSpawn;

    public Transform winMenu;
    public Wave[] waveSystem;

    private int currentWave = -1;
    private int currentWavePart = 0;
    private GameDataScript gameData;
    private float spawnCoolDown = 0;
    public bool waveInProgress = false;
    private bool speedUp = false;


    public Sprite nextWaveBut;
    public Sprite nextWaveButP;
    public Sprite speedUpBut;
    public Sprite speedUpButP;

	// Use this for initialization
	void Start () {
        UpdateWaveCounterUI();
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
	}
	
	// Update is called once per frame
	void Update () {

        spawnCoolDown -= Time.deltaTime;

        if (waveInProgress)
        {

            if (currentWavePart < waveSystem[currentWave].wave.Length && spawnCoolDown <= 0)
            {
                for (int i = 0; i < waveSystem[currentWave].wave[currentWavePart].numberOfEnemies; i++)
                {
                    GameObject newEnemy = Instantiate(waveSystem[currentWave].wave[currentWavePart].enemyType, new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1)), Quaternion.identity);
                    newEnemy.GetComponent<EnemyNavScript>().ChangeTargetWaypoint(firstWaypoint);
                }
                spawnCoolDown = waveSystem[currentWave].wave[currentWavePart].howLongTowaitAfter;
                currentWavePart++;

            }
        }
        if (currentWave >= 0)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && currentWavePart == waveSystem[currentWave].wave.Length)
            {
                //waveStarterButton.GetComponentInChildren<Text>().text = "Start Next Wave";
                SwapWaveStarterSprite();
                currentWavePart = 0;
                waveInProgress = false;

                if (currentWave == waveSystem.Length-1 && gameData.uniHealth > 0) {
                    
                        winMenu.gameObject.SetActive(true);
                }
            }
        }


    }

    //this is the method called by the button to spawn enemies. it randomizes the position of the spawned enemies, so they don't spawn on top of each other
    public void SpawnEnemies() {
        if (waveInProgress == false && currentWave < waveSystem.Length - 1)
        {
            currentWave++;
            waveInProgress = true;
            UpdateWaveCounterUI();
            //waveStarterButton.GetComponentInChildren<Text>().text = ">>";
            SwapWaveStarterSprite();
            if (waveSystem[currentWave].tutorialbeforeWave != null)
            {
                waveSystem[currentWave].tutorialbeforeWave.gameObject.SetActive(true);
                //Time.timeScale = 0;
                waveInProgress = false;
            }
        }
        else if (speedUp == false) {
            Time.timeScale = 2;
            speedUp = true;
        } else if(speedUp) {
            Time.timeScale = 1;
            speedUp = false;
        }



        //for (int i = 0; i < numberOfEnemiesToSpawn; i++) {
        //    GameObject newEnemy = Instantiate(enemyToSpawn, new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1)), Quaternion.identity);
        //    newEnemy.GetComponent<EnemyNavScript>().ChangeTargetWaypoint(firstWaypoint);
        //}
    }

    private void UpdateWaveCounterUI() {
        int waveNumberToDistplay = currentWave + 1;
            waveCounterText.GetComponent<Text>().text = waveNumberToDistplay.ToString() + " / " + waveSystem.Length.ToString();

    }

    private void SwapWaveStarterSprite() {
        SpriteState speedUpSpriteState = new SpriteState();
        speedUpSpriteState.pressedSprite = speedUpButP;


        SpriteState startWaveSpriteState = new SpriteState();
        startWaveSpriteState.pressedSprite = nextWaveButP;

        if (waveStarterButton.GetComponent<Image>().sprite == nextWaveBut || waveStarterButton.GetComponent<Image>().sprite == nextWaveButP)
        {
            waveStarterButton.GetComponent<Image>().sprite = speedUpBut;
            waveStarterButton.spriteState = speedUpSpriteState;
        }
        else
        {
            waveStarterButton.GetComponent<Image>().sprite = nextWaveBut;
            waveStarterButton.spriteState = startWaveSpriteState;
        }

    }
}
