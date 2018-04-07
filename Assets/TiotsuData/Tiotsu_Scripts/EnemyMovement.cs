using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public Transform target;
	public float speed;
	int speedhelp;

	// Use this for initialization
	void Start () {
		int.TryParse (PlayerPrefs.GetString("myyunk"),out speedhelp);

		speed = 0.25f+ (speedhelp/300000);

	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 dir = target.position - transform.position;
			transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

			if (Vector3.Distance (transform.position, target.position) <= 0.4f) {
				PlayerPrefs.SetInt ("gametimer",0);
			}
		}
	}
}
