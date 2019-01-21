using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private bool isAttached = false;

    private bool isFired = false;
    public static int score = 0;
    public Text gameScore;
    public bool showAllWow;
    public bool allSameScore;
    public AudioClip hitSound;

    public StartBanner wow_banner;

    void OnTriggerStay()
    {
        //AttachArrow();
    }

    private void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.ToString().Equals("Golden Bow (UnityEngine.BoxCollider)"))
        {
            AttachArrow();
        }
        else
        {
            ArrowManager.aSrc.clip = hitSound;
            ArrowManager.aSrc.Play();
            colideSet();
            transform.parent = other.transform;
            int addedScore = 0;
            if (showAllWow)
            {
                wow_banner.ShowBanner();
            }
            if (other.ToString().Contains("SCORE1") || allSameScore)
            {
                addedScore = 1;
            }
            else if (other.ToString().Contains("SCORE2"))
            {
                addedScore = 2;
            }
            else if (other.ToString().Contains("SCORE3"))
            {
                addedScore = 3;
                if (!showAllWow)
                {
                    wow_banner.ShowBanner();
                }
            }
            else if (other.ToString().Contains("SCORE4"))
            {
                addedScore = 4;
                if (!showAllWow)
                {
                    wow_banner.ShowBanner();
                }
            }
            score += addedScore;
          
        }
    }

    void Update()
    {
        if (isFired && transform.GetComponent<Rigidbody>().velocity.magnitude > 5f)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    private void colideSet()
    {
    
        Rigidbody r = transform.GetComponent<Rigidbody>();
        r.velocity = new Vector3(0f, 0f, 0f);
        r.useGravity = false;
        r.isKinematic = false;
        r.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        r.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        r.collisionDetectionMode = CollisionDetectionMode.Discrete;
        
    }

    public void Fired()
    {
        isFired = true;
        isAttached = false;
    }

    private void AttachArrow()
    {
        var device = SteamVR_Controller.Input((int)ArrowManager.Instance.trackObj.index);
        if (!isAttached /*&& device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)*/)
        {
            ArrowManager.Instance.AttachBowToArrow();
            isAttached = true;
        }
    }


}
