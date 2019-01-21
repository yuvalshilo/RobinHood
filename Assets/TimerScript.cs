using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerScript : MonoBehaviour {

    public float roundTimeInMinutes;
    private TimeSpan timeLeft;
    public Text timer;
    public Text gameover;
    public Text retrytext;
    public Button retry;
    public GameObject controller1;
    public GameObject controller2;

    // Use this for initialization
    void Start () {
        ResetTime();
        if (gameover != null)
        {
            gameover.enabled = false;
            retry.image.enabled = false;
            retrytext.enabled = false;
        }
        controller1.active = true;
        controller2.active = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (timeLeft.Seconds < 0)
        {
            GameOver();
            return;
        }
        timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
        if (timer != null) timer.text = String.Format("0{0}:0{1}:{2}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
    }

    public void ResetTime()
    {
        timeLeft = System.TimeSpan.FromMinutes(roundTimeInMinutes);
    }

    public void GameOver()
    {
        System.Console.WriteLine("Game Over !");
        if (gameover != null)
        {
            gameover.enabled = true;
            retry.image.enabled = true;
            retrytext.enabled = true;
        } else
        {
            Application.LoadLevel(0);
        }
        controller1.active = false;
        controller2.active = false;
    }
}
