using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

	public static bool won;

	void Start () 
  {
		won = false;
	}

	void OnCollisionEnter2D(Collision2D coll) 
  {	
    if (coll.gameObject.CompareTag("Player")) {
    	if (!won) {GameIsWon();}
    }
  }

    private void GameIsWon() 
  	{
      if (SceneManager.GetActiveScene().buildIndex == 2) {
        won = true;
        SceneManager.LoadScene(3);
  		  StartCoroutine(SaveUserScore());
      } else {
          SceneManager.LoadScene(2);
      }
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
