using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioONOFF : MonoBehaviour {

	public AudioListener audiolistener;
	public List<AudioSource> audioSource;
	bool playpause;

	void Start(){
		playpause = true;
	}

	public void ONbutton(){
		PlayerPrefs.SetInt ("audiovalue",1);
		playpause = true;
	}
	public void OFFbutton(){
		PlayerPrefs.SetInt ("audiovalue",0);
		playpause = true;
	}

	void audioonoff(int value){
		if (value == 0) {
			foreach(AudioSource audiosource in audioSource){
				audiosource.Pause();
			}
		}else{
			foreach(AudioSource audiosource in audioSource){
				audiosource.Play ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playpause) {
			if (PlayerPrefs.GetInt ("audiovalue") == 1) {
				audioonoff (1);
			} else {
				audioonoff(0);
			}
			playpause = false;
		}
	}
}
