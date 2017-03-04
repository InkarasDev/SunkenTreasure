using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) 
    {
    	
        if (coll.gameObject.CompareTag("Player"))
        {
        	// Player has touched the chest, do win
            //Time.timeScale = 0.0f;
        }
    }
    
}
