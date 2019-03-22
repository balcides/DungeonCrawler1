using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

	public float speed;
	public float range;
	public Transform player;
	public CharacterController controller;
	public AnimationClip die;
	public AnimationClip run;
	public AnimationClip idle;
	public int health;

	Animation animations;

	void Awake(){
		animations = GetComponent<Animation> ();

	}


	// Use this for initialization
	void Start () {
		health = 100;

	}


	// Update is called once per frame
	void Update () {
		Debug.Log (health);

		if (!isDead ()) {
			if (!inRange ()) {
				chase ();

			} else {
				animations.CrossFade (idle.name);

			}
		} else {
			dieMethod ();

		}
	}


	bool inRange(){
		if (Vector3.Distance (transform.position, player.position) < range) { return true;
		} else { return false; }

	}


	void chase(){
		transform.LookAt (player.position);
		controller.SimpleMove (transform.forward * speed);
		animations.CrossFade(run.name);

	}


	void  dieMethod(){
		animations = GetComponent<Animation> ();
		animations.Play (die.name);
		if (animations[die.name].time > animations [die.name].length * 0.9) {
			Destroy (gameObject);

		}
	}


	public void getHit(int damage){
		health = health - damage;
		if (health < 0) {
			health = 0;

		}

	}


	bool isDead(){
		if (health <= 0) {
			return true;

		} else {
			return false;

		}
	}


	void OnMouseOver(){
		player.GetComponent<Fighter> ().opponent = gameObject;

	}
}
