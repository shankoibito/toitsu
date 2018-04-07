using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerDataLevelReady : MonoBehaviour {
	public Text auravalue;
	public Text yunkvalue;
	public Text myaura;
	public Text time;
	string min;
	string sec;
	float timer= 120;
	float minutes;
	float seconds;
	// Use this for initialization
	void Start () {
		timer = 150;
		auravalue.text = PlayerPrefs.GetString ("auraattack");
		yunkvalue.text = PlayerPrefs.GetString ("yunkattack");
		myaura.text = PlayerPrefs.GetString ("myaura");
		PlayerPrefs.SetInt ("gametimer",1);
	}

	void timechange(){
		minutes = Mathf.Floor(timer/60);
		seconds = timer%60;
		if (minutes < 10) {
			min = "0" + minutes.ToString();
		}
		if (seconds <= 9) {
			sec = "0" + Mathf.RoundToInt (seconds).ToString ();
		} else {
			sec = Mathf.RoundToInt (seconds).ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer > 0) {
			timechange ();
		} else {
			PlayerPrefs.SetInt ("gametimer",0);
		}
		time.text = min + ":" + sec;
		if (PlayerPrefs.GetInt ("Decreasemyvalue") == 30) {
			int helpaura = 0;
			int.TryParse (myaura.text,out helpaura);
			myaura.text = (helpaura - 30).ToString();
			PlayerPrefs.SetString("myaura", myaura.text);
			PlayerPrefs.SetInt ("Decreasemyvalue",0);
		}else if (PlayerPrefs.GetInt ("Decreasemyvalue") == 40) {
			int helpaura = 0;
			int.TryParse (myaura.text,out helpaura);
			myaura.text =( helpaura - 40).ToString();
			PlayerPrefs.SetString("myaura", myaura.text);
			PlayerPrefs.SetInt ("Decreasemyvalue",0);
		}else if (PlayerPrefs.GetInt ("Decreasemyvalue") == 25) {
			int helpaura = 0;
			int.TryParse (myaura.text,out helpaura);
			myaura.text =( helpaura - 25).ToString();
			PlayerPrefs.SetString("myaura", myaura.text);
			PlayerPrefs.SetInt ("Decreasemyvalue",0);
		}else if (PlayerPrefs.GetInt ("Decreasemyvalue") == 20) {
			int helpaura = 0;
			int.TryParse (myaura.text,out helpaura);
			myaura.text =( helpaura - 20).ToString();
			PlayerPrefs.SetString("myaura", myaura.text);
			PlayerPrefs.SetInt ("Decreasemyvalue",0);
		}

	}
		
}
