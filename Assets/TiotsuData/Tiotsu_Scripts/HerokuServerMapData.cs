using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class HerokuServerMapData : MonoBehaviour
{
	public string addDataURL = "https://tiotsu.herokuapp.com/";
	public string GeolocationData;
	public GameObject CreateHome,lefttutimage,text2,righttutimage,text1,nextrbtn,text3,text4,locationoccupied;
	public string helpmail;
	public string helpname;
	public string mylocation;
	public int yunk;
	public int aura;
	public int houselevel;
	public int level;
	public GameObject loadingscreen,vidrwimggo;
	public Slider slider;
	public Text progresstext;
	BasicMap _map;
	Camera _camera;
	Vector3 _cameraStartPos;
	public RawImage vidimgRw;
	public VideoClip videotoplay;
	private VideoPlayer vidplyr;
	private VideoSource videosrc;
	private AudioSource audiosrc;




	void Awake()
	{
		_camera = Camera.main;
		_cameraStartPos = _camera.transform.position;
		_map = FindObjectOfType<BasicMap>();
	}
	void Reload(float value)
	{
		//_camera.transform.position = _cameraStartPos;
		_map.Initialize(_map.CenterLatitudeLongitude, (int)value);
	}

	IEnumerator Start()
	{
		if (!Input.location.isEnabledByUser)
			yield break;

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out");
			yield break;
		}
		locationacceess();
	}

	public void nextbtn(){
		if (PlayerPrefs.GetInt ("tuthelp") == 8) {
			text2.SetActive (false);
			text3.SetActive (true);
			PlayerPrefs.SetInt ("tuthelp",9);
		}else if(PlayerPrefs.GetInt ("tuthelp") == 9){
			text3.SetActive (false);
			text4.SetActive (true);
			PlayerPrefs.SetInt ("tuthelp",10);
		}else if(PlayerPrefs.GetInt ("tuthelp") == 10){
			text4.SetActive (false);
			lefttutimage.SetActive (false);
			Application.runInBackground = true;
			StartCoroutine (playvid());
		}
	}
	public void Button(){
		perfomdataserveraction ();
		text1.SetActive (false);
		righttutimage.SetActive (false);
		text2.SetActive (true);
		lefttutimage.SetActive (true);
		nextrbtn.SetActive (true);
		PlayerPrefs.SetInt ("tuthelp", 8);
		CreateHome.SetActive (false);

	}

	public void LoadLevel(int SceneIndex){
		StartCoroutine (LoadAsynchronously(SceneIndex));
	}

	public void perfomdataserveraction(){
		yunk = 100;
		aura = 500;
		houselevel = 1;
		level = 1;
		helpmail = PlayerPrefs.GetString ("helpmail");
		helpname = PlayerPrefs.GetString("helpname");
		StartCoroutine(locationacceess ());
		StartCoroutine(UploadGeolocationData ());
	}



	IEnumerator locationacceess(){
		if (Input.location.status == LocationServiceStatus.Failed) {
			print ("Unable to determine device location");
			yield break;
		} else {
			// Access granted and location value could be retrieved

			mylocation = Input.location.lastData.longitude + "," + Input.location.lastData.latitude;
			//GeolocationData = "coordinates':["+Input.location.lastData.longitude+","+Input.location.lastData.latitude+"],'type':'Point"; 
			GeolocationData = "{'type': 'Feature', 'id':'"+helpmail+"', 'properties': {'name': '"+helpmail+"','houselevel': '"+houselevel+"','yunk': '"+yunk+"','aura': '"+aura+"'},'geometry': {'type':'Point','coordinates':["+Input.location.lastData.longitude+","+Input.location.lastData.latitude+"]}}";
		}
		string myemail=PlayerPrefs.GetString("helpmail");
		using (UnityWebRequest www = UnityWebRequest.Get ("https://tiotsu.herokuapp.com/tiotsulocationcheck/"+mylocation)) {
			yield return www.Send ();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				Debug.Log (www.downloadHandler.text);
				List<string> locationcheck = new List<string>(www.downloadHandler.text.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries));
				PlayerPrefs.SetString("locationcheck",locationcheck[0]);
				if (locationcheck [0] == "0") {
					locationoccupied.SetActive (true);
				}

			}
		}
			
		Input.location.Stop ();
	}
		
	IEnumerator UploadGeolocationData() {
		WWWForm GeolocationForm = new WWWForm();
		GeolocationForm.AddField("Geolocation", GeolocationData);
		print (GeolocationData);
		GeolocationForm.AddField ("mymail",helpmail);
		GeolocationForm.AddField ("username",helpname);
		GeolocationForm.AddField ("yunk",yunk);
		GeolocationForm.AddField ("aura",aura);
		GeolocationForm.AddField ("houselevel",houselevel);
		GeolocationForm.AddField ("level",level);
		GeolocationForm.AddField ("mylocation",mylocation);

		UnityWebRequest www = UnityWebRequest.Post(addDataURL, GeolocationForm);
		yield return www.Send();
		yield return new WaitForSeconds (10);
		Mapbox.Unity.MapboxAccess.Instance.ClearCache ();
		PlayerPrefs.SetInt ("reloadmap",1);


		if (www.isNetworkError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Done");
			StopCoroutine (UploadGeolocationData());
		}

	}

	IEnumerator LoadAsynchronously(int SceneIndex){
		AsyncOperation operation = SceneManager.LoadSceneAsync (SceneIndex);
		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress/.9f);
			loadingscreen.SetActive (true);
			slider.value = Mathf.RoundToInt( progress);
			Debug.Log (operation.progress);
			progresstext.text = progress * 100 + "%";

			yield return null;
		}
	}

	IEnumerator playvid(){
		vidrwimggo.SetActive (true);
		vidplyr = gameObject.AddComponent<VideoPlayer> ();
		audiosrc = gameObject.AddComponent<AudioSource> ();

		vidplyr.playOnAwake = false;
		audiosrc.playOnAwake = false;

		audiosrc.Pause ();

		vidplyr.source = VideoSource.VideoClip;

		vidplyr.audioOutputMode = VideoAudioOutputMode.AudioSource;

		vidplyr.EnableAudioTrack (0,true);
		vidplyr.SetTargetAudioSource (0,audiosrc);

		vidplyr.clip = videotoplay;
		vidplyr.Prepare ();

		WaitForSeconds waittime = new WaitForSeconds (1);
		while(!vidplyr.isPrepared){
			yield return waittime;
			break;
		}

		vidimgRw.texture = vidplyr.texture;

		vidplyr.Play ();
		audiosrc.Play ();
	}

	void Update(){
		if(PlayerPrefs.GetInt("reloadmap")==1){
			Reload (19);
			PlayerPrefs.SetInt ("reloadmap",0);
		}
		if(vidplyr.time>19){
			LoadLevel (0);
		}
	}
}