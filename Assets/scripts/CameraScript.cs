using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

	public GameObject player;
    private Vector3 offset;
    private AudioSource musicSound; 

 	public Slider MusicSlider;
 	public Slider FxSlider;
 	public Toggle MuteMusic;
 	public Toggle MuteFx;
 	public static float FxVolume;
 	public static bool FxNotMuted;
	// Use this for initialization
	void Start () {

      	musicSound = GetComponent<AudioSource> ();
     	
	}
	
	void Update() {

		if (MuteMusic.isOn) {
			musicSound.volume = MusicSlider.value;
			} else {
				musicSound.volume = 0.0f;
			}
		FxVolume = FxSlider.value;
		FxNotMuted = MuteFx.isOn;
		

	}
	// Update is called once per frame
    // late update runs after update, so we have updated values of this frame to work with instead of values from prev frame. 
	void LateUpdate () {
        //transform.position = player.transform.position + offset;
        if (player.transform.position.y >= -123.0f && player.transform.position.y <= 120.0f) {
        	transform.position = new Vector3(0.0f, player.transform.position.y, -1.0f);
        }
        
	}
}
