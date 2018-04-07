using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class PlayerHouse : MonoBehaviour {

	private Transform target;
	//public GameObject attackbutton;
	public float range = 15.0f;
	public string myemail;

	// Use this for initialization
	void Start () {
		//attackbutton = GameObject.Find("/Player/Attack");
		InvokeRepeating ("UpdateTarget",0f,0.5f);
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

		} else {
			target = null;
		}
	}
	void Display_ID(IGraphResult Id)
	{
		if (Id.Error == null) {

			myemail = (string)Id.ResultDictionary ["email"];
			Debug.Log (myemail);
		} else
			Debug.Log (Id.Error);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
