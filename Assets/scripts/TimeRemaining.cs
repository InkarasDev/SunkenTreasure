using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemaining : MonoBehaviour {

	public static float timeRemaining;
	public static bool inWater;
	public Text timer;
	public GameObject gameIsLostText;
	public Slider slider;

  	void Start()
  	{
  	    timeRemaining = 3000;
  	}
  	void Update()
  	{
        if (Time.timeScale != 1) {return;}
  	    timeRemaining -= Time.deltaTime;
  	    if (timeRemaining > 0) {
  	        //float minutes = Mathf.Floor(timeRemaining / 60);
  	        //float seconds = Mathf.Floor(timeRemaining % 60);
  	        //timer.text = " " + minutes.ToString("00") + ":" + seconds.ToString("00");
            //Debug.Log(timeRemaining);
  	        slider.value = timeRemaining;
  	    } else {
  	    	GameIsLost();
  	    }

        
  	}
    
    void LateUpdate() {
      if (PlayerScript.PlayerHealth <= 0) {
          GameIsLost();
        }
    }

  	private void GameIsLost() 
  	{
  		gameIsLostText.SetActive(true);
      StartCoroutine(SaveUserScore());
  	}

    private IEnumerator SaveUserScore() 
    {
      string highscore_url = "http://gediminasceponis.000webhostapp.com/saverecord.php";
      WWWForm form = new WWWForm();
      form.AddField( "playerpoints", PlayerScript.playerScore );
      form.AddField( "playername", PlayerScript.playerName );
      // Create a download object
      WWW download = new WWW( highscore_url, form );
  
      // Wait until the download is done
      yield return download;
  }

}
