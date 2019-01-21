using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public Text scoreBoard; 
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        scoreBoard.text = "Score: " + Arrow.score.ToString();
    }
}
