﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

	public GameObject opponent;
	public AnimationClip attack;
	public AnimationClip dieClip;

	public int maxHealth;
	public int health;
	public int damage;

	private double impactLength;
	public double impactTime;
	public bool impacted;
	public float range;

	bool started;
	bool ended;

	public float combatEscapeTime;
	public float countDown;

	Animation animations;


	void Awake(){
		animations = GetComponent<Animation> ();

	}


	// Use this for initialization
	void Start () {
		impactLength = (animations [attack.name].length * impactTime);
		health = maxHealth;
	}


	// Update is called once per frame
	void Update () {

		Debug.Log (health);

		if (Input.GetKey (KeyCode.Space) && inRange()) {
			animations.CrossFade (attack.name);
			ClickToMove.attack = true;
			if (opponent != null) {
				transform.LookAt (opponent.transform.position);

			}
		}

		if (animations[attack.name].time > 0.9 * animations[attack.name].length) {
			ClickToMove.attack = false;
			impacted = false;

		}
		impact ();
		die ();
	}


	void impact(){
		if (opponent != null && animations.IsPlaying (attack.name) && !impacted) {
			if(animations[attack.name].time > animations[attack.name].length * impactLength &&
				(animations[attack.name].time < 0.9 * animations[attack.name].length)){
				countDown = combatEscapeTime;
				CancelInvoke ("combatEscapeCountDown");
				InvokeRepeating ("combatEscapeCountDown", 0, 1);
				opponent.GetComponent<Mob> ().getHit (damage);
				impacted = true;

			}
		}
	}


	void combatEscapeCountDown(){

		countDown = countDown - 1;
		if(countDown == 0){
			CancelInvoke ("combatEscapeCountDown");

		}
	}


	//if dead returns true or false
	public bool isDead(){
		if (health == 0) {
			return true;

		} else {
			return false;

		}

	}


	void die(){
		if (isDead() && !ended) {
			if (!started) {
				ClickToMove.die = true;
				animations.Play (dieClip.name);
				started = true;

			}

			if (started && !animations.IsPlaying(dieClip.name)){
				//whatever you want to do
				Debug.Log("You have died");
				health = 100;

				ended = true;
				started = false;
				ClickToMove.die = false;
			}

		}
	}
		

	public void getHit(int damage){

		//TODO: later centralize this
		health = health - damage;
		if (health < 0) {
			health = 0;

		}
	}

	bool inRange(){
		if (Vector3.Distance (opponent.transform.position, transform.position) <= range) {
			return true;

		} else {
			return false;

		}
	}
}
