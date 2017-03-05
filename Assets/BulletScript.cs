using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	public Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindWithTag("Player");
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = transform.forward * 1000.0f;
		rigidbody.AddForce(new Vector2(transform.position.x, -50.0f));
		// -player.GetComponent<Rigidbody2D>().velocity.y
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) 
    {
    
        if (coll.gameObject.CompareTag("Enemies"))
        {
        	gameObject.SetActive(false);
        	Enemy1PatrolScript.enemyHealth--;
        	if (Enemy1PatrolScript.enemyHealth <= 0) {
        		coll.gameObject.SetActive(false);
        	}
        	
        	
        }
    }
}
