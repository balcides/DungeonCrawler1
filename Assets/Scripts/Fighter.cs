using System.Collections;
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
	public bool inAction;

	bool started;
	bool ended;

	public float combatEscapeTime;
	public float countDown;

	public bool specialAttack;

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

		if (Input.GetKeyDown (KeyCode.Space) && !specialAttack) {
			inAction = true;

		}

		if (inAction) {
			if (attackFunction (0, 1, KeyCode.Space, null, 0, true)) {
				
			} else {
				inAction = false;

			}
		}
		die ();
	}


	public bool attackFunction(int stunSeconds, double scaledDamage, KeyCode key, GameObject particleEffect, int projectile, bool opponentBased){

		if (opponentBased) {
			if (Input.GetKey (key) && inRange ()) {
				animations.CrossFade (attack.name);
				ClickToMove.attack = true;
				if (opponent != null) {
					transform.LookAt (opponent.transform.position);

				}
			}
		} else {
			if (Input.GetKey (key)) {
				animations.Play (attack.name);
				ClickToMove.attack = true;
				transform.LookAt (ClickToMove.cursorPosition);

			}
		}
			
		if (animations[attack.name].time > 0.9 * animations[attack.name].length) {
			ClickToMove.attack = false;
			impacted = false;

			if (specialAttack) {
				specialAttack = false;
			}

			return false;

		}
		impact (stunSeconds, scaledDamage, particleEffect, projectile, opponentBased);
		return true;
	}


	public void resetAttackFunction(){

		ClickToMove.attack = false;
		impacted = false;
		animations.Stop (attack.name);
		
	}


	void impact(int stunSeconds, double scaledDamage, GameObject particleEffect, int projectile, bool opponentBased){
		if (!opponentBased || opponent != null && animations.IsPlaying (attack.name) && !impacted) {
			if(animations[attack.name].time > animations[attack.name].length * impactLength &&
				(animations[attack.name].time < 0.9 * animations[attack.name].length)){
				countDown = combatEscapeTime;
				CancelInvoke ("combatEscapeCountDown");
				InvokeRepeating ("combatEscapeCountDown", 0, 1);

				if (opponentBased) {
					opponent.GetComponent<Mob> ().getHit (damage * scaledDamage);
					opponent.GetComponent<Mob> ().getStun (stunSeconds);
				}
					
				//send out spheres
				Quaternion rot = transform.rotation;
				rot.x = 0f;
				rot.z = 0f;

				if (projectile > 0) {
					//shoot projectiles
					Instantiate(Resources.Load("Projectile"), new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), rot);
				}

				//send particles
				if (particleEffect != null) {
					Instantiate (particleEffect, new Vector3 (opponent.transform.position.x, opponent.transform.position.y + 1.5f, opponent.transform.position.z), Quaternion.identity);
				}
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
		if (opponent != null && Vector3.Distance (opponent.transform.position, transform.position) <= range) {
			return true;

		} else {
			return false;

		}
	}
}
