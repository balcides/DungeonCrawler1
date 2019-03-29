using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item {

	public Texture2D image;
	public int x, y, width, height;

	public abstract void performAction ();



}
