using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCharacters2 : MonoBehaviour {
	public float speed = 10f;
	public float StartHealth = 100f;
	public float Health;
	AudioSource audsrcplyrcry;
	public AudioClip plyrcry;
	public GameObject fx;
	private Transform target2;
	private int wavepointindex2;

	[Header("Unity Stuff")]

	public Image healthBar;

	void Start(){
		audsrcplyrcry = GetComponent<AudioSource> ();
		if (PlayerPrefs.GetInt ("helpspawn2") == 1) {
			target2 = waypoints.points [0];
			Debug.Log ("done");
			PlayerPrefs.SetInt ("helpspawn2",2);
		} else if (PlayerPrefs.GetInt ("helpspawn2") == 2) {
			target2 = waypoints.points [2];
			Debug.Log ("done");
			PlayerPrefs.SetInt ("helpspawn2",3);
		}else if (PlayerPrefs.GetInt ("helpspawn2") == 3) {
			target2 = waypoints.points [4];
			PlayerPrefs.SetInt ("helpspawn2",4);
		} else {
			target2 = waypoints.points [6];
			PlayerPrefs.SetInt ("helpspawn2",1);
		}
		Debug.Log (wavepointindex2);
		target2 = waypoints.points [wavepointindex2];
		//target2 =waypoints.points[0];
		Health = StartHealth;
	}

	public void TakeDamage(float amount){
		Health -= amount;

		healthBar.fillAmount = Health/StartHealth;
		audsrcplyrcry.PlayOneShot (plyrcry,0.9f);
		if(Health<=0){
			Die ();
		}
	}

	void Die(){
		Destroy (gameObject);
	}

	void Update(){
		if (target2 != null) {
			Vector3 dir = target2.position - transform.position;
			transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

			if (Vector3.Distance (transform.position, target2.position) <= 0.4f) {
				//GetNextWayPoint ();
			}
		} else {
			if (PlayerPrefs.GetInt ("helpspawn2") == 1) {
				target2 = waypoints.points [0];
				Debug.Log ("done");
				PlayerPrefs.SetInt ("helpspawn2",2);
			} else if (PlayerPrefs.GetInt ("helpspawn2") == 2) {
				target2 = waypoints.points [2];
				Debug.Log ("done");
				PlayerPrefs.SetInt ("helpspawn2",3);
			}else if (PlayerPrefs.GetInt ("helpspawn2") == 3) {
				target2 = waypoints.points [4];
				PlayerPrefs.SetInt ("helpspawn2",4);
			} else {
				target2 = waypoints.points [6];
				PlayerPrefs.SetInt ("helpspawn2",1);
			}
			Debug.Log (wavepointindex2);
			target2 = waypoints.points [wavepointindex2];
			//target2 =waypoints.points[0];
			Health = StartHealth;
		}
		if (gameObject.transform.position.z > -17) {
			Destroy (fx);
		}
	}

	void GetNextWayPoint(){
		if (wavepointindex2 >= waypoints.points.Length - 1) {
			//Destroy (gameObject);
			return;
		}
		wavepointindex2++;
		wavepointindex2++;
		target2 = waypoints.points [wavepointindex2];
	}
}
