using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterScript : MonoBehaviour {

	public Text coins;
	
	void Update () 
	{
		coins.text = PlayerScript.playerScore.ToString();
	}
}
