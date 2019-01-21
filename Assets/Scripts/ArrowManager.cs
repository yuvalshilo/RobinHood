using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ArrowManager : MonoBehaviour {
    private GameObject currentArrow;
    private Queue<GameObject> previosArrows = new Queue<GameObject>(5);
    public SteamVR_TrackedObject trackObj;

    public GameObject arrowPrefab;

    public static ArrowManager Instance;

    public GameObject arrowStartPoint;

    static public AudioSource aSrc;
    public AudioClip shootSound;

    public GameObject stringAttachPoint;
    public GameObject stringStartPoint;

    private bool isAttached = false;

    private void Awake()
    {
        aSrc = GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        AttachArrow();
        PullString();
    }

    private void PullString()
    {
        if (isAttached)
        {
            float dist = (stringStartPoint.transform.position - trackObj.transform.position).magnitude;
            stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(dist * 5, 0f, 0f);
            var device = SteamVR_Controller.Input((int)trackObj.index);
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Fire();   
            }
        }
    }

    private void AttachArrow()
    {
        if (currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab);
            currentArrow.transform.parent = trackObj.transform;
            currentArrow.transform.localPosition = new Vector3(0f, 0f, 0.5f /* Z distance from controller */ );
            currentArrow.transform.rotation = trackObj.transform.rotation;

        }
    }
    public void AttachBowToArrow()
    {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.position = arrowStartPoint.transform.position;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        isAttached = true;
    }

    private void Fire()
    {
        float currDist = (stringStartPoint.transform.position - trackObj.transform.position).magnitude;
        if(previosArrows.Count >= 5)
        {
            Destroy(previosArrows.Dequeue());
        }
        currentArrow.GetComponent<Arrow> ().Fired ();

        const float fireConst = 35f;

        currentArrow.transform.parent = null;
        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * fireConst * currDist; // should be by dist.
        r.useGravity = true;

        currentArrow.GetComponent<Collider>().isTrigger = false;
        
        stringAttachPoint.transform.position = stringStartPoint.transform.position;
        previosArrows.Enqueue(currentArrow);
        currentArrow = null;
        isAttached = false;

        aSrc.clip = shootSound;
        aSrc.Play();
    }
}
