
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class facebook_init : MonoBehaviour {
	
	public GameObject DialogLoggedIn;
	public GameObject DialogLoggedOut;
	public GameObject DialogUsername;
	public GameObject DialogProfilePic;
	public GameObject Btn;
	public string myemail;
	public string myname;
	public Sprite noimage;
	public GameObject main_panel,tutorial,audsrc;
	public GameObject start_panel,settingspanel;

	void Awake()
	{
		FB.Init (SetInit, OnHideUnity);
	}

	void SetInit()
	{

		if (FB.IsLoggedIn) {
             Debug.Log ("FB is logged in");



		} else {
			Debug.Log ("FB is not logged in");
		}

		DealWithFBMenus (FB.IsLoggedIn);

	}

	void OnHideUnity(bool isGameShown)
	{

		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

	}

	public void FBlogin()
	{

		List<string> permissions = new List<string> ();
		permissions.Add ("public_profile");
		permissions.Add ("email");

		FB.LogInWithReadPermissions (permissions, AuthCallBack);
	}

	public void FBlogout()
	{
		settingspanel.SetActive (false);
		main_panel.SetActive (false);
		start_panel.SetActive (true);
		if (FB.IsLoggedIn) 
		{
			FB.LogOut ();
			Debug.Log ("FB is logged out");
			Text username = DialogUsername.GetComponent<Text> ();
			username.text = "";
			Image userimage = DialogProfilePic.GetComponent<Image > ();
			userimage.sprite = noimage;
			Button connect = Btn.GetComponent<Button> ();
			connect.GetComponentInChildren<Text>().text = "Disconnected";

		}
	}


	void AuthCallBack(IResult result)
	{

		if (result.Error != null) {
			Debug.Log (result.Error);
		} else {
			if (FB.IsLoggedIn) {
				Debug.Log ("FB is logged in");

			} else {
				Debug.Log ("FB is not logged in");
			}

			DealWithFBMenus (FB.IsLoggedIn);
		}

	}

	void DealWithFBMenus(bool isLoggedIn)
	{

		if (isLoggedIn) {
			DialogLoggedIn.SetActive (true);
			DialogLoggedOut.SetActive (false);
			if (PlayerPrefs.GetString ("playerstatus") == "0") {
				main_panel.SetActive (true);
			} else {
				tutorial.SetActive (true);
			}
			audsrc.SetActive (true);
			start_panel.SetActive(false);


			FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
			FB.API ("/me?fields=email", HttpMethod.GET, Display_ID);
		} else {
			DialogLoggedIn.SetActive (false);
			DialogLoggedOut.SetActive (true);
		}

	}

	void DisplayUsername(IResult result)
	{
		//change button text
		Button connect = Btn.GetComponent<Button> ();
		connect.GetComponentInChildren<Text>().text = "Connected";

		Text UserName = DialogUsername.GetComponent<Text> ();

		if (result.Error == null) {
			
			UserName.text = (string)result.ResultDictionary ["first_name"];
			myname = (string)result.ResultDictionary ["first_name"];
			PlayerPrefs.SetString ("helpname",myname);

		} else {
			Debug.Log (result.Error);
		}

	}

	void DisplayProfilePic(IGraphResult result)
	{

		if (result.Texture != null) {

			Image ProfilePic = DialogProfilePic.GetComponent<Image> ();

			ProfilePic.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());

		}

	}

	void Display_ID(IGraphResult Id)
	{
		if (Id.Error == null) {


			myemail = (string)Id.ResultDictionary["email"];
			PlayerPrefs.SetString ("helpmail",myemail);
			StartCoroutine (CheckPlayerData());
		} else
			Debug.Log (Id.Error); 


	}

	IEnumerator CheckPlayerData(){
		string emailtocheck = PlayerPrefs.GetString ("helpmail");
		using (UnityWebRequest www = UnityWebRequest.Get ("https://tiotsu.herokuapp.com/playerdatacheck/" + emailtocheck)) {
			yield return www.Send ();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				Debug.Log (www.downloadHandler.text);
				PlayerPrefs.SetString ("playerstatus",www.downloadHandler.text);
				yield return new WaitForSeconds (2);
				StopCoroutine (CheckPlayerData ());
			}
		}
	}

	void Start(){
		DealWithFBMenus (FB.IsLoggedIn);
	}










}