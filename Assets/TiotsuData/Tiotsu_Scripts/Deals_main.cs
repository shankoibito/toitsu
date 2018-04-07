using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Deals_main : MonoBehaviour {

	public Text myyunk;
	public GameObject sorryearnmore,congo;
	int myyunks;
	string mail;
	string houselevel;

	// Use this for initialization
	void Start () {
		myyunk.text = PlayerPrefs.GetString("myyunk");
		int.TryParse (myyunk.text, out myyunks);
		mail = PlayerPrefs.GetString ("helpmail");
	}

	public void buydeal1house2(){
		if (myyunks > 3000&&PlayerPrefs.GetString("myhouselevel")=="1") {
			myyunks = myyunks - 3000;
			houselevel = "2";
			StartCoroutine (UploadDealTaskData());
			PlayerPrefs.SetString ("myyunk",myyunks.ToString());
			congo.SetActive (true);

		} else {
			sorryearnmore.SetActive (true);
		}
	}

	IEnumerator UploadDealTaskData() {
		WWWForm GameDealsForm = new WWWForm();
		GameDealsForm.AddField("mymail",mail  );
		GameDealsForm.AddField("yunk", myyunks.ToString());
		GameDealsForm.AddField("houselevel",houselevel);

		UnityWebRequest www = UnityWebRequest.Post("https://tiotsu.herokuapp.com/dealsdataupdate", GameDealsForm);
		yield return www.Send();


		if (www.isNetworkError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Done");
			StopCoroutine (UploadDealTaskData());
		}

	}
	
	// Update is called once per frame
	void Update () {
		myyunk.text = PlayerPrefs.GetString("myyunk");
	}
}
