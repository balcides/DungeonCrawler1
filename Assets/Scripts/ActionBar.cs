using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour {

	public Texture2D actionBar;
	public Rect position;
	public SkillSlot[] skill;
	public float skillX, skillY, skillWidth, skillHeight, skillDistance;


	// Use this for initialization
	void Start () {
		initialize ();
	}


	void initialize(){
		SpecialAttack[] attacks = GameObject.FindGameObjectWithTag ("Player").GetComponents<SpecialAttack> ();

		skill = new SkillSlot[attacks.Length];

		for (int count = 0; count < attacks.Length; count++) {
			skill [count] = new SkillSlot ();
			skill [count].skill = attacks [count];
		}

		skill [0].key = KeyCode.Q;
		skill [1].key = KeyCode.W;
		skill [2].key = KeyCode.E;
		//skill [3].key = KeyCode.R;
	}


	// Update is called once per frame
	void Update () {
		updateSkillSlots ();
	}


	void updateSkillSlots(){
		for (int count = 0; count < skill.Length; count++) {
			skill [count].position.Set (skillX + count * (skillWidth + skillDistance), skillY, skillWidth, skillHeight);

		}
	}


	void OnGUI(){

		drawActionBar ();
		drawSkillSlots ();
	}


	void drawActionBar(){

		GUI.DrawTexture (getScreenRect(position), actionBar);

	}


	void drawSkillSlots(){

		for (int count = 0; count < skill.Length; count++) {
			GUI.DrawTexture (getScreenRect(skill[count].position), skill [count].skill.picture);
		}
	}


	Rect getScreenRect(Rect position){

		return new Rect (Screen.width * position.x, 
				  Screen.height * position.y, 
				  Screen.width * position.width, 
				  Screen.height * position.height);
	}
}
