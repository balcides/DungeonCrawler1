using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

	public float speed;
	public float range;
	public Transform player;
	public LevelSystem playerLevel;
	private Fighter opponent;

	public CharacterController controller;
	public AnimationClip die;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip attackClip;

	public double impactTime = 0.35f; //unify this later

	public int maxHealth;
	public int health;
	public int damage;

	private bool impacted;

	Animation animations;

	void Awake(){
		animations = GetComponent<Animation> ();

	}


	// Use this for initialization
	void Start () {

		health = maxHealth;
		opponent = player.GetComponent<Fighter> ();
	}


	// Update is called once per frame
	void Update () {
		if (!isDead ()) {
			if (!inRange ()) {
				chase ();

			} else {
				animations.Play(attackClip.name);
				attack ();

				//TODO: centralize this function
				if (animations [attackClip.name].time > (0.9f * animations [attackClip.name].length)) {
					impacted = false;

				}

			}
		} else {
			dieMethod ();

		}
	}

	void attack(){

		//TODO: later make this a centralized function
		if (animations [attackClip.name].time > (animations [attackClip.name].length * impactTime) && 
			!impacted && animations[attackClip.name].time < 0.9 * animations[attackClip.name].length) {
			opponent.getHit (damage);
			impacted = true;

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
		animations.Play (die.name);
		if (animations[die.name].time > animations [die.name].length * 0.9) {
			playerLevel.exp = playerLevel.exp + 100;
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
