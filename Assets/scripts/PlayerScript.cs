using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
	public static int PlayerHealth;
	public static Rigidbody2D rb;

	//shooting stuff
	public GameObject shot;
	public Transform shotSpawn;
	private float fireRate, nextFire;
	void Awake() {
        Application.targetFrameRate = 60;
    }
	void Start () {
		PlayerHealth = 2;
		Time.timeScale = 1.0f;
		rb = GetComponent<Rigidbody2D>();
		fireRate = 1.0f;
	}
	
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
        VelocityWhileIdle();
        
	}

	void OnCollisionEnter2D(Collision2D coll) {
    
        if (coll.gameObject.CompareTag("Enemies")) {
        	
        	CollisionHandler();
        	
        } else if (coll.gameObject.CompareTag("Enemies2")) {

        	CollisionHandler(true);

        } else if (coll.gameObject.CompareTag("oxygen")) {
        	coll.gameObject.SetActive(false);
        	TimeRemaining.timeRemaining += 5;
        } else if (coll.gameObject.CompareTag("hp")) {
        	coll.gameObject.SetActive(false);
        	PlayerHealth++;
        }
    }

    public void CollisionHandler(bool dead = false) 
    {
    	if (dead) {
    		PlayerHealth -= 2;
    	} else {
    		PlayerHealth--;
    	}

    	if (PlayerHealth <= 0) {
    		gameObject.SetActive(false);
    		Time.timeScale = 0.0f;
    	}
    }

    private void LimitSpeed() {
    	if (SceneManager.GetActiveScene().buildIndex == 0) {
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
    	} else {
    		if (rb.velocity.y <= -3.0f) {
        	rb.velocity =  new Vector2(rb.velocity.x, -3.0f);
        	}
        	if (rb.velocity.y >= 2.0f) {
        		rb.velocity =  new Vector2(rb.velocity.x, 2.0f);
        	}
        	if (rb.velocity.x >= 3.0f) {
        		rb.velocity =  new Vector2(3.0f, rb.velocity.y);
        	}
        	if (rb.velocity.x <= -3.0f) {
        		rb.velocity =  new Vector2(-3.0f, rb.velocity.y);
        	}
    	}
    	
    }

    private void VelocityWhileIdle() {
    	if (Time.timeScale != 1) {return;}
		if (SceneManager.GetActiveScene().buildIndex == 0) {
    		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.005f);
    	} else {
    		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.005f);
    	}
    }

}
