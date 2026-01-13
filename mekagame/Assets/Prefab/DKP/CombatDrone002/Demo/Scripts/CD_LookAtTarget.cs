using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD_LookAtTarget : MonoBehaviour {

	public Transform LookAtTargetTransform;

	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (LookAtTargetTransform != null) {
			myTransform.LookAt (LookAtTargetTransform.position, Vector3.up);
		}
	}
}
