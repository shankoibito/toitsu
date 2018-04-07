using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_AnimControl : MonoBehaviour {

	public Animator anim;
	public float speed = 2.0f;
	public float rotationSpeed = 75.0f;
	public Transform g;
	AudioSource audsrc;
	public AudioClip walkaud;
	Vector3 old_pos , new_pos;
	void Start () {
		audsrc = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		g = GetComponent<Transform> ();
		new_pos.Set (0,0,0);
	}
	IEnumerator walksound(){
		if (anim.GetBool ("IsWalking")) {
			audsrc.PlayOneShot (walkaud, 0.8f);
		}
		yield return new WaitForSeconds (2);
	}

	void Update () {
		old_pos = new_pos;
	     new_pos = g.transform.position;
		if (audsrc.isPlaying == false) {
			StartCoroutine (walksound ());
		}
		if (  Mathf.Abs(new_pos.z - old_pos.z ) + Mathf.Abs(new_pos.x - old_pos.x) > 0.01   ) {
			anim.SetBool ("IsWalking", true);
		} else {
			anim.SetBool ("IsWalking", false);
		}

	/*	float translation = Input.GetAxis ("Vertical") * speed;
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

		transform.Translate (0,0,translation );
		transform.Rotate (0,rotation,0);

		if (translation != 0) {
			anim.SetBool ("IsWalking", true);

		}
		else
			anim.SetBool ("IsWalking", false);*/
	}
}
