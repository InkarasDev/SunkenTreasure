﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBalloonScript : MonoBehaviour {

	public bool staticBalloon;
	private float incrementValue;
	private float patrolLimit;
	private bool up;
	public bool thisShouldWiggle;
	private GameObject playerTransformPosition;
	private bool shouldMoveBecausePlayerIsClose;

	void Start () 
	{
		patrolLimit = 0.25f;
		incrementValue = 0.005f;
		up = true;
		shouldMoveBecausePlayerIsClose = false;
        playerTransformPosition = GameObject.FindWithTag("Player");
	    InvokeRepeating("CheckIfPlayerIsClose", 0.0f, 1.0f);
	}
	
	void Update () 
	{
		if (Time.timeScale != 1) { return;}
		if (!shouldMoveBecausePlayerIsClose) {return;}

		if (!staticBalloon && !thisShouldWiggle) {
			transform.position = new Vector2(transform.position.x, transform.position.y + incrementValue);
		} else if (!thisShouldWiggle) {
			patrolLimit -= incrementValue;
			if (patrolLimit >= 0 && up) {
				transform.position = new Vector2(transform.position.x, transform.position.y + incrementValue);
			} else {
				if (up) {
					patrolLimit = 0.25f;
				}

				up = false;
				transform.position = new Vector2(transform.position.x, transform.position.y - incrementValue);
				if (patrolLimit <= 0) {
					patrolLimit = 0.25f;
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

	private void CheckIfPlayerIsClose() 
	{
        
        float playerY = playerTransformPosition.transform.position.y;
        float thisY = transform.position.y;
        if (playerY < 0) {
            playerY = -1 * playerY;
        }
        if (thisY < 0) {
            thisY = -1 * thisY;
        }

        if(Mathf.Pow(playerY - thisY, 2) < 80) {
            shouldMoveBecausePlayerIsClose = true;
        } else {
            shouldMoveBecausePlayerIsClose = false;
        }
    }
}
