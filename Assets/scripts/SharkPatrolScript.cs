using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharkPatrolScript : MonoBehaviour {

	public bool goLeft = true;
	private float incrementValue = 0.125f;
	public float maxLeft;
	public float maxRight;
	public int enemyHealth;
    private GameObject playerTransformPosition;
	private bool shouldMoveBecausePlayerIsClose;

	void Start () {
        shouldMoveBecausePlayerIsClose = false;
        playerTransformPosition = GameObject.FindWithTag("Player");
	    InvokeRepeating("CheckIfPlayerIsClose", 0.0f, 1.0f);
	}
	
	void Update () {
		
		if (Time.timeScale != 1) {return;}
        if (!shouldMoveBecausePlayerIsClose) {return;}

        if (transform.position.x >= maxLeft && goLeft ) {
        	transform.position = new Vector2(transform.position.x - incrementValue , transform.position.y);
        	if (transform.position.x <= maxLeft) {
        		goLeft = false;
        		transform.rotation = Quaternion.Euler(transform.rotation.x, 0.00f, transform.rotation.z);
        	}
        } else {
        	transform.position = new Vector2(transform.position.x + incrementValue, transform.position.y);
        	if (transform.position.x >= maxRight) {
        		goLeft = true;
        		transform.rotation = Quaternion.Euler(transform.rotation.x, 180.00f, transform.rotation.z);
        	}
        }
	}

	void OnCollisionEnter2D(Collision2D coll) 
    {
    
        if (coll.gameObject.CompareTag("Bullet"))
        {
        	coll.gameObject.SetActive(false);
        	enemyHealth--;
        	if (enemyHealth <= 0) {
        		gameObject.SetActive(false);
        	}
        }

       
	}

	private void CheckIfPlayerIsClose() {
        
        
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
