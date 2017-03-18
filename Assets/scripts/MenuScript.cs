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
	public Text testText;
	public Image leaderboardImage;
	public GameObject playerScoreText;
	public static string iAmHoldingPlayerRecords;

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

	void Update () {
		if (TimeRemaining.timeRemaining <= 0 || PlayerScript.PlayerHealth <= 0) {
			ShowMenu();
		}
	
		if (Input.GetKeyDown(KeyCode.Escape) && PlayerScript.playerName.Length != 0) {
			if (Time.timeScale != 1) {
				HideMenu();
				HideLeaderboard();
			} else {
				ShowMenu();
			}
		}
	}

	private void HideMenu()
	{
		ResumeGame();

		foreach (Transform child in transform)
		{
			if (child.gameObject.CompareTag("playerrecord")) {
				Destroy(child.gameObject);
			}

		  	child.gameObject.SetActive(false);
		  	
		}
		HideLeaderboard();
	}

	private void ShowMenu()
	{
		// pereinam per visus mygtukus menu
		foreach (Transform child in transform)
		{
			if (!child.gameObject.CompareTag("playerrecord")) {
				child.gameObject.SetActive(true);
			}
		  	
		}

		PauseGame();

		StartCoroutine(GetUsersRecords());
	}

	private void RestartOnClick()
	{
		HideMenu();
		SceneManager.LoadScene(0);
		ResumeGame();
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
		HideMenu();
		string result;
		string[] playerScore; 
		float yPosition = 510.00f;
		ShowLeaderboard();
		result = iAmHoldingPlayerRecords;
		playerScore = result.Split('_');

		foreach (string value in playerScore)
        {
            Vector3 playerScorePosition = new Vector3(450.0f, yPosition, 0.0f);
            GameObject go;
			go=Instantiate(playerScoreText, playerScorePosition, Quaternion.Euler(0.0f,0.0f,0.0f)) as GameObject;	
			go.transform.SetParent(transform);
			go.transform.position = playerScorePosition;
			playerScoreText.GetComponent<UnityEngine.UI.Text>().text = value;
			yPosition -= 40.0f;
        }

        PauseGame();
	}

	private void AboutOnClick()
	{
		
	}

	private void ExitOnClick()
	{
		Application.Quit();
	}

	private void ShowLeaderboard()
	{

		leaderboardImage.gameObject.SetActive(true);

	}

	private void HideLeaderboard()
	{

		leaderboardImage.gameObject.SetActive(false);

	}

	public static void PauseGame() 
	{
		Time.timeScale = 0.0f;
	}

	public static void ResumeGame()
	{
		Time.timeScale = 1.0f;
	}

	private IEnumerator GetUsersRecords() 
	{
		string highscore_url = "http://gceponisdev.16mb.com/getuserdata.php";
	
		WWWForm form = new WWWForm();
		form.AddField( "a", "0" );

		// Create a download object
		WWW download = new WWW( highscore_url, form );

		// Wait until the download is done
		yield return download;
		iAmHoldingPlayerRecords = download.text;
		
	}
	// MZqhY5SmFCkc DB PW
}
