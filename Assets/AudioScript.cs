using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    private AudioSource aud_s;
    public AudioClip aud_c;

	// Use this for initialization
	void Start () {
        aud_s = GetComponent<AudioSource>();
        aud_s.clip = aud_c;
        aud_s.Play();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
