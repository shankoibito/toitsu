using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	private Transform target;

	[Header("Attributes")]
	public float range = 15f;
	public float FireRate = 1f;
	public float FireCountDown = 0f;

	[Header("Unity Setup Feilds")]
	public float turnspeed = 10f;

	public string attackertag = "attacker";
	public string attackertag2 = "attacker2";
	public Transform partToRotate;

	public GameObject bulletPrefab;
	public Transform firePoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("UpdateTarget",0f,0.5f);
	}

	void UpdateTarget(){
		GameObject[] attackers = GameObject.FindGameObjectsWithTag (attackertag);
		GameObject[] attackers2 = GameObject.FindGameObjectsWithTag (attackertag2);
		float shortestdistance = Mathf.Infinity;
		GameObject NearestAttacker= null;

		foreach (GameObject attacker in attackers) {
			float distanceToAttacker = Vector3.Distance (transform.transform.position ,attacker.transform.position);
			if (distanceToAttacker < shortestdistance) {
				shortestdistance = distanceToAttacker;
				NearestAttacker = attacker;
			}
		}
		foreach (GameObject attacker2 in attackers2) {
			float distanceToAttacker = Vector3.Distance (transform.transform.position ,attacker2.transform.position);
			if (distanceToAttacker < shortestdistance) {
				shortestdistance = distanceToAttacker;
				NearestAttacker = attacker2;
			}
		}
		if (NearestAttacker != null && shortestdistance <= range) {
			target = NearestAttacker.transform;
		} else {
			target = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null){
			return;
		}

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime*turnspeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f,rotation.y,rotation.z);


		if (FireCountDown<= 0f){
			shoot ();
			FireCountDown = 1f / FireRate;
		}

		FireCountDown -= Time.deltaTime;

	}

	void shoot(){
		Debug.Log ("Shoot");
		GameObject BulletGo = (GameObject)Instantiate (bulletPrefab,firePoint.position,firePoint.rotation);
		Bullet bullet = BulletGo.GetComponent<Bullet> ();

		if (bullet != null) {
			bullet.Seek (target);
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
