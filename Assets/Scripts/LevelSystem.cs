using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour {

	//we need 100 exp for each level up

	public int level;
	public int exp;
	public Fighter player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		LevelUp ();

	}

	void LevelUp(){

		//100 * level
		if (exp >= Mathf.Pow(level,2) + 100) {
			level = level + 1;
			exp = exp - (int)(Mathf.Pow(level,2) + 100);
			levelEffect ();
		}
	}


	void levelEffect(){
		player.maxHealth = player.maxHealth + 100;
		player.damage = player.damage + (int)(Mathf.Pow(level,2) + 100);
		player.health = player.maxHealth;
	}


}
