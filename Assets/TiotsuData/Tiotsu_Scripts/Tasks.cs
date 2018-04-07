using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Tasks : MonoBehaviour {

	public GameObject player,helppanel,txt1,txt2,txt3,tskcnvs1,tskcnvs2,youlost;
	public GameObject map;
	public GameObject task1complete;
	public GameObject TaskArsenal;
	bool iam,iam2;
	public Transform playertransform;
	Vector3 oldpos, newpos;
	public Text time,ppcount;
	public Text movement;
	string min;
	string sec;
	float timer= 1200;
	float minutes;
	float seconds;
	string mail;
	int aurah,yunkh;

	public void task1help(){
		helppanel.SetActive (true);
		txt1.SetActive (true);

	}
	public void task2help(){
		helppanel.SetActive (true);
		txt2.SetActive (true);

	}
	public void task3help(){
		helppanel.SetActive (true);
		txt3.SetActive (true);

	}
	public void cross(){
		txt1.SetActive (false);
		txt2.SetActive (false);
		txt3.SetActive (false);
		helppanel.SetActive (false);

	}

	public void task1(){
		PlayerPrefs.SetInt ("publicplace1",0);
		PlayerPrefs.SetString ("publicplace1value", "start");
		PlayerPrefs.SetInt ("publicplace2",0);
		PlayerPrefs.SetString ("publicplace2value", "start");
		PlayerPrefs.SetInt ("publicplace3",0);
		PlayerPrefs.SetString("publicplace3value","start");
		PlayerPrefs.SetInt("task1",1);
		int.TryParse (PlayerPrefs.GetString("myaura"), out aurah);
		int.TryParse (PlayerPrefs.GetString("yunkh"), out yunkh);
		tskcnvs1.SetActive (true);
		player.SetActive (true);
		map.SetActive (true);
		TaskArsenal.SetActive (false);
		
	}
		

	public void task1_complete(){
		if (PlayerPrefs.GetInt ("publicplace3") == 1) {
			task1complete.SetActive (true);
			PlayerPrefs.SetInt("task1",0);
			PlayerPrefs.SetInt ("publicplace1",0);
			PlayerPrefs.SetInt ("publicplace2",0);
			PlayerPrefs.SetInt ("publicplace3",0);
			ppcount.text="0";
			StartCoroutine (UploadGameTaskData());
		}
	}

	public void task2(){
		PlayerPrefs.SetInt("task2",1);
		int.TryParse (PlayerPrefs.GetString("myaura"), out aurah);
		int.TryParse (PlayerPrefs.GetString("yunkh"), out yunkh);
		player.SetActive (true);
		tskcnvs2.SetActive (true);
		map.SetActive (true);
		TaskArsenal.SetActive (false);
	}

	public void task2_complete(){
		if (PlayerPrefs.GetInt ("task2") == 1) {
			oldpos = newpos;
			newpos = playertransform.position;
			if (Mathf.Abs (newpos.z - oldpos.z) + Mathf.Abs (newpos.x - oldpos.x) > 0.001) {
				movement.text = "";
				timer -= Time.deltaTime;
				if (timer > 0) {
					timechange ();
				} else {
					PlayerPrefs.SetInt ("task2", 0);
					StartCoroutine (UploadGameTaskData ());
					task1complete.SetActive (true);
				}
				time.text = min + ":" + sec;
			} else {
				movement.text = "No Movement Detected";
			} 
			if (timer <= 0) {
				youlost.SetActive (true);
			}
		}
	}

	void timechange(){
		minutes = Mathf.Floor(timer/60);
		seconds = timer%60;
		if (minutes < 10) {
			min = "0" + minutes.ToString ();
		} else {
			min = minutes.ToString ();
		}
		if (seconds <= 9) {
			sec = "0" + Mathf.RoundToInt (seconds).ToString ();
		} else {
			sec = Mathf.RoundToInt (seconds).ToString ();
		}
	}
	public void task3(){
		PlayerPrefs.SetInt("task3",1);
		PlayerPrefs.SetInt("task3attack1",0);
		PlayerPrefs.SetInt("task3attack2",0);
		
	}

	public void task4(){
		PlayerPrefs.SetInt("task4",1);
		
	}

	public void task5(){
		PlayerPrefs.SetInt("task5",1);
		
	}
	// Use this for initialization


	void Start () {
		StartCoroutine (GetMyAura());
		timer = 1200;
		ppcount.text="0";
		iam = true;
		iam2 = true;
		PlayerPrefs.SetInt("task1",0);
		PlayerPrefs.SetInt("task2",0);
		PlayerPrefs.SetInt("task3",0);
		PlayerPrefs.SetInt("task4",0);
		PlayerPrefs.SetInt("task5",0);
		newpos.Set (0,0,0);
		time.text = "";
		movement.text = "";


		mail = PlayerPrefs.GetString ("helpmail");
	}

	IEnumerator UploadGameTaskData() {
		WWWForm GameTaskForm = new WWWForm();
		GameTaskForm.AddField("mymail",mail  );
		GameTaskForm.AddField("yunk", (yunkh+Random.Range(100,200)).ToString());
		GameTaskForm.AddField("aura", (aurah+Random.Range(400,600)).ToString());

		UnityWebRequest www = UnityWebRequest.Post("https://tiotsu.herokuapp.com/windataupdate", GameTaskForm);
		yield return www.Send();


		if (www.isNetworkError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Done");
			StopCoroutine (UploadGameTaskData());
		}

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
		
	
	// Update is called once per frame
	void Update () {
		task1_complete ();
		task2_complete ();
		if(PlayerPrefs.GetInt("task1")==1){
			if(PlayerPrefs.GetInt("publicplace1")==1&&iam){
				ppcount.text = "1";
				iam = false;
			}else if(PlayerPrefs.GetInt("publicplace2")==1&&iam2){
				ppcount.text = "2";
				iam2 = false;
			}else if(PlayerPrefs.GetInt("publicplace3")==1){
				ppcount.text = "3";
			}
		}
	}
}
