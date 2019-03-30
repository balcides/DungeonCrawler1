using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour {

	int interval = 120; //in terms of frames
	int count;


	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
	}


	// Update is called once per frame
	void Update () {
		
		if(count == interval){

			Debug.Log ("Save");
			savePosition ();
			//save
			count = 0;
			
		}
		count++;
	}


	void savePosition(){

		PlayerPrefs.SetFloat ("x", ClickToMove.currentPosition.x);
		PlayerPrefs.SetFloat ("y", ClickToMove.currentPosition.y);
		PlayerPrefs.SetFloat ("z", ClickToMove.currentPosition.z);
	}


	public static Vector3 readPlayerPosition(){
		
		Vector3 position = new Vector3 ();
		position.x = PlayerPrefs.GetFloat ("x");
		position.y = PlayerPrefs.GetFloat ("y");
		position.z = PlayerPrefs.GetFloat ("z");

		return position;
	}


	public static void saveMobHealth(int id, int health){
		PlayerPrefs.SetInt ("MobHealth" + id, health);


	}


	public static int readMobHealth(int id){

		if (PlayerPrefs.HasKey ("MobHealth" + id)) {
			return PlayerPrefs.GetInt ("MobHealth" + id);

		} else {
			return -1;
		}


	}
}
