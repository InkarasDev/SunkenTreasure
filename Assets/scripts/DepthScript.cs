using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthScript : MonoBehaviour {

	public GameObject player;
	public Slider slider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float depth = Mathf.Floor(player.transform.position.y);
		slider.value = -depth;
	}
}
