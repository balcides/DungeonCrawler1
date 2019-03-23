using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {

	public Fighter player;
	public KeyCode key;
	public double damagePercentage;
	public int stunTime;
	public bool inAction;
	public GameObject particleEffect;
	public int projectile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (key) && !player.specialAttack) {
			player.resetAttackFunction ();
			player.specialAttack = true;
			inAction = true;

		}

		if (inAction) {
			if (player.attackFunction (stunTime, damagePercentage, key, particleEffect, projectile)) {


			} else {
				inAction = false;

			}
		}


	}
}
