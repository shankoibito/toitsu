using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Tower1",0);
		PlayerPrefs.SetInt ("Tower2",0);
		StartCoroutine (CheckPlayerData());
		
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
				StopCoroutine (CheckPlayerData ());
			}
		}
	}
	
	// Update is called once per frame
}
