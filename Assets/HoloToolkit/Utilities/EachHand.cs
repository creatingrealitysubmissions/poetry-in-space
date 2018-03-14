using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EachHand : MonoBehaviour {

    Vector3 offset;
    public GameObject selectedGameObject;
    public SimpleGazeControl gazeControl;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		/*if (WSA.Input.selectPressed)
        {
            Debug.Log("R_Trriger held..");
            selectedGameObject.transform.position = transform.position + offset;
        }*/
        if (Input.GetButtonDown("R_Trigger"))
        {
            Debug.Log("R_Trigger down,");
            //if you are looking at an object
            if (gazeControl.objectBeingLookAt.tag == "Planet")
            {
                Debug.Log("Planet");
                selectedGameObject = gazeControl.objectBeingLookAt;
                offset = transform.position - selectedGameObject.transform.position;          
            }
        }
	}
}
