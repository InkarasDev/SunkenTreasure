using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayPatrolScript : MonoBehaviour {

	private bool goUp = true;
	private float incrementValue = 0.05f;
	public float maxUp;
	public float maxDown;
	public int enemyHealth;
	//public bool collided = false;

	void Start () {
		enemyHealth = 1;
	}
	
	void Update () {
        if (Time.timeScale != 1) {return;}
        
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

	// void LateUpdate() {
	// 	// collided is set to false after every frame, this is used to prevent collision event from firing twice on collision.
	// 	collided = false;
	// }
	
}
