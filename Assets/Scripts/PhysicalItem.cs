using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : MonoBehaviour {

	public static Inventory inventory;
	public GameObject gui;
	public int itemRep;

	
	// Update is called once per frame
	void Update () {

	}


	void OnMouseDown(){
		Debug.Log ("Pick me up!");
		inventory = gui.GetComponent<Inventory> ();
		inventory.addItems (Items.getArmor(itemRep));
		Destroy (gameObject);
	}
}
