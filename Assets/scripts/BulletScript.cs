using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	public Rigidbody2D rigidbody;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		
		rigidbody.velocity = transform.forward * 1000.0f;
		rigidbody.AddForce(new Vector2(transform.position.x, -50.0f));
	}

	private void OnCollisionEnter2D(Collision2D coll) {
   
			Instantiate(explosion, transform.position, transform.rotation);
			// istrina kulka
			Destroy(gameObject);	
    }
   
}
