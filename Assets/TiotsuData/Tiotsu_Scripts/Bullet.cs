using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	public float speed = 70f;
	public GameObject ImpactEffect;

	public void Seek(Transform _target){
		target = _target;
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null){
			Destroy (gameObject);
			Debug.Log("Running");
			return;
		}
		Vector3 dir = target.position - transform.position;
		float DistancethisFrame = speed * Time.deltaTime;

		if(dir.magnitude<=DistancethisFrame){
			HitTarget ();
			return;
		}
		transform.Translate (dir.normalized * DistancethisFrame, Space.World);
	}

	void HitTarget(){
		Debug.Log ("We Hit Something");
		GameObject EffectIns = (GameObject)Instantiate (ImpactEffect,transform.position,transform.rotation);
		Destroy (EffectIns,2f);
		Destroy (gameObject);
		Damage (target);
	}
	void Damage(Transform attacker){

		AttackCharacters a1 = attacker.GetComponent<AttackCharacters> ();
		AttackCharacters2 a2 = attacker.GetComponent<AttackCharacters2> ();

		if(a1!=null){
			a1.TakeDamage (10f);
		}

		if(a2!=null){
			a2.TakeDamage (10f);
		}
	}

}
