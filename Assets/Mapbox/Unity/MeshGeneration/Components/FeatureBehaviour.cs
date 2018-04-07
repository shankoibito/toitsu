namespace Mapbox.Unity.MeshGeneration.Components
{
	using UnityEngine;
	using System.Linq;
	using Mapbox.Unity.MeshGeneration.Data;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine.Networking;

	public class FeatureBehaviour : MonoBehaviour
	{
		private Transform target;
		//public GameObject attackbutton;
		public float range = 10.0f;
		public float range2 = 5.0f;
		public string emailattack;
		public int Helpint;
		public string myemail;
		public string helpmail;


		// Use this for initialization
		void Start () {
			//attackbutton = GameObject.Find("/Player/Attack");
			InvokeRepeating ("UpdateTarget",0f,0.5f);

			Helpint = 1;
		}


		void UpdateTarget(){
			GameObject[] attacker = GameObject.FindGameObjectsWithTag ("Player");
			float shortestdistance = Mathf.Infinity;
			GameObject NearestAttacker= null;

			foreach (GameObject attack in attacker) {
				float distanceToAttacker = Vector3.Distance (transform.transform.position ,attack.transform.position);
				if (distanceToAttacker < shortestdistance) {
					shortestdistance = distanceToAttacker;
					NearestAttacker = attack;
				}
			}
			if (NearestAttacker != null && shortestdistance <= range) {
				target = NearestAttacker.transform;
				//attackbutton.SetActive(true);
				if (gameObject.tag == "playerbase") {
					ShowDebugData ();
					List<string> attacklist = new List<string>(DataString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
					emailattack = attacklist[2];
					PlayerPrefs.SetString ("emailtoattack",emailattack);
					Debug.Log (emailattack);
					helpmail = PlayerPrefs.GetString ("helpmail");
					StartCoroutine (GetMyAura());
					StartCoroutine (GetEnemyEmail ());
					//StartCoroutine (UploademailattackData ());
				}else if(gameObject.tag == "PublicPlace"){
					if(PlayerPrefs.GetInt("task1")==1){	
						ShowDebugData ();
						List<string> publicplacedata = new List<string>(DataString.Split(new string[] { "\r\n" }, StringSplitOptions.None));
						string nameofplace= publicplacedata[4];
						if (PlayerPrefs.GetInt ("publicplace1") == 0) {
							PlayerPrefs.SetString ("publicplace1value", nameofplace);
							PlayerPrefs.SetInt ("publicplace1", 1);

						} else if (PlayerPrefs.GetInt ("publicplace2") == 0&&nameofplace!=PlayerPrefs.GetString("publicplace1value")) {
							PlayerPrefs.SetString ("publicplace2value", nameofplace);
							PlayerPrefs.SetInt ("publicplace2", 1);

						} else if (nameofplace!=PlayerPrefs.GetString("publicplace1value") &&nameofplace!=PlayerPrefs.GetString("publicplace2value")){
							PlayerPrefs.SetString ("publicplace3value", nameofplace);
							PlayerPrefs.SetInt ("publicplace3", 1);

						}

					}
				}
			}else {
				target = null;
			}
		}

		IEnumerator UploademailattackData() {
			WWWForm EmailAttackForm = new WWWForm();
			EmailAttackForm.AddField("emailattack", emailattack );

			UnityWebRequest www = UnityWebRequest.Post("https://tiotsu.herokuapp.com/tiotsudataget", EmailAttackForm);
			yield return www.Send();


			if (www.isNetworkError) {
				Debug.Log (www.error);
			} else {
				Debug.Log ("Done");
				StopCoroutine (UploademailattackData());
			}

		}

		IEnumerator GetEnemyEmail(){
			string emailtoattack=PlayerPrefs.GetString("emailtoattack");
			using (UnityWebRequest www = UnityWebRequest.Get ("https://tiotsu.herokuapp.com/tiotsudatasend/"+emailtoattack)) {
				yield return www.Send ();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log (www.error);
				} else {
					Debug.Log (www.downloadHandler.text);
					List<string> attackdata = new List<string>(www.downloadHandler.text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
					PlayerPrefs.SetString("auraattack",attackdata[0]);
					PlayerPrefs.SetString("nameattack",attackdata[1]);
					PlayerPrefs.SetString("yunkattack",attackdata[2]);
					PlayerPrefs.SetString("houselevelattack",attackdata[3]);

					StopCoroutine (GetEnemyEmail());
				}
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
					List<string> mydata = new List<string>(www.downloadHandler.text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
					PlayerPrefs.SetString("myaura",mydata[0]);
					PlayerPrefs.SetString("myyunk",mydata[2]);

					StopCoroutine (GetMyAura());
				}
			}

		}

		// Update is called once per frame
		void Update () {

		}

		void OnDrawGizmosSelected(){
			if (gameObject.tag == "playerbase") {
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere (transform.position, range);
			}
			if (gameObject.tag == "PublicPlace") {
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere (transform.position, range2);
			}
		}
		public Transform Transform { get; set; }
		public VectorFeatureUnity Data;

		public void ShowDebugData()
		{
			//DataString = string.Join("\r\n", Data.Properties.Select(x => x.Key + " - " + x.Value.ToString()).ToArray());
			DataString = string.Join("\r\n", Data.Properties.Select(x => /*x.Key + " - " + */x.Value.ToString()).ToArray());
		}

		[Multiline(5)]
		public string DataString;

		public void Init(VectorFeatureUnity feature)
		{
			Transform = transform;
			Data = feature;
		}
	}
}