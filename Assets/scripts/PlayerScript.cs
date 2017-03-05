using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
	public static int PlayerHealth = 2;
	public static Rigidbody2D rb;

	//shooting stuff

	public GameObject shot;
	public Transform shotSpawn;
	private float fireRate, nextFire;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector3(moveHorizontal, moveVertical);
        rb.AddForce(movement * 1.0f);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire) {
        	nextFire = Time.time + fireRate;
        	Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
       	LimitSpeed();
        
        
	}

	void OnCollisionEnter2D(Collision2D coll) 
    {
    
        if (coll.gameObject.CompareTag("Enemies"))
        {
        	
        	CollisionHandler();
        	
        }
    }

    public void CollisionHandler() 
    {
    	PlayerHealth--;
    	// Debug.Log(PlayerHealth);
        //rb.AddForce(new Vector2(transform.position.x, transform.position.y + 2.00f));
    }

    private void LimitSpeed() {
    	if (rb.velocity.y <= -3.0f) {
        	rb.velocity =  new Vector2(rb.velocity.x, -3.0f);
        }
        if (rb.velocity.y >= 3.0f) {
        	rb.velocity =  new Vector2(rb.velocity.x, 3.0f);
        }
        if (rb.velocity.x >= 3.0f) {
        	rb.velocity =  new Vector2(3.0f, rb.velocity.y);
        }
        if (rb.velocity.x <= -3.0f) {
        	rb.velocity =  new Vector2(-3.0f, rb.velocity.y);
        }
    }


}
