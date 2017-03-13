using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBalloonScript : MonoBehaviour {

	public bool staticBalloon;
	private float incrementValue;
	private float limit;
	private bool up;
	public bool thisShouldWiggle;
	// Use this for initialization
	void Start () {
		limit = 0.25f;
		incrementValue = 0.005f;
		up = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 1) { return;}

		if (!staticBalloon && !thisShouldWiggle) {
			transform.position = new Vector2(transform.position.x, transform.position.y + incrementValue);
		} else if (!thisShouldWiggle) {
			limit -= incrementValue;
			if (limit >= 0 && up) {
				transform.position = new Vector2(transform.position.x, transform.position.y + incrementValue);
			} else {
				if (up) {
					limit = 0.25f;
				}

				up = false;
				transform.position = new Vector2(transform.position.x, transform.position.y - incrementValue);
				if (limit <= 0) {
					limit = 0.25f;
					up = true;
				}
			}
		}

		if (thisShouldWiggle) {
			WiggleMe();
		}
		
	}

	private void WiggleMe() 
	{
		transform.Rotate( new Vector3(transform.rotation.x, transform.rotation.y  + 10.0f, transform.rotation.z));
	}
}
