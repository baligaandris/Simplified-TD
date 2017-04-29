using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvasScript : MonoBehaviour {
    public Text grade;
    public Text teachersNote;
    private GameDataScript GameData;

	// Use this for initialization
	void Start () {
        GameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
        if (GameData.uniHealth <= 0)
        {
            grade.GetComponent<Text>().text = "F";
            teachersNote.GetComponent<Text>().text = "See me after class";
        }
        else if (GameData.uniHealth <= 5)
        {
            grade.GetComponent<Text>().text = "D";
            teachersNote.GetComponent<Text>().text = "You should put some more work in.";
        }
        else if (GameData.uniHealth <= 10) {
            grade.GetComponent<Text>().text = "C";
            teachersNote.GetComponent<Text>().text = "Satisfactory.";
        }
        else if (GameData.uniHealth <= 15)
        {
            grade.GetComponent<Text>().text = "B";
            teachersNote.GetComponent<Text>().text = "Not bad!";
        }
        else if (GameData.uniHealth <= 19)
        {
            grade.GetComponent<Text>().text = "A";
            teachersNote.GetComponent<Text>().text = "Good Job!";
        }
        else if (GameData.uniHealth == 20)
        {
            grade.GetComponent<Text>().text = "A+";
            teachersNote.GetComponent<Text>().text = "Excellent!";
        }
    }
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
