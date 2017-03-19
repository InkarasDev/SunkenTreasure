using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour {

	public InputField playerInput;
	public Button StartGameButton;
	
	void Start () {
		StartGameButton.onClick.AddListener(StartGameButtonOnClick);
	}
	
	private void StartGameButtonOnClick() 
	{

		if (playerInput.text.ToString().Length != 0 ) {
        	PlayerScript.playerName = playerInput.text.ToString();
        	SceneManager.LoadScene(1);
        }

	}
}
