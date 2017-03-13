using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UrchiMovementScript : MonoBehaviour {

	//private bool goLeft = true;
	//private float incrementValue = 0.20f;
	//public float maxLeft;
	//public float maxRight;
	public int enemyHealth;
	//public bool collided = false;
	public Rigidbody2D rb;
	// Use this for initialization
	private static float timeRemaining;

	void Start () {
		// rb.velocity = transform.forward * 1000.0f;
		// rb.AddForce(new Vector2(transform.position.x, -50.0f));
		timeRemaining = 5;
		enemyHealth = 1;
	}
	
	void Update () {
		timeRemaining -= Time.deltaTime;
		LimitSpeed();

		if (timeRemaining <= 0) {
			var randomInt = Random.Range(0,2);
        	if (randomInt == 0) {
        		rb.AddForce(new Vector2(100.0f, -200.0f) * 1.0f);
        		} else {
        			rb.AddForce(new Vector2(-100.0f, -200.0f) * 1.0f);
        		}
        	timeRemaining = 5;
		}
		//if (Time.timeScale != 1) {return;}
        
        // if (transform.position.x >= maxLeft && goLeft ) {
        // 	transform.position = new Vector2(transform.position.x - incrementValue , transform.position.y);
        // 	if (transform.position.x <= maxLeft) {
        // 		goLeft = false;
        // 		transform.rotation = Quaternion.Euler(transform.rotation.x, 0.00f, transform.rotation.z);
        // 	}
        // } else {
        // 	transform.position = new Vector2(transform.position.x + incrementValue, transform.position.y);
        // 	if (transform.position.x >= maxRight) {
        // 		goLeft = true;
        // 		transform.rotation = Quaternion.Euler(transform.rotation.x, 180.00f, transform.rotation.z);
        // 	}
        // }
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

        // if (coll.gameObject.CompareTag("Obsticle"))
        // {	
        	var randomInt = Random.Range(0,2);
        	if (randomInt == 0) {
        		rb.AddForce(new Vector2(100.0f, 0.0f) * 1.0f);
        		} else {
        			rb.AddForce(new Vector2(-100.0f, 0.0f) * 1.0f);
        		}
        	//Debug.Log(randomInt);
        //}
	}
	// limits speed in all directions to 1.
	void LimitSpeed() {
    	if (SceneManager.GetActiveScene().buildIndex == 0) {
    		if (rb.velocity.y <= -1.0f) {
        	rb.velocity =  new Vector2(rb.velocity.x, -1.0f);
        	}
        	if (rb.velocity.y >= 1.0f) {
        		rb.velocity =  new Vector2(rb.velocity.x, 1.0f);
        	}
        	if (rb.velocity.x >= 1.0f) {
        		rb.velocity =  new Vector2(1.0f, rb.velocity.y);
        	}
        	if (rb.velocity.x <= -1.0f) {
        		rb.velocity =  new Vector2(-1.0f, rb.velocity.y);
        	}
    	} else {
    		if (rb.velocity.y <= -1.0f) {
        	rb.velocity =  new Vector2(rb.velocity.x, -1.0f);
        	}
        	if (rb.velocity.y >= 1.0f) {
        		rb.velocity =  new Vector2(rb.velocity.x, 1.0f);
        	}
        	if (rb.velocity.x >= 1.0f) {
        		rb.velocity =  new Vector2(1.0f, rb.velocity.y);
        	}
        	if (rb.velocity.x <= -1.0f) {
        		rb.velocity =  new Vector2(-1.0f, rb.velocity.y);
        	}
    	}
    	
    }
}
