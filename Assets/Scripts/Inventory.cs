using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public Texture2D image;
	public Rect position;

	public List<Item> items = new List<Item>();
	int slotWidthSize = 10;
	int slotHeightSize = 4;
	public Slot[,] slots;
	public int slotX, slotY;

	// Use this for initialization
	void Start () {
		setSlots ();
	}


	void setSlots(){

		slots = new Slot[slotWidthSize, slotHeightSize];
		for (int x = 0; x < slotWidthSize; x++) {
			Debug.Log ("sdfs");

			for (int y = 0; y < slotHeightSize; y++) {
				slots [x, y] = new Slot ();
			}
		}
	}


	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){

		drawInventory ();
	}

	void drawInventory(){

		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture (position, image);
	}
}
