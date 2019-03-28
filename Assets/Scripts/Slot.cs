﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Slot {

	public Item item;
	public bool occupied;
	public Rect position;

	public static Texture2D test;

	public Slot(Rect position){
		this.position = position;
	}

	public void draw(float frameX, float frameY){
		if (item != null) {
			GUI.DrawTexture (new Rect (frameX + position.x, frameY + position.y, position.width, position.height), item.image);
		}

	}
}
