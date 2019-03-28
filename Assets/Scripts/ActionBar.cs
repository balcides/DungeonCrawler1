using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour {

	public Texture2D actionBar;
	public Rect position;
	public SkillSlot[] skill;
	public float skillX, skillY, skillWidth, skillHeight, skillDistance;

	int keyBindSlot = -1;


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

		skill [0].setKey(KeyCode.Q);
		skill [1].setKey(KeyCode.W);
		skill [2].setKey(KeyCode.E);
		skill [3].setKey(KeyCode.R);
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
		setKeyBindings ();
	}


	void drawActionBar(){

		GUI.DrawTexture (getScreenRect(position), actionBar);

	}


	void drawSkillSlots(){

		for (int count = 0; count < skill.Length; count++) {
			GUI.DrawTexture (getScreenRect(skill[count].position), skill [count].skill.picture);
		}
	}


	void setKeyBindings(){
		for (int count = 0; count < skill.Length; count++) {
			if (Input.GetMouseButtonDown(0) && Event.current.isMouse && getScreenRect(skill [count].position).Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))) {

				if (keyBindSlot == -1) {
					keyBindSlot = count;
					skill [keyBindSlot].skill.activated = false;

				} else {
					keyBindSlot = -1;

				}
			}

		}

		if (keyBindSlot != -1 && Event.current.isKey) {
			skill [keyBindSlot].setKey(Event.current.keyCode);
			skill [keyBindSlot].skill.activated = true;
			keyBindSlot = -1;

		}

	}


	Rect getScreenRect(Rect position){

		return new Rect (Screen.width * position.x, 
				  Screen.height * position.y, 
				  Screen.width * position.width, 
				  Screen.height * position.height);
	}
}
