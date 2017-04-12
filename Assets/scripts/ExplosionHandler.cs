using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionHandler : MonoBehaviour {

	private AudioSource exploasionSound;
 
    void Start () 
    {
    	exploasionSound = GetComponent<AudioSource> ();
       	StartCoroutine (KillOnAnimationEnd ());

    	if (CameraScript.FxNotMuted) {
			exploasionSound.volume = CameraScript.FxVolume;
		} else {
			exploasionSound.volume = 0.0f;
		}
    }

    private IEnumerator KillOnAnimationEnd() 
    {
         yield return new WaitForSeconds (0.6f);
         Destroy (gameObject);
    }
}
