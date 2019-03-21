using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

	public GameObject opponent;

	public AnimationClip attack;

	public int damage;
	public double impactTime;
	public bool impacted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Space)) {

			GetComponent<Animation> ().CrossFade (attack.name);
			ClickToMove.attack = true;

			if (opponent != null) {
				
				transform.LookAt (opponent.transform.position);
			}
		}

		if (!GetComponent<Animation> ().IsPlaying (attack.name)) {

			ClickToMove.attack = false;
			impacted = false;
		}

		impact ();
	}

	void impact(){

		if (opponent != null && GetComponent<Animation> ().IsPlaying (attack.name) && !impacted) {

			if(GetComponent<Animation> ()[attack.name].time > GetComponent<Animation> ()[attack.name].length * impactTime){
				Debug.Log ("set:" +  GetComponent<Animation> ().IsPlaying (attack.name));
				opponent.GetComponent<Mob> ().getHit (damage);
				impacted = true;
				Debug.Log (GetComponent<Animation> () [attack.name].time);
			}
		}
	}
}
