﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SkillSlot {

	public SpecialAttack skill;
	public Rect position;

	public void setKey(KeyCode keyCode){
		
		if (skill != null) {
			skill.key = keyCode;
		}
	}
}
