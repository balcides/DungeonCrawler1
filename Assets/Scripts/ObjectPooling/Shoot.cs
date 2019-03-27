using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public Pool pool;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C)) {
			pool.activate (1, new Vector3(1.3f,0f,0.1f), Quaternion.identity);

		}else if(Input.GetKeyDown (KeyCode.S)){
			pool.activate (2, new Vector3(1.3f,0f,0.1f), Quaternion.identity);

		}else if(Input.GetKeyDown (KeyCode.A)){
			pool.activate (0, new Vector3(1.3f,0f,0.1f), Quaternion.identity);

		}else if(Input.GetKeyDown (KeyCode.F)){
			pool.activate (3, new Vector3(1.3f,0f,0.1f), Quaternion.identity);
		}
	}
}
