using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

	public GameObject Button,shopobj;
	string new1,new2;
	bool help;

	void OnCollisionEnter (Collision col)
	{
		/*int number;
		var a = int.TryParse(col.gameObject.name, out number); 
		if(a){
			Button.SetActive(true);
		}*/

		if (col.gameObject.tag == "playerbase"&&help) {
			Button.SetActive(true);
		}
		if(col.gameObject.tag == "dealer"){
			shopobj.SetActive (true);
		}

	}
	void OnCollisionStay (Collision col)
	{
		/*int number;
		var a = int.TryParse(col.gameObject.name, out number); 
		if(a){
			Button.SetActive(true);
		}*/

		if (col.gameObject.tag == "playerbase"&&help) {
			Button.SetActive(true);
		}
		if(col.gameObject.tag == "dealer"){
			shopobj.SetActive (true);
		}

	}

	void OnCollisionExit (Collision col2)
	{
		
		if (col2.gameObject.tag == "playerbase") {
			Button.SetActive(false);
		}
		if(col2.gameObject.tag == "dealer"){
			shopobj.SetActive (false);
		}

	}




	// Use this for initialization
	void Start () {
		new1 = PlayerPrefs.GetString ("helpmail");
		new2 = PlayerPrefs.GetString ("emailtoattack");
		//Button = GameObject.Find("/PCanvas/Attack");
		
	}
	
	// Update is called once per frame
	void Update () {
		new1 = PlayerPrefs.GetString ("helpmail");
		new2 = PlayerPrefs.GetString ("emailtoattack");
		if (new1 != new2) {
			help = true;
		}else{
			help = false;
		}
	}
}
