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
	public Button back;
	public Button backConfig;
	public Text testText;
	public Image leaderboardImage;
	public Image aboutUsImage;
	public GameObject playerScoreName;
	public GameObject playerScorePoints;
	public GameObject sounds;
	public static string iAmHoldingPlayerRecords;
	private bool isDownloaded;

	// Use this for initialization
	void Start () 
	{
		isDownloaded = false;
		// listenerius mygtukam visiem.
		restart.onClick.AddListener(RestartOnClick);
		resume.onClick.AddListener(ResumeOnClick);
		exit.onClick.AddListener(ExitOnClick);
		about.onClick.AddListener(AboutOnClick);
		leaderboard.onClick.AddListener(LeaderboardOnClick);
		config.onClick.AddListener(ConfigOnClick);
		back.onClick.AddListener(BackOnClick);
		backConfig.onClick.AddListener(BackOnClick);
		// nuvaziuoja pasiimt zaideju duomenu.
		StartCoroutine(GetUsersRecords());

	}

	void Update () 
	{

		// ziuri ar ddar nepralosei
		if (TimeRemaining.timeRemaining <= 0 || PlayerScript.PlayerHealth <= 0 ) {
			if (Time.timeScale != 0) {
				ShowMenu();
				PauseGame();
			}
			
		}
		// leidzia isjungt menu tik td, jei dar nesi praloses
		if (
			Input.GetKeyDown(KeyCode.Escape) 
			&& PlayerScript.playerName.Length != 0 
			&& TimeRemaining.timeRemaining > 0 
			&& PlayerScript.PlayerHealth > 0
			&& !WinScript.won
			) {
				
			if (Time.timeScale != 1) {
				HideMenu();
				HideLeaderboard();
				ResumeGame();
			} else {
				ShowMenu();
				PauseGame();
			}
					
		}
	}

	private void HideMenu()
	{
		//Debug.Log("hiding");
		foreach (Transform child in transform)
		{
			if (child.gameObject.CompareTag("playerrecord") ) {
				// destroys copies of player records to save mem.
				Destroy(child.gameObject);
			} else if(child.gameObject.CompareTag("back")){
				
			} else if (child.gameObject.CompareTag("sounds")) {

			} else {
				child.gameObject.SetActive(false);
			}

		}
		sounds.gameObject.SetActive(false);
		aboutUsImage.gameObject.SetActive(false);
		
	}

	private void ShowMenu()
	{
		
		// pereinam per visus mygtukus menu
		foreach (Transform child in transform)
		{
			if (!child.gameObject.CompareTag("playerrecord") && !child.gameObject.CompareTag("back")) {
				child.gameObject.SetActive(true);
			}
		  	
		}
		// go fetch data about user scores so we can display leaderboard faster.
		StartCoroutine(GetUsersRecords());
	}

	private void RestartOnClick()
	{
		HideMenu();
		if (SceneManager.GetActiveScene().buildIndex == 3) {
			SceneManager.LoadScene(0);
		} else {
			SceneManager.LoadScene(1);
		}
		WinScript.won = false;
		PlayerScript.playerScore = 0;
		ResumeGame();
	}

	private void ResumeOnClick()
	{
		if (WinScript.won) {return;}
		HideMenu();
		ResumeGame();
	}

	private void ConfigOnClick()
	{
		HideMenu();
		sounds.gameObject.SetActive(true);
	}

	private void BackOnClick()
	{	
		HideMenu();
		HideLeaderboard();
		ShowMenu();
		aboutUsImage.gameObject.SetActive(false);
		sounds.gameObject.SetActive(false);
		
	}

	private void LeaderboardOnClick()
	{
		

		HideMenu();
		PauseGame();
		ShowLeaderboard();

		string result;
		string[] playerScore; 
		float yPosition = 170.00f;
		
		result = iAmHoldingPlayerRecords;
		playerScore = result.Split('_');

		foreach (string value in playerScore)
        {
        	string[] row = value.Split(' ');
        	
            Vector3 playerScorePosition = new Vector3(leaderboardImage.gameObject.transform.position.x + 60.0f, leaderboardImage.gameObject.transform.position.y + yPosition, 0.0f);
            Vector3 playerScorePositionName = new Vector3(leaderboardImage.gameObject.transform.position.x + 140.0f, leaderboardImage.gameObject.transform.position.y + yPosition, 0.0f);
            GameObject name;
			name=Instantiate(playerScoreName, playerScorePositionName, Quaternion.Euler(0.0f,0.0f,0.0f)) as GameObject;	
			name.transform.SetParent(transform);
			name.transform.position = playerScorePositionName;

			GameObject points;
			points=Instantiate(playerScorePoints, playerScorePosition, Quaternion.Euler(0.0f,0.0f,0.0f)) as GameObject;	
			points.transform.SetParent(transform);
			points.transform.position = playerScorePosition;

			points.GetComponent<UnityEngine.UI.Text>().text = row[0];
			name.GetComponent<UnityEngine.UI.Text>().text = row[1];
			
			yPosition -= 40.0f;
        }
        
	}

	private void AboutOnClick()
	{
		HideMenu();
		aboutUsImage.gameObject.SetActive(true);
		back.gameObject.SetActive(true);
	}

	private void ExitOnClick()
	{
		Application.Quit();
	}

	private void ShowLeaderboard()
	{

		leaderboardImage.gameObject.SetActive(true);
		back.gameObject.SetActive(true);

	}

	private void HideLeaderboard()
	{

		leaderboardImage.gameObject.SetActive(false);
		back.gameObject.SetActive(false);

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
		string highscore_url = "http://gediminasceponis.000webhostapp.com/getuserdata.php";
	
		WWWForm form = new WWWForm();
		form.AddField( "a", "0" );

		// Create a download object
		WWW download = new WWW( highscore_url, form );

		// Wait until the download is done
		yield return download;
		// this handles php errors and/or no user records
		if (download.text.Length == 0 || download.text.ToString()[0] == '<') {
			iAmHoldingPlayerRecords = "No records";
		} else {
			iAmHoldingPlayerRecords = download.text;
		}
		if (SceneManager.GetActiveScene().buildIndex == 3 && !isDownloaded) {
			LeaderboardOnClick();
			isDownloaded = true;
		}
		
		
	}
	// 20 + 330 + 172 + 150 pusryciai
	// 20 + 20 + 252 + 343 + 150 pietus
	// 500 + 50 + 250 + 20 + 50

	// 2327
}
