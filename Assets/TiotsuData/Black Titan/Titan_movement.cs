//This script will be applied to every prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Titan_movement : MonoBehaviour {

	public Animator anim;
	public Transform go;
	AudioSource audsrcanim;
	bool sound;
	public AudioClip attacksound1,attacksound2;

	void Start () {
		anim = GetComponent<Animator> ();
		go = GetComponent <Transform> ();
		audsrcanim = GetComponent<AudioSource> ();

	}

	IEnumerator attacksound(){
		if(gameObject.tag=="attacker"){
			audsrcanim.PlayOneShot (attacksound1,0.9f);
			yield return new WaitForSeconds (1.4f);
			sound = true;
		}
		if(gameObject.tag=="attacker2"){
			audsrcanim.PlayOneShot (attacksound2,1f);
			yield return new WaitForSeconds (1.6f);
			sound = true;
		}



	}

	void Update () {

		if (go.transform.position.z  > 8.3)
		{	
			if(anim.GetBool("IsWalking")||sound){
				StartCoroutine (attacksound());
				sound = false;
			}	
			anim.SetBool ("IsWalking", false);//player will start to attack
			if((PlayerPrefs.GetInt ("Tower1") == 1)||(PlayerPrefs.GetInt ("Tower2") == 1)){
				
			}
			
		}




	}
}
