using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour {
	
	public Text hp;

	void Update () 
	{
		hp.text = PlayerScript.PlayerHealth.ToString();
	}
}
