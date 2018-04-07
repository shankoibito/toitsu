using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameWinLooseCheck : MonoBehaviour {
	public GameObject win;
	public GameObject lost;
	public GameObject ReturnButton;
	public GameObject task3complete;
	public GameObject MainUI;
	public Text myauratxt,lootauratxt,lootyunkstxt,aurausedtxt,lootauratxt2,lootyunkstxt2,aurausedtxt2;
	int lootaura;
	int lootyunk;
	int result;
	int myaura;
	int prematchaura;
	int myyunk;
	string mail;
	int yunk;
	int aura;
	bool nomoreupdateaurayunkval;
	AudioSource audsrcgamewinloose;
	public AudioClip winaud,looseaud;


	void CheckWin(){
		if (PlayerPrefs.GetInt ("Tower1") == 1 && PlayerPrefs.GetInt ("Tower2") == 1) {
			nomoreupdateaurayunkval = true;
			Time.timeScale = 0;
			aura = myaura + lootaura;
			yunk = myyunk + lootyunk;
			PlayerPrefs.SetString ("myaura",aura.ToString());
			PlayerPrefs.SetString ("myyunk",yunk.ToString());
			mail = PlayerPrefs.GetString ("helpmail");
			StartCoroutine( UploadGameLooseWinData ());
			defencelost ();
			PlayerPrefs.SetInt ("Tower1",0);
			PlayerPrefs.SetInt ("Tower2",0);
			win.SetActive (true);
			aurausedtxt.text = (prematchaura - myaura).ToString ();
			lootauratxt.text = lootaura.ToString();
			lootyunkstxt.text = lootyunk.ToString();
			if (MainUI.activeSelf&&win.activeSelf) {
				audsrcgamewinloose.PlayOneShot (winaud,0.8f);
			}
			MainUI.SetActive (false);
			task3operations ();
			ReturnButton.SetActive (true);
		} else if ((PlayerPrefs.GetInt ("Tower1") == 1&&PlayerPrefs.GetInt ("gametimer")==0) || PlayerPrefs.GetInt ("Tower2") == 1&&PlayerPrefs.GetInt ("gametimer")==0) {
			nomoreupdateaurayunkval = true;
			Time.timeScale = 0;
			aura = myaura + lootaura/2;
			yunk = myyunk + lootyunk/2;
			PlayerPrefs.SetString ("myaura",aura.ToString());
			PlayerPrefs.SetString ("myyunk",yunk.ToString());
			mail = PlayerPrefs.GetString ("helpmail");

			StartCoroutine( UploadGameLooseWinData ());
			defencelost ();
			PlayerPrefs.SetInt ("Tower1",0);
			PlayerPrefs.SetInt ("Tower2",0);
			win.SetActive (true);
			aurausedtxt.text = (prematchaura - myaura).ToString ();
			lootauratxt.text = (lootaura/2).ToString();
			lootyunkstxt.text = (lootyunk/2).ToString();
			if (MainUI.activeSelf&&win.activeSelf) {
				audsrcgamewinloose.PlayOneShot (winaud,0.8f);
			}
			MainUI.SetActive (false);
			task3operations ();
			ReturnButton.SetActive (true);
		} else if(PlayerPrefs.GetInt ("gametimer")==0) {
			nomoreupdateaurayunkval = true;
			Time.timeScale = 0;
			mail = PlayerPrefs.GetString ("helpmail");
			StartCoroutine( UploadGameLooseWinData ());
			PlayerPrefs.SetInt ("Tower1",0);
			PlayerPrefs.SetInt ("Tower2",0);
			aurausedtxt2.text = (prematchaura - myaura).ToString ();
			lootauratxt2.text = (lootaura/4).ToString();
			lootyunkstxt2.text = (lootyunk/4).ToString();
			lost.SetActive (true);
			if (MainUI.activeSelf&&lost.activeSelf) {
				audsrcgamewinloose.PlayOneShot (looseaud,0.8f);
			}
			MainUI.SetActive (false);
			ReturnButton.SetActive (true);
		}
	}
	// Use this for initialization
	void Start () {
		nomoreupdateaurayunkval = false;
		audsrcgamewinloose = GetComponent<AudioSource> ();
		int loota = 0, looty =0 ,mya=0 , myy=0;
		int.TryParse(PlayerPrefs.GetString ("auraattack"),out loota);
		lootaura = loota;
		int.TryParse(PlayerPrefs.GetString ("yunkattack"),out looty);
		lootyunk = looty;
		int.TryParse (PlayerPrefs.GetString("myaura"),out mya);
		myaura = mya;
		prematchaura = mya;
		int.TryParse (PlayerPrefs.GetString("myyunk"),out myy);
		myyunk = myy;
	}

	public void Returnmain (int SceneIndex) {
		Time.timeScale = 1;
		StartCoroutine (LoadAsynchronously(SceneIndex));
	}

	IEnumerator UploadGameLooseWinData() {
		WWWForm GameLooseWinForm = new WWWForm();
		GameLooseWinForm.AddField("mymail",mail  );
		GameLooseWinForm.AddField("yunk", yunk);
		GameLooseWinForm.AddField("aura", aura );

		UnityWebRequest www = UnityWebRequest.Post("https://tiotsu.herokuapp.com/windataupdate", GameLooseWinForm);
		yield return www.Send();


		if (www.isNetworkError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Done");
			StopCoroutine (UploadGameLooseWinData());
		}

	}
	void defencelost(){
		mail = PlayerPrefs.GetString ("emailtoattack");
		aura = lootaura - lootaura / 4;
		yunk = lootyunk - lootyunk / 4;
		PlayerPrefs.SetString ("myaura",aura.ToString());
		PlayerPrefs.SetString ("myyunk",yunk.ToString());
		StartCoroutine( UploadGameLooseWinData ());
	}
	public GameObject loadingscreen;
	public Slider slider;
	public Text progresstext;


	IEnumerator LoadAsynchronously(int SceneIndex){
		AsyncOperation operation = SceneManager.LoadSceneAsync (SceneIndex);
		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress/.9f);
			loadingscreen.SetActive (true);
			slider.value = progress;
			Debug.Log (operation.progress);
			progresstext.text = progress * 100 + "%";

			yield return null;
		}
	}
	public void task3operations(){
		if (PlayerPrefs.GetInt ("task3") == 1 && PlayerPrefs.GetInt ("task3attack1") == 0) {
			PlayerPrefs.SetInt ("task3attack1", 1);
		} else if (PlayerPrefs.GetInt ("task3") == 1 && PlayerPrefs.GetInt ("task3attack1") == 1) {
			PlayerPrefs.SetInt ("task3attack2", 1);
		}

		if (PlayerPrefs.GetInt ("task3attack1") == 1 && PlayerPrefs.GetInt ("task3attack2") == 1) {
			audsrcgamewinloose.PlayOneShot (winaud,0.8f);
			task3complete.SetActive (true);
			PlayerPrefs.SetInt("task3",1);
			StartCoroutine (UploadGameTaskData ());
		}
	}

	IEnumerator UploadGameTaskData() {
		WWWForm GameTaskForm = new WWWForm();
		GameTaskForm.AddField("mymail",mail  );
		GameTaskForm.AddField("yunk", (yunk+Random.Range(100,200)).ToString());
		GameTaskForm.AddField("aura", (aura+Random.Range(400,600)).ToString());

		UnityWebRequest www = UnityWebRequest.Post("https://tiotsu.herokuapp.com/windataupdate", GameTaskForm);
		yield return www.Send();


		if (www.isNetworkError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Done");
			StopCoroutine (UploadGameTaskData());
		}

	}
	
	// Update is called once per frame
	void Update () {
		CheckWin ();
		int.TryParse (myauratxt.text, out result);
		if(result<=0){
			PlayerPrefs.SetInt ("gametimer",0);
		}
		if (!nomoreupdateaurayunkval) {
			int loota = 0, looty =0 ,mya=0 , myy=0;
			int.TryParse(PlayerPrefs.GetString ("auraattack"),out loota);
			lootaura = loota;
			int.TryParse(PlayerPrefs.GetString ("yunkattack"),out looty);
			lootyunk = looty;
			int.TryParse (PlayerPrefs.GetString("myaura"),out mya);
			myaura = mya;
			int.TryParse (PlayerPrefs.GetString("myyunk"),out myy);
			myyunk = myy;
		}
	}
}
