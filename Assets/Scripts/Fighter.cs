using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

	public GameObject opponent;
	public AnimationClip attack;

	public int damage;
	public double impactLength;
	public bool impacted;
	public float range;

	Animation animations;

	void Awake(){
		animations = GetComponent<Animation> ();

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
	}

	void impact(){
		if (opponent != null && animations.IsPlaying (attack.name) && !impacted) {
			if(animations[attack.name].time > animations[attack.name].length * impactLength &&
				(animations[attack.name].time < 0.9 * animations[attack.name].length)){
				opponent.GetComponent<Mob> ().getHit (damage);
				impacted = true;

			}
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
