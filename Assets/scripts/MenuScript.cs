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
	// Use this for initialization
	void Start () {
		restart.onClick.AddListener(RestartOnClick);
		resume.onClick.AddListener(ResumeOnClick);
		exit.onClick.AddListener(ExitOnClick);
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
		restart.gameObject.SetActive(false);
		resume.gameObject.SetActive(false);
		exit.gameObject.SetActive(false);
	}

	private void ShowMenu()
	{
		Time.timeScale = 0.0f;
		restart.gameObject.SetActive(true);
		resume.gameObject.SetActive(true);
		exit.gameObject.SetActive(true);
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

	private void ExitOnClick()
	{
		Application.Quit();
	}
}
