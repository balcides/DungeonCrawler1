using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyHealth : MonoBehaviour {

	public Fighter player;
	public Texture2D frame;
	public Rect framePosition;

	public float horizontalDistance;
	public float verticalDistance;
	public float width;
	public float height;

	public Texture2D healthBar;
	public Rect healthBarPosition;

	public Mob target;
	public float healthPercentage;

	//custom
	public Vector2 healthBarSize = new Vector2 (500, 50);
	Vector2 screenRez = new Vector2 (1920, 1080);


	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {

		if (player.opponent != null) {
			target = player.opponent.GetComponent<Mob> ();
			healthPercentage = (float)target.health / (float)target.maxHealth;

		} else {
			target = null;
			healthPercentage = 0;

		}
	}


	void OnGUI(){
		if (target != null) {
			drawFrame ();
			drawBar ();

		}
	}


	void drawFrame(){
		framePosition.x = (Screen.width - framePosition.width) / 2;
		float width = (healthBarSize.x / screenRez.x);
		float height = (healthBarSize.y / screenRez.y);
		framePosition.width = Screen.width * width;
		framePosition.height = Screen.height * height;
		GUI.DrawTexture (framePosition, frame);

	}


	void drawBar(){
		healthBarPosition.x = framePosition.x + framePosition.width * horizontalDistance;
		healthBarPosition.y = framePosition.y + framePosition.height * verticalDistance;
		healthBarPosition.width = framePosition.width * width * healthPercentage;
		healthBarPosition.height = framePosition.height * height;

		GUI.DrawTexture (healthBarPosition, healthBar);
	}

}
