using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpLocationCheck : MonoBehaviour {

	public GameObject locationPanel,networkpanel;

	IEnumerator Start()
	{
		locationcheck ();
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
			
	}



	IEnumerator locationcheck(){
		if (!Input.location.isEnabledByUser) {
			print ("Unable to determine device location");
			locationPanel.SetActive (true);
			yield break;
		} else {
			locationPanel.SetActive (false);
			Debug.Log ("loc works");
		}
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine( locationcheck ());
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			Debug.Log ("Error. Check internet connection!");
			networkpanel.SetActive (true);
		} else {
			networkpanel.SetActive (false);
		}
	}
}
