﻿using UnityEngine;
using System.Collections;

public class SmallMap : MonoBehaviour {

	private Transform target;

	private float preserveY;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		preserveY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Update");
		this.transform.position = new Vector3(target.position.x, preserveY, target.position.z);
	}
}
