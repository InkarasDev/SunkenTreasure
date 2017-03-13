using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public GameObject menu;
	public Button restart;
	public Button resume;
	public Button exit;
	public Button about;
	public Button leaderboard;
	public Button config;

	// Use this for initialization
	void Start () {
		// listenerius mygtukam visiem.
		restart.onClick.AddListener(RestartOnClick);
		resume.onClick.AddListener(ResumeOnClick);
		exit.onClick.AddListener(ExitOnClick);
		about.onClick.AddListener(AboutOnClick);
		leaderboard.onClick.AddListener(LeaderboardOnClick);
		config.onClick.AddListener(ConfigOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeRemaining.timeRemaining <= 0 || PlayerScript.PlayerHealth <= 0) {
			ShowMenu();
		}
	
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale != 1) {
				HideMenu();
			} else {
				ShowMenu();
			}
		}
	}

	private void HideMenu()
	{
		Time.timeScale = 1.0f;

		foreach (Transform child in transform)
		{
		  	child.gameObject.SetActive(false);
		}
	}

	private void ShowMenu()
	{
		// pereinam per visus mygtukus menu
		foreach (Transform child in transform)
		{
		  	child.gameObject.SetActive(true);
		}

		Time.timeScale = 0.0f;
	}

	private void RestartOnClick()
	{
		HideMenu();
		SceneManager.LoadScene(0);
		Time.timeScale = 1.0f;
	}

	private void ResumeOnClick()
	{
		HideMenu();
	}

	private void ConfigOnClick()
	{
		//Application.Quit();
	}

	private void LeaderboardOnClick()
	{
		//Application.Quit();
	}

	private void AboutOnClick()
	{
		//Application.Quit();
	}

	private void ExitOnClick()
	{
		Application.Quit();
	}
}
