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
	public Text testText;
	public Image leaderboardImage;
	public Image aboutUsImage;
	public GameObject playerScoreName;
	public GameObject playerScorePoints;
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
		back.onClick.AddListener(BackOnClick);
	}

	void Update () {


		if (TimeRemaining.timeRemaining <= 0 || PlayerScript.PlayerHealth <= 0 ) {
			if (Time.timeScale != 0) {
				ShowMenu();
				PauseGame();
			}
			
		}
	
		if (Input.GetKeyDown(KeyCode.Escape) && PlayerScript.playerName.Length != 0 && TimeRemaining.timeRemaining > 0 && PlayerScript.PlayerHealth > 0) {
				
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
		Debug.Log("hiding");
		foreach (Transform child in transform)
		{
			if (child.gameObject.CompareTag("playerrecord") ) {
				// destroys copies of player records to save mem.
				Destroy(child.gameObject);
			} else if(child.gameObject.CompareTag("back")){
				
			} else {
				child.gameObject.SetActive(false);
			}

		}
		
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
		SceneManager.LoadScene(0);
		ResumeGame();
	}

	private void ResumeOnClick()
	{
		HideMenu();
		ResumeGame();
	}

	private void ConfigOnClick()
	{
		//Application.Quit();
	}

	private void BackOnClick()
	{	
		HideMenu();
		HideLeaderboard();
		ShowMenu();
		aboutUsImage.gameObject.SetActive(false);
		
	}

	private void LeaderboardOnClick()
	{
		

		HideMenu();
		PauseGame();
		ShowLeaderboard();

		string result;
		string[] playerScore; 
		float yPosition = 480.00f;
		
		result = iAmHoldingPlayerRecords;
		playerScore = result.Split('_');

		foreach (string value in playerScore)
        {
        	string[] row = value.Split(' ');
        	
            Vector3 playerScorePosition = new Vector3(450.0f, yPosition, 0.0f);
            Vector3 playerScorePositionName = new Vector3(520.0f, yPosition, 0.0f);
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
		iAmHoldingPlayerRecords = download.text;

		
	}
	// MZqhY5SmFCkc DB PW
}
