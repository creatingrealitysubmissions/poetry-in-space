﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGazeControl : MonoBehaviour {
    public GameObject head;
    public float offsetLandingZPosition;
    float gazeTimer;
    GameObject currentLocation;
    public GameObject objectBeingLookAt;
    public AudioSource sfx_source;

    public float MAX_GAZE_TIMER = 5f;
    public float lerpSpeed = 1.0f;
	// Use this for initialization
	void Start () {
        sfx_source = GetComponent<AudioSource>();
        gazeTimer = MAX_GAZE_TIMER;
	}

    void FixedUpdate()
    {
        Vector3 fwd = head.transform.TransformDirection(Vector3.forward);

        //Debug.DrawLine(head.transform.position, head.transform.position + fwd * 50f, Color.red, 1f);
        Ray ray = new Ray(head.transform.position, fwd);
        RaycastHit hitInfo;
        
		if (Physics.Raycast(ray, out hitInfo, 10f))
        {
            Debug.Log("step 1");

            if(currentLocation == null || hitInfo.collider.gameObject != currentLocation)
            {
                objectBeingLookAt = hitInfo.collider.gameObject;
                Debug.Log("step 2");
                if (hitInfo.collider.tag == "GazeTeleport")
                {
                    Debug.Log("step 3");
                    if (gazeTimer > 0f)//david changed
                    {
                        sfx_source.Play();
                        currentLocation = hitInfo.collider.gameObject;
                        //transform.position = hitInfo.collider.transform.position - head.transform.localPosition;
                        StartCoroutine(MoveTo(hitInfo.collider.transform.position - head.transform.localPosition));
                        gazeTimer = MAX_GAZE_TIMER;
                    }
                }
                
                else
                {
                    gazeTimer = MAX_GAZE_TIMER;
                }
            }
            
        }
          
    }

    IEnumerator MoveTo(Vector3 target)
    {

        yield return new WaitForSeconds(.5f);
        Vector3 startPostion = transform.position;
        float i = 0f;
        while(i < 1f)
        {
            i += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(startPostion, target, i);
            yield return null;
        }
    }
}
