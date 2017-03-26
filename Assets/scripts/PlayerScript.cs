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
	public Collider2D enemyCollider;

	//shooting stuff
	public GameObject shot;
	public Transform shotSpawn;
	private float fireRate, nextFire;

	private AudioSource ouchSound;
	public AudioClip lostAudio;
	private bool audtioNotPlaying;
	

	private Animator currentPlayerAnimator;
	private Animator onHitAnimator;
	public GameObject onHitPositionAndAnimationGameObject;

	void Awake() 
	{
        Application.targetFrameRate = 60;
    }

	void Start () 
	{	

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

        if(Input.anyKey == false) {
			SlowDown();
		}

		if (PlayerHealth <= 0 || TimeRemaining.timeRemaining <= 0) {
			if (CameraScript.FxNotMuted && audtioNotPlaying) {
			ouchSound.clip = lostAudio;
    		ouchSound.volume = CameraScript.FxVolume;
    		if (CameraScript.FxNotMuted) {
    			ouchSound.Play();
    		}
    		
    		audtioNotPlaying = false;
    		}
		}
        
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
    
        if (coll.gameObject.CompareTag("Enemies")) {
        	
        	CollisionHandler();
        	KnockUp(coll);
        	AddOnHitAnimation(coll);
        	enemyCollider = coll.gameObject.GetComponent<Collider2D>();
        	DisableEnemyCollider(enemyCollider);

        } else if (coll.gameObject.CompareTag("Enemies2")) {

        	CollisionHandler(true);
        	KnockUp(coll);
        	AddOnHitAnimation(coll);

        } else if (coll.gameObject.CompareTag("Background")) {

        	KnockAway();

        } else if (coll.gameObject.CompareTag("Obsticle")) {
        	
        	TimeRemaining.timeRemaining -= 5;
        	KnockUp(coll);
        	AddOnHitAnimation(coll);

        }
    }

    void OnTriggerEnter2D(Collider2D collided) 
    {

    	if (collided.gameObject.CompareTag("oxygen")) {

        	collided.gameObject.SetActive(false);
        	TimeRemaining.timeRemaining += 20;
        	
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

    private void DisableEnemyCollider(Collider2D enemyCollider)
    {

		enemyCollider.isTrigger = true;
    	StartCoroutine (EnableEnemyColliderAfterThreeSec (enemyCollider));

    }

    //kai atsitrenkia i priesa, padarom, kad dar 1 sekundes i nieka negaletu atsitrenkt
    private IEnumerator EnableEnemyColliderAfterThreeSec(Collider2D enemyCollider) 
    {

        yield return new WaitForSeconds (3.0f);
        enemyCollider.isTrigger = false;
        
    }

    // animacija grazinu i praeita po 2s.
    private IEnumerator HideOnHitAnimationAfterOneLoop() 
    {
        yield return new WaitForSeconds (0.25f);
        onHitPositionAndAnimationGameObject.gameObject.SetActive(false);
        onHitPositionAndAnimationGameObject.transform.position = transform.position;
    }

    private void AddOnHitAnimation(Collision2D coll)
    {
    	// paziuri kurioj vietoj piest ta animacija
    	float xx = coll.gameObject.transform.position.x - transform.position.x;
    	float yy = coll.gameObject.transform.position.y - transform.position.y;
    	onHitPositionAndAnimationGameObject.transform.position = new Vector2 (onHitPositionAndAnimationGameObject.transform.position.x + xx / 2, onHitPositionAndAnimationGameObject.transform.position.y + yy / 2);
    	onHitPositionAndAnimationGameObject.gameObject.SetActive(true);
    	StartCoroutine(HideOnHitAnimationAfterOneLoop ());
    	
    }

    private void LimitSpeed() 
    {
    	// neduod insvaziuot greiciau nei 2, bet kuria krpytim
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

    private void VelocityWhileIdle() 
    {
    	if (Time.timeScale != 1) {return;}
    	// duoda pluduriavimo efekta, arba skendimo antram lygy
		if (SceneManager.GetActiveScene().buildIndex == 1) {
    		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.005f);
    	} else {
    		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.005f);
    	}
    }

    private void SlowDown() 
    {

    	if (Time.timeScale != 1) {return;}
    	// kai nieko nepamyges zaidejas kvieciama, kad greiciau sustotu
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
    	// meta i viena arba i kita sona
    	if (transform.position.x > 0) {
    		
    		rb.AddForce(new Vector2(-10.0f, 0.0f) * 1.0f);

    	} else {
    		
    		rb.AddForce(new Vector2(10.0f, 0.0f) * 1.0f);

    	}
    	
    }

    private void KnockUp(Collision2D coll)
    {
    	// meta virsun arb apacion
    	if (coll.gameObject.transform.position.y > transform.position.y) {
    		rb.AddForce(new Vector2(0.0f, -10.0f) * 1.0f);
    	} else {
    		rb.AddForce(new Vector2(0.0f, 10.0f) * 1.0f);
    	}

    }

    

}
