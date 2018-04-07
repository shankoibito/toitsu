using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerModeMainScript : MonoBehaviour {

	public GameObject AttackButton;
	public GameObject CreateNewHomeButton;
	public GameObject mainui;
	public GameObject text1,righttutimage;
	public Text aura,yunk,name,houselvl;
	// Use this for initialization
	void Start () {
		StartCoroutine (GetMyAuraandData());
		CreateNewHomeButton.SetActive (false);
		AttackButton.SetActive (false);
		if (PlayerPrefs.GetString ("playerstatus") == "1") {
			if (PlayerPrefs.GetInt ("tuthelp") == 6) {
				mainui.SetActive (false);
				righttutimage.SetActive (true);
				text1.SetActive (true);
				PlayerPrefs.SetInt ("tuthelp", 7);
			}
			CreateNewHomeButton.SetActive (true);
		} else {
			aura.text =PlayerPrefs.GetString("myaura");
			yunk.text =PlayerPrefs.GetString("myyunk");
			name.text =PlayerPrefs.GetString("helpname");
			houselvl.text = PlayerPrefs.GetString ("myhouselevel");
		}
	}
	IEnumerator GetMyAuraandData(){
		string myemail=PlayerPrefs.GetString("helpmail");
		using (UnityWebRequest www = UnityWebRequest.Get ("https://tiotsu.herokuapp.com/tiotsudatasend/"+myemail)) {
			yield return www.Send ();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				Debug.Log (www.downloadHandler.text);
				List<string> mydata = new List<string>(www.downloadHandler.text.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries));
				PlayerPrefs.SetString("myaura",mydata[0]);
				PlayerPrefs.SetString("myyunk",mydata[2]);
				PlayerPrefs.SetString("myhouselevel",mydata[3]);

				StopCoroutine (GetMyAuraandData());
			}
		}

	}

}
