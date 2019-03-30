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

	private Item temp;
	private Vector2 selected, secondSelected;
	private bool test;

	public KeyCode key;
	public bool displayInventory = false;
	public float GUIwait, GUIwaitStart;


	// Use this for initialization
	void Start () {
		setSlots ();
		test = false;
		GUIwait = 0f;
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
		//addItem (0,0,Items.getArmor(0));
		test = true;
	}


	// Update is called once per frame
	void Update () {

		if (!test) {
			testMethod ();
		}

		GUIwait -= Time.deltaTime;

		if (GUIwait <= 0) {
			GUIwait = 0;
		}



		if (Input.GetKey (key) && !displayInventory && GUIwait == 0) {
			displayInventory = true;
			GUIwait = GUIwaitStart;
			ClickToMove.busy = true;

		} else if (Input.GetKey (key) && displayInventory && GUIwait == 0) {
			displayInventory = false;
			GUIwait = GUIwaitStart;
			ClickToMove.busy = false;
		}

	}





	void OnGUI(){

		drawSlots ();
		drawItems ();
		detectGUIAction ();
		drawTempItem ();


		//on key down, activate inventory
		if (displayInventory) {
			drawInventory ();

		} else {
			

		}

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
			GUI.DrawTexture(new Rect(4 + slotX + position.x + items[count].x * width, 
									 4 + slotY + position.y + items[count].y * height, 
													      items[count].width * width - 8, 
									     				  items[count].height * height - 8), 
														  items[count].image);
		}
	}


	void drawTempItem(){
		if (temp != null) {
			GUI.DrawTexture(new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, temp.width * width, temp.height * height), temp.image);

		}
	}


	public bool addItems(Item item){
		for (int x = 0; x < slotWidthSize; x++) {
			for (int y = 0; y < slotHeightSize; y++) {
				if(addItem(x,y, item)){
					return true;

				}

			}
		}
		return false;
	}


	private bool addItem(int x, int y, Item item){
		for (int sX = 0; sX < item.width; sX++) {
			for (int sY = 0; sY < item.height; sY++) {
				if (slots [x, y].occupied) {
					Debug.Log ("breaks" + x + " , " + y);
					return false;
				}
			}
		}

		if (x + item.width > slotWidthSize) {
			Debug.Log ("Out of X bounds");
			return false;

		}else if (y + item.height > slotHeightSize){
			Debug.Log ("Out of Y bounds");
			return false;

		}
		Debug.Log ("comes" + x + " , " + y);

		item.x = x;
		item.y = y;
		items.Add (item);

		for (int sX = x; sX < item.width + x; sX++) {
			for (int sY = y; sY < item.height + y; sY++) {
				slots [sX, sY].occupied = true;
			}
		}

		return true;
	}


	void detectGUIAction(){
		if (Input.mousePosition.x > position.x && Input.mousePosition.x < position.x + position.width && displayInventory) {
			if (Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height) {
				detectMouseAction ();
				ClickToMove.busy = true;
				return;
			}
		}
		ClickToMove.busy = false;
	}


	void removeItem(Item item){
		for (int x = item.x; x < item.x + item.width; x++) {
			for (int y = item.y; y < item.y + item.height; y++) {
				slots [x, y].occupied = false;

			}
		}
		items.Remove (item);
	}


	void detectMouseAction(){
		for (int x = 0; x < slotWidthSize; x++) {
			for (int y = 0; y < slotHeightSize; y++) {
				Rect slot = new Rect (position.x + slots[x,y].position.x, position.y + slots[x,y].position.y, width, height);
				if(slot.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
					if (Event.current.isMouse && Input.GetMouseButtonDown (0)) {
						selected.x = x;
						selected.y = y;
						for (int index = 0; index < items.Count; index++) {
							for(int countX = items[index].x; countX < items[index].x + items[index].width; countX++){
								for(int countY = items[index].y; countY < items[index].y + items[index].width; countY++){
									if (countX == x && countY == y) {
										temp = items [index];
										removeItem (temp);
										return;
							
									}
								}
							}
						}
				
					}else if(Event.current.isMouse && Input.GetMouseButtonUp(0)){
						secondSelected.x = x;
						secondSelected.y = y;

						//checking drag and drop coordinate
						if (secondSelected.x != selected.x || secondSelected.y != selected.y) {
							if (temp != null) {
								if (addItem ((int)secondSelected.x, (int)secondSelected.y, temp)) {
									
								} else {
									addItem (temp.x, temp.y, temp);

								}
								temp = null;
							}
						} else {
							addItem (temp.x, temp.y, temp);
							temp = null;

						}
					}
					//Debug.Log (selected + "     " + secondSelected);
					return;
				}
			}
		}
	}


	void drawInventory(){
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture (position, image);
	}
}
