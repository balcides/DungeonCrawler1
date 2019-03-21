using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

	public float speed;
	public float range;
	public Transform player;
	public CharacterController controller;

	public AnimationClip run;
	public AnimationClip idle;

	public int health;

	// Use this for initialization
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {

		if (!inRange ()) {
			chase ();
		} else {
			GetComponent<Animation> ().CrossFade (idle.name);
		}

		Debug.Log (health);
	}

	bool inRange(){

		if (Vector3.Distance (transform.position, player.position) < range) { return true;
		} else { return false; }
	}

	void chase(){
		
		transform.LookAt (player.position);
		controller.SimpleMove (transform.forward * speed);
		GetComponent<Animation> ().CrossFade(run.name);
	}


	public void getHit(int damage){

		health = health - damage;
		if (health < 0) {

			health = 0;
		}

	}

	void OnMouseOver(){

		player.GetComponent<Fighter> ().opponent = gameObject;
	}
}
