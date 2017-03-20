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
	private Animator animator;
	private AudioSource ouchSound;
	public AudioClip lostAudio;
	private bool audtioNotPlaying;
	public RuntimeAnimatorController onHitAnimation;
	private RuntimeAnimatorController currentPlayerAnimation;
	private CapsuleCollider2D playerCollider;
	private Animator currentPlayerAnimator;
	//private string leaderboardText;

	void Awake() 
	{
        Application.targetFrameRate = 60;
    }

	void Start () 
	{	
		playerCollider = GetComponent<CapsuleCollider2D> ();
		currentPlayerAnimator = GetComponent<Animator> ();
		currentPlayerAnimation = currentPlayerAnimator.runtimeAnimatorController;
		audtioNotPlaying = true;
		ouchSound = GetComponent<AudioSource> ();
		ouchSound.Stop();
		playerScore = 0;
		PlayerHealth = 5;
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
        //VelocityWhileIdle();

        if(Input.anyKey == false) {
			SlowDown();
		}

		if (PlayerHealth <= 0 || TimeRemaining.timeRemaining <= 0) {
			//ouchSound.Stop();
			if (CameraScript.FxNotMuted && audtioNotPlaying) {
			ouchSound.clip = lostAudio;
    		ouchSound.volume = CameraScript.FxVolume;
    		ouchSound.Play();
    		audtioNotPlaying = false;
    		}
		}
        
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
        	//Destroy(collided.GameObject);
        	TimeRemaining.timeRemaining += 20;
        	
        } else if (collided.gameObject.CompareTag("hp")) {

        	collided.gameObject.SetActive(false);
        	//Destroy(collided.gameObject);
        	PlayerHealth++;

        } else if (collided.gameObject.CompareTag("coin")) {

        	collided.gameObject.SetActive(false);
        	//Destroy(collided.gameObject);
        	playerScore++;
        	
        } 
    }

    public void CollisionHandler(bool air = false) 
    {
    	playerCollider.enabled = false;
    	StartCoroutine (EnablePlayerColliderAfterTwoSeconds ());
    	StartCoroutine (ChangePlayersAnimationToPreviousOneAfterTwoSeconds ());
    	Animator animator = playerTransform.gameObject.GetComponent<Animator>();
 		animator.runtimeAnimatorController = onHitAnimation;
    	if (CameraScript.FxNotMuted) {
    		ouchSound.volume = CameraScript.FxVolume;
    		ouchSound.Play();
    	}
    	
    	if (air) {
    		TimeRemaining.timeRemaining -= 5;

    	} else {
    		PlayerHealth--;
    	}


    }
    // kai atsitrenkia i priesa, padarom, kad dar 2 sekundes i nieka negaletu atsitrenkt
    private IEnumerator EnablePlayerColliderAfterTwoSeconds() {
        yield return new WaitForSeconds (2.0f);
        playerCollider.enabled = true;
    }
    // animacija grazinu i praeita po 2s.
    private IEnumerator ChangePlayersAnimationToPreviousOneAfterTwoSeconds() {
        yield return new WaitForSeconds (2.0f);
        Animator animator = playerTransform.gameObject.GetComponent<Animator>();
 		animator.runtimeAnimatorController = currentPlayerAnimation;
    }

    private void LimitSpeed() 
    {
    	if (SceneManager.GetActiveScene().buildIndex == 1) {
    		if (rb.velocity.y <= -2.0f) {
        	rb.velocity =  new Vector2(rb.velocity.x, -2.0f);
        	}
        	if (rb.velocity.y >= 2.0f) {
        		rb.velocity =  new Vector2(rb.velocity.x, 2.0f);
        	}
        	if (rb.velocity.x >= 2.0f) {
        		rb.velocity =  new Vector2(2.0f, rb.velocity.y);
        	}
        	if (rb.velocity.x <= -2.0f) {
        		rb.velocity =  new Vector2(-2.0f, rb.velocity.y);
        	}
    	} else {
    		if (rb.velocity.y <= -2.0f) {
        	rb.velocity =  new Vector2(rb.velocity.x, -2.0f);
        	}
        	if (rb.velocity.y >= 1.5f) {
        		rb.velocity =  new Vector2(rb.velocity.x, 1.5f);
        	}
        	if (rb.velocity.x >= 2.0f) {
        		rb.velocity =  new Vector2(2.0f, rb.velocity.y);
        	}
        	if (rb.velocity.x <= -2.0f) {
        		rb.velocity =  new Vector2(-2.0f, rb.velocity.y);
        	}
    	}
    	
    }

    private void VelocityWhileIdle() {
    	if (Time.timeScale != 1) {return;}

		if (SceneManager.GetActiveScene().buildIndex == 1) {
    		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.005f);
    	} else {
    		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.005f);
    	}
    }

    private void SlowDown() 
    {

    	if (Time.timeScale != 1) {return;}

		if (SceneManager.GetActiveScene().buildIndex == 1) {
			rb.velocity = new Vector2(rb.velocity.x / 1.05f, rb.velocity.y / 1.05f);
    		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.015f);
    	} else {
    		rb.velocity = new Vector2(rb.velocity.x / 1.05f, rb.velocity.y / 1.05f);
    		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.015f);
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
    		rb.AddForce(new Vector2(0.0f, -10.0f) * 1.0f);
    	} else {
    		rb.AddForce(new Vector2(0.0f, 10.0f) * 1.0f);
    	}

    }

    

}
