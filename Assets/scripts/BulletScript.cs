using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour {
	public Rigidbody2D rigidbody;
	public GameObject explosion;
	// Use this for initialization
	void Start () 
	{
		
		if (SceneManager.GetActiveScene().buildIndex == 1) {
			rigidbody.velocity = transform.forward * 1000.0f;
			rigidbody.AddForce(new Vector2(transform.position.x, -50.0f));
		} else {
			rigidbody.velocity = transform.forward * 1000.0f;
			rigidbody.AddForce(new Vector2(transform.position.x, + 100.0f));
			rigidbody.gravityScale = -1.0f;
		
		}
		StartCoroutine(DestroyBulletAfter1Sec());
	}

	private void OnCollisionEnter2D(Collision2D coll) 
	{
   
			Instantiate(explosion, transform.position, transform.rotation);
			// istrina kulka
			Destroy(gameObject);	
    }

    private IEnumerator DestroyBulletAfter1Sec() 
	{
		yield return new WaitForSeconds (1.0f);
		Destroy(gameObject);
	}
   
}
