using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Tutorial : MonoBehaviour {

	public GameObject rightTutimage;
	public GameObject leftTutImage;
	public GameObject txt1;
	public GameObject txt2;
	public GameObject txt3;
	public GameObject txt4;
	public GameObject txt5;
	public GameObject txt6;
	public GameObject NextButtonR;
	public GameObject NextButtonL;
	public GameObject Playmenu;


	// Use this for initialization
	void Start () {
		StartCoroutine (CheckPlayerData());
		PlayerPrefs.SetInt ("tuthelp",1);
	}

	void tutstart(){
		if (PlayerPrefs.GetString("playerstatus")=="1"){
			if (PlayerPrefs.GetInt ("tuthelp") == 1) {
				leftTutImage.SetActive (true);
				txt1.SetActive (true);
				NextButtonR.SetActive (true);
				Playmenu.SetActive (false);
			}
		}
	}

	public void NextButtonLW(){
		if (PlayerPrefs.GetInt ("tuthelp") == 4) {
			txt4.SetActive (false);
			txt5.SetActive (true);
			Playmenu.SetActive (true);
			PlayerPrefs.SetInt ("tuthelp", 5);
			NextButtonL.SetActive (false);
		}
	}

	public void playbuttontut(){
		if (PlayerPrefs.GetInt ("tuthelp") == 5) {
			txt5.SetActive (false);
			leftTutImage.SetActive (true);
			rightTutimage.SetActive (false);
			txt6.SetActive (true);
			PlayerPrefs.SetInt ("tuthelp", 6);
		}
	}

	public void NextButtonRW(){
		if (PlayerPrefs.GetInt ("tuthelp") == 1) {
			txt1.SetActive (false);
			txt2.SetActive (true);
			PlayerPrefs.SetInt ("tuthelp",2);
		}else if(PlayerPrefs.GetInt ("tuthelp") == 2){
			txt2.SetActive (false);
			txt3.SetActive (true);
			PlayerPrefs.SetInt ("tuthelp",3);
		}else if(PlayerPrefs.GetInt ("tuthelp") == 3){
			txt3.SetActive (false);
			leftTutImage.SetActive (false);
			rightTutimage.SetActive (true);
			txt4.SetActive (true);
			NextButtonL.SetActive (true);
			PlayerPrefs.SetInt ("tuthelp",4);
			NextButtonR.SetActive (false);
		}
	}

	IEnumerator CheckPlayerData(){
		string emailtocheck = PlayerPrefs.GetString ("helpmail");
		using (UnityWebRequest www = UnityWebRequest.Get ("https://tiotsu.herokuapp.com/playerdatacheck/" + emailtocheck)) {
			yield return www.Send ();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				Debug.Log (www.downloadHandler.text);
				PlayerPrefs.SetString ("playerstatus", www.downloadHandler.text);
				yield return new WaitForSeconds (2);
				StopCoroutine (CheckPlayerData ());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		tutstart ();
	}
}
