using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript : MonoBehaviour {

    public GameObject theSun;
    public Vector3 axisToRotateAround = Vector3.up;
    public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(theSun.transform.position, axisToRotateAround, Time.deltaTime * speed);
	}
}
