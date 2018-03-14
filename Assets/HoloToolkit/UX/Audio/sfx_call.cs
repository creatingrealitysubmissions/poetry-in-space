using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_call : MonoBehaviour {

    public AudioSource sfx;

    // Use this for initialization
    void Start() { 
            sfx = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void playSfx () {
        sfx.Play();
    }
}
