using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target1Control : MonoBehaviour {

    public float speed;
    private int current;
    private float originX = 0f;
    private int direction;
    bool upgradedLevel = false;
    public float xMove;
    public bool showBanner;

    public StartBanner banner;

    public int levelUpgrade = 3;

	// Use this for initialization
	void Start () {
        originX = transform.position.x;
        direction = -1;
    }

    public void Hit()
    {
        if (showBanner)
        {
            banner.ShowBanner();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.ToString().Equals("Arrow (UnityEngine.BoxCollider)"))
        {
            Hit(); // tododoodo
        }
    }

    // Update is called once per frame
    void Update () {
    upgradedLevel = Arrow.score >= levelUpgrade && levelUpgrade >= 0;
    if (upgradedLevel)
    {
        // init vars
        float xFinal = originX + direction * xMove;

        // move target
        float dX = Mathf.Abs(xFinal - transform.position.x);
        if (dX > 0.01f)
        {
            transform.position = new Vector3(transform.position.x + direction * Time.deltaTime * speed, transform.position.y, transform.position.z);
        }else
        {
            direction = -direction;
        }
    }

    }
}
