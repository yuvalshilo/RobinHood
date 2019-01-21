using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StartBanner : MonoBehaviour {

    public Text banner;
    private TimeSpan timeLeft;
    private float animationTime = 1.5f;

    // Use this for initialization
    void Start () {
        banner.enabled = false;
        ResetTime();
    }
	
    public void ShowBanner()
    {
        if (!banner.enabled)
        {
            ResetTime();
            banner.enabled = true;
        }
    }

    public void ResetTime()
    {
        timeLeft = System.TimeSpan.FromSeconds(animationTime);
    }

	// Update is called once per frame
	void Update () {
        if (timeLeft.Seconds < 0)
        {
            banner.enabled = false;
            return;
        }
        timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
    }
}
