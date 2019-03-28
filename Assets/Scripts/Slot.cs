using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Slot {

	public Item item;
	public bool occupied;
	public Rect position;

	public Texture2D test;

	//public Slot(Rect position = null){
	public Slot(){


	}

	void draw(){
		GUI.DrawTexture (position, item.image);
	}
}
