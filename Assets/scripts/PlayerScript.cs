using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
	public static int PlayerHealth = 2;
	public Rigidbody2D rb;
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
    	Debug.Log(PlayerHealth);
        //rb.AddForce(new Vector2(transform.position.x, transform.position.y + 2.00f));
    }
}
