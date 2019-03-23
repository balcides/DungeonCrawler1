using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {

	Fighter player;
	public KeyCode key;
	public double damagePercentage;
	public int stunTime;
	public bool inAction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (key)) {
			player.resetAttackFunction ();
			player.specialAttack = true;
			inAction = true;
		}

		if (inAction) {
			player.attackFunction ();

		}
	}
}
