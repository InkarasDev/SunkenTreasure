using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class PlayerScript : MonoBehaviour {
	public static int PlayerHealth;
	public static int playerScore;
	public static string playerName;
	public Rigidbody2D rb;
	public Transform playerTransform;
	//shooting stuff
	public GameObject shot;
	public Transform shotSpawn;
	private float fireRate, nextFire;
	public InputField playerInput;
	private Animator animator;
	//private string leaderboardText;

	void Awake() 
	{
        Application.targetFrameRate = 60;
    }

	void Start () 
	{	

		playerScore = 0;
		PlayerHealth = 99;
		Time.timeScale = 1.0f;
		
		fireRate = 1.0f;
		
	}
	
	void Update () 
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.AddForce(movement * 1.0f);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire && Time.timeScale != 0) {
        	nextFire = Time.time + fireRate;
        	Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }

       	LimitSpeed();
        VelocityWhileIdle();
        
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
    
        if (coll.gameObject.CompareTag("Enemies")) {
        	
        	CollisionHandler();
        	KnockUp(coll);
        	
        } else if (coll.gameObject.CompareTag("Enemies2")) {

        	CollisionHandler(true);
        	KnockUp(coll);

        } else if (coll.gameObject.CompareTag("Background")) {

        	KnockAway();

        } else if (coll.gameObject.CompareTag("Obsticle")) {
        	
        	TimeRemaining.timeRemaining -= 5;
        	KnockUp(coll);

        }
    }

    void OnTriggerEnter2D(Collider2D collided) 
    {

    	if (collided.gameObject.CompareTag("oxygen")) {

        	collided.gameObject.SetActive(false);
        	TimeRemaining.timeRemaining += 10;
        	
        } else if (collided.gameObject.CompareTag("hp")) {

        	collided.gameObject.SetActive(false);
        	PlayerHealth++;

        } else if (collided.gameObject.CompareTag("coin")) {

        	collided.gameObject.SetActive(false);
        	playerScore++;
        	
        } 
    }

    public void CollisionHandler(bool air = false) 
    {
    	if (air) {
    		TimeRemaining.timeRemaining -= 5;

    	} else {
    		PlayerHealth--;
    	}

    	if (PlayerHealth <= 0) {
    		gameObject.SetActive(false);
    		Time.timeScale = 0.0f;
    	}
    }

    private void LimitSpeed() 
    {
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

    private void KnockAway() 
    {

    	if (transform.position.x > 0) {
    		
    		rb.AddForce(new Vector2(-10.0f, 0.0f) * 1.0f);

    	} else {
    		
    		rb.AddForce(new Vector2(10.0f, 0.0f) * 1.0f);

    	}
    	
    }

    private void KnockUp(Collision2D coll)
    {

    	if (coll.gameObject.transform.position.y > transform.position.y) {
    		rb.AddForce(new Vector2(0.0f, -20.0f) * 1.0f);
    	} else {
    		rb.AddForce(new Vector2(0.0f, 20.0f) * 1.0f);
    	}

    }

    

}
