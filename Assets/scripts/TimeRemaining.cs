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
  private bool lost;

  	void Start()
  	{
  	    timeRemaining = 180;
        lost = false;
  	}

  	void Update()
  	{
        if (Time.timeScale != 1) {return;}
        
  	    timeRemaining -= Time.deltaTime;
  	    if (timeRemaining > 0) {
  	        slider.value = timeRemaining;
  	    } 
  	}
    
    void LateUpdate() {
      if (PlayerScript.PlayerHealth <= 0 || timeRemaining <= 0) {
        if (!lost) {
          GameIsLost();
          lost = true;
        } 
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
