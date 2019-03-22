﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {

	public float speed;
	public CharacterController controller;
	public AnimationClip run;
	public AnimationClip idle;
	private Vector3 position; 
	public static bool attack;

	// Use this for initialization
	void Start () {
		position = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (!attack) {
			//click to move
			if (Input.GetMouseButton (0)) {
				
				//locate where player clicked on the terrain
				locatePosition ();

			}
			//move the player to the position
			moveToPosition ();

		}
	}

	void locatePosition(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, 1000)) {

			//if you're not player, run this
			if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy") {
				//position = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
				position = hit.point;

			}
		}
	}

	void moveToPosition(){

		//Game object is moving
		if (Vector3.Distance (transform.position, position) > 1) {
			Quaternion newRotation = Quaternion.LookRotation (position - transform.position);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 10);
			controller.SimpleMove (transform.forward * speed);
			GetComponent<Animation> ().CrossFade(run.name);

		//Game object is not moving
		} else {
			GetComponent<Animation> ().CrossFade (idle.name);

		}
	}
}
