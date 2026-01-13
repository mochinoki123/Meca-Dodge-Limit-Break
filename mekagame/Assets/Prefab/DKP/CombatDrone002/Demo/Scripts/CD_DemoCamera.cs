using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD_DemoCamera : MonoBehaviour {

	public KeyCode SpinCameraLeftKey = KeyCode.A;
	public KeyCode SpinCameraRightKey = KeyCode.D;

	public float SpinSpeed = 2.0f;

	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (SpinCameraLeftKey)) {
			myTransform.RotateAround (myTransform.position, myTransform.up, SpinSpeed * Time.deltaTime);
		}

		if (Input.GetKey (SpinCameraRightKey)) {
			myTransform.RotateAround (myTransform.position, myTransform.up, -(SpinSpeed * Time.deltaTime));
		}

	}
}
