using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public GameObject player;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        // offset = transform.position - player.transform.position;
        // Debug.Log(player.transform.position.x);
	}
	
	// Update is called once per frame
    // late update runs after update, so we have updated values of this frame to work with instead of values from prev frame. 
	void LateUpdate () {
        //transform.position = player.transform.position + offset;
        if (player.transform.position.y >= 0) {
        	transform.position = new Vector3(2.0f, player.transform.position.y, -1.0f);
        	}
        
	}
}
