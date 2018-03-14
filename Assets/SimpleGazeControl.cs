using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGazeControl : MonoBehaviour {
    public GameObject head;
    public float offsetLandingZPosition;
    float gazeTimer;
    GameObject currentLocation;

    public float MAX_GAZE_TIMER = 5f;
    public float lerpSpeed = 1f;
	// Use this for initialization
	void Start () {
        gazeTimer = MAX_GAZE_TIMER;
	}

    void FixedUpdate()
    {
        Vector3 fwd = head.transform.TransformDirection(Vector3.forward);

        //Debug.DrawLine(head.transform.position, head.transform.position + fwd * 50f, Color.red, 1f);
        Ray ray = new Ray(head.transform.position, fwd);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, 50f))
        {
            print("There is something in front of the object!" + hitInfo.collider.name);
            if(currentLocation == null || hitInfo.collider.gameObject != currentLocation)
            {
                if (hitInfo.collider.tag == "GazeTeleport")
                {
                    print("timer is: " + gazeTimer);
                    gazeTimer -= Time.fixedDeltaTime;
                    if (gazeTimer < 0f)
                    {
                        if(currentLocation != null)
                        {
                            foreach (Transform t in currentLocation.transform)
                            {
                                if (t.gameObject.tag == "path")
                                    t.gameObject.SetActive(false);
                            }
                        }
                        currentLocation = hitInfo.collider.gameObject;
                        foreach (Transform t in currentLocation.transform)
                        {
                            if (t.gameObject.tag == "path")
                                t.gameObject.SetActive(true);
                        }
                        print("START TELEPORTING!");
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
