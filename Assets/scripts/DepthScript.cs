using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthScript : MonoBehaviour {

	public GameObject player;	

	void Update () 
	{
		float yPlayerPos = player.transform.position.y + 580.00f;
		transform.position = new Vector2(transform.position.x, yPlayerPos);
	}
}
