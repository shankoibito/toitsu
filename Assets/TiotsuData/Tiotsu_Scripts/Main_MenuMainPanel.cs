using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Main_MenuMainPanel : MonoBehaviour {


	// Use this for initialization
	void Start(){
		StartCoroutine (GetMyAura());
	}
	IEnumerator GetMyAura(){
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

				StopCoroutine (GetMyAura());
			}
		}

	}

}
