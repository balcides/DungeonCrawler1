using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public Texture2D image;
	public Rect position;

	public List<Item> items = new List<Item>();
	int slotWidthSize = 10;
	int slotHeightSize = 6;
	public Slot[,] slots;

	public int slotX, slotY;
	public int width = 29;
	public int height = 29;

	private bool test;


	// Use this for initialization
	void Start () {
		
		setSlots ();
		test = false;


	}


	void setSlots(){

		slots = new Slot[slotWidthSize, slotHeightSize];
		for (int x = 0; x < slotWidthSize; x++) {

			for (int y = 0; y < slotHeightSize; y++) {
				slots [x, y] = new Slot (new Rect(slotX + width * x, slotY + height * y, width, height));
			}
		}
	}


	void testMethod(){
		addItem (0,0,Items.armor[0]);
		addItem (1,1,Items.armor[0]);
		test = true;
	}


	// Update is called once per frame
	void Update () {

		if (!test) {
			testMethod ();
		}

	}


	void OnGUI(){

		drawInventory ();
		drawSlots ();
		drawItems ();
	}


	void drawSlots(){
		for (int x = 0; x < slotWidthSize; x++) {

			for (int y = 0; y < slotHeightSize; y++) {
				slots [x, y].draw(position.x, position.y);
			}
		}
	}


	void drawItems(){
		for (int count = 0; count < items.Count; count++) {
			GUI.DrawTexture(new Rect(slotX + position.x + items[count].x * width, 
									 slotY + position.y + items[count].y * height, 
													      items[count].width * width, 
									     				  items[count].height * height), 
														  items[count].image);
		}
	}


	void addItem(int x, int y, Item item){
		for (int sX = 0; sX < item.width; sX++) {
			for (int sY = 0; sY < item.height; sY++) {
				if (slots [x, y].occupied) {
					Debug.Log ("breaks" + x + " , " + y);
					return;
				}
			}
		}
		Debug.Log ("comes" + x + " , " + y);
		items.Add (item);

		for (int sX = x; sX < item.width + x; sX++) {
			for (int sY = y; sY < item.height + y; sY++) {
				slots [sX, sY].occupied = true;
			}
		}

	}


	void drawInventory(){

		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture (position, image);
	}
}
