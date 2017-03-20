using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

	private bool won;

	// Use this for initialization
	void Start () {
		won = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) 
    {
    	
        if (coll.gameObject.CompareTag("Player"))
        {
        	if (!won) {
        		GameIsWon();
        		won = true;
        	}
        	
        	

        }
    }

    private void GameIsWon() 
  	{
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
      SceneManager.LoadScene(1);
  }
}
