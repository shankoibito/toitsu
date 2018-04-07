using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCharacters : MonoBehaviour {
	public float speed = 10f;
	public float StartHealth = 100f;
	public float Health;
	private Transform target;
	private int wavepointindex;
	AudioSource audsrcplyrcry;
	public GameObject fx;
	public AudioClip plyrcry;
	[Header("Unity Stuff")]

	public Image healthBar;

	void Start(){
		audsrcplyrcry = GetComponent<AudioSource> ();
		//target =waypoints.points[1];
		if (PlayerPrefs.GetInt ("helpspawn") == 1) {
			target = waypoints.points [1];
			PlayerPrefs.SetInt ("helpspawn",2);
		} else if (PlayerPrefs.GetInt ("helpspawn") == 2) {
			target = waypoints.points [3];
			PlayerPrefs.SetInt ("helpspawn",3);
		}else if (PlayerPrefs.GetInt ("helpspawn") == 3) {
			target = waypoints.points [5];
			PlayerPrefs.SetInt ("helpspawn",4);
		} else {
			target = waypoints.points [7];
			PlayerPrefs.SetInt ("helpspawn",1);
		}
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
		if (target != null) {
			Vector3 dir = target.position - transform.position;
			transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

			if (Vector3.Distance (transform.position, target.position) <= 0.4f) {
				//GetNextWayPoint ();
			}
		} else {
			if (PlayerPrefs.GetInt ("helpspawn") == 1) {
				target = waypoints.points [1];
				PlayerPrefs.SetInt ("helpspawn",2);
			} else if (PlayerPrefs.GetInt ("helpspawn") == 2) {
				target = waypoints.points [3];
				PlayerPrefs.SetInt ("helpspawn",3);
			}else if (PlayerPrefs.GetInt ("helpspawn") == 3) {
				target = waypoints.points [5];
				PlayerPrefs.SetInt ("helpspawn",4);
			} else {
				target = waypoints.points [6];
				PlayerPrefs.SetInt ("helpspawn",1);
			}
			Debug.Log (PlayerPrefs.GetInt ("helpspawn"));
			target = waypoints.points [wavepointindex];
			Health = StartHealth;
		}
		if (gameObject.transform.position.z > -17) {
			Destroy (fx);
		}
	}

	void GetNextWayPoint(){
		if (wavepointindex >= waypoints.points.Length - 1) {
			//Destroy (gameObject);
			return;
		}
		wavepointindex++;
		wavepointindex++;
		target = waypoints.points [wavepointindex];
	}
}
