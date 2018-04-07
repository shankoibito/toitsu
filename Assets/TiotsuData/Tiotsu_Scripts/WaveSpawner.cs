using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public Transform attackerPrefab;
	public Transform attackerPrefab2;
	public Transform attackerPrefab3;
	public Transform attackerPrefab4;
	public Transform SpawnPoint;
	public Transform SpawnPoint2;
	public Transform tower1loc,tower2loc;
	public int buttonarmy=1;
	public int buttonpad = 1;
	public GameObject power1, power2;
	//public float timebtwwaves = 5f;
	//private float countdown= 2f;
	//private int waveno = 1;
	//private int waveindex = 0;
	public void PadSpawnButton1(){
		buttonpad = 1;
		if (PlayerPrefs.GetInt ("armybuttonresetspawn") == 0) {
			SpawnAttacker ();
			PlayerPrefs.SetInt ("armybuttonresetspawn", 1);
		} else {
			PlayerPrefs.SetInt ("Decreasemyvalue",0);
		}
		Debug.Log (PlayerPrefs.GetInt ("armybuttonreset"));
		PlayerPrefs.SetInt ("armybuttonreset",1);
	}
	public void PadSpawnButton2(){
		buttonpad = 2;
		if (PlayerPrefs.GetInt ("armybuttonresetspawn") ==0) {
			SpawnAttacker ();
			PlayerPrefs.SetInt ("armybuttonresetspawn",1);
		}else {
			PlayerPrefs.SetInt ("Decreasemyvalue",0);
		}
		Debug.Log (PlayerPrefs.GetInt ("armybuttonreset"));
		PlayerPrefs.SetInt ("armybuttonreset",1);
	}

	public void Army1 (){
		buttonarmy = 1;
	}
	public void Army2 (){
		buttonarmy = 2;
	}
	public void Power1(){
		buttonarmy = 3;
	}
	public void Power2(){
		buttonarmy = 4;
	}
	void Start () {
		PlayerPrefs.SetInt ("helpspawn",1);
		PlayerPrefs.SetInt ("helpspawn2",1);
		PlayerPrefs.SetInt ("armybuttonreset",0);

	}

	void Update () {
		/*if (countdown <= 0f){
			StartCoroutine(SpawnWave ());
			countdown = timebtwwaves;
		}
		countdown -= Time.deltaTime;*/
	}

	/*IEnumerator SpawnWave(){
		Debug.Log ("Wave Incoming!");
		//waveindex++;
		for(int i=0 ; i<waveindex;i++){
			SpawnAttacker ();
			yield return new WaitForSeconds (0.5f);
		}
	}*/

	public void SpawnAttacker(){
		if (buttonpad == 1) {
			if (buttonarmy == 1) {
				Instantiate (attackerPrefab, SpawnPoint.position, SpawnPoint.rotation);
				//ForkParticlePlugin.Instance.Test();
				PlayerPrefs.SetInt ("Decreasemyvalue",30);
			} else if(buttonarmy == 2) {
				Instantiate (attackerPrefab2, SpawnPoint.position, SpawnPoint.rotation);
				ForkParticlePlugin.Instance.Test();
				PlayerPrefs.SetInt ("Decreasemyvalue",40);
			} else if(buttonarmy == 3){
				GameObject power1eff = (GameObject)Instantiate (power1,tower1loc.position,tower1loc.rotation);
				Destroy (power1eff,6f);
				PlayerPrefs.SetInt ("Decreasemyvalue",25);
			}else if(buttonarmy == 4){
				GameObject power2eff = (GameObject)Instantiate (power2,tower1loc.position,tower1loc.rotation);
				Destroy (power2eff,4f);
				PlayerPrefs.SetInt ("Decreasemyvalue",20);
			}
		} else {
			if (buttonarmy == 1) {
				Instantiate (attackerPrefab3, SpawnPoint2.position, SpawnPoint2.rotation);
				PlayerPrefs.SetInt ("Decreasemyvalue",30);
			} else if(buttonarmy == 2) {
				Instantiate (attackerPrefab4, SpawnPoint2.position, SpawnPoint2.rotation);
				PlayerPrefs.SetInt ("Decreasemyvalue",40);
			}else if(buttonarmy == 3){
				GameObject power1eff = (GameObject)Instantiate (power1,tower2loc.position,tower2loc.rotation);
				Destroy (power1eff,6f);
				PlayerPrefs.SetInt ("Decreasemyvalue",25);
			}else if(buttonarmy == 4){
				GameObject power2eff = (GameObject)Instantiate (power2,tower2loc.position,tower2loc.rotation);
				Destroy (power2eff,4f);
				PlayerPrefs.SetInt ("Decreasemyvalue",20);
			}
		}

	}
}
