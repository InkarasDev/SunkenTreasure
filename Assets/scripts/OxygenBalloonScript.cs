using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBalloonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 1) { return;}
		transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
	}
}
