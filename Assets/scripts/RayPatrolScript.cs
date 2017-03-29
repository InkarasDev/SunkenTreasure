using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RayPatrolScript : MonoBehaviour {

	private bool goUp = true;
	private float incrementValue = 0.05f;
	public float maxUp;
	public float maxDown;
	public int enemyHealth;
    private GameObject playerTransformPosition;
    private bool shouldMoveBecausePlayerIsClose;
	
    

	void Start () {
        shouldMoveBecausePlayerIsClose = false;
        playerTransformPosition = GameObject.FindWithTag("Player");
		enemyHealth = 1;
        InvokeRepeating("CheckIfPlayerIsClose", 0.0f, 1.0f);
        
	}
	
	void Update () {
        if (Time.timeScale != 1) {return;}
        if (!shouldMoveBecausePlayerIsClose) {return;}

        if (transform.position.y <= maxUp && goUp ) {
        	transform.position = new Vector2(transform.position.x, transform.position.y + incrementValue);
        	if (transform.position.y >= maxUp) {
        		goUp = false;
        		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180.0f);
        	}
        } else {
        	transform.position = new Vector2(transform.position.x, transform.position.y - incrementValue);
        	if (transform.position.y <= maxDown) {
        		goUp = true;
        		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0.0f);
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
        //Debug.Log(Mathf.Pow(playerY - thisY, 2));
        if(Mathf.Pow(playerY - thisY, 2) < 80) {
            shouldMoveBecausePlayerIsClose = true;
        } else {
            shouldMoveBecausePlayerIsClose = false;
        }
        
    }

  
}
