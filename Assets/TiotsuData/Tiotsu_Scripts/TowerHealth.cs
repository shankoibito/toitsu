using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour {
	public Image towerhealthbar;
	public float TowerStartHealth = 10000f;
	public float Tower_Health;
	AudioSource audsrc;
	public AudioClip destoysnd;
	public Button button;
	public Button button2;
	public GameObject cross1,node;
	public GameObject cross2,node2;
	public GameObject explodeprefab;

	void OnCollisionStay(Collision col){
		if (col.gameObject.tag == "attacker") {
			TowerTakeDamage (6);
			Debug.Log (Tower_Health);
		}
		if (col.gameObject.tag == "attacker2") {
			TowerTakeDamage (8);
			Debug.Log (Tower_Health);
		}
		if (col.gameObject.tag == "attacker3") {
			TowerTakeDamage (5);
			Debug.Log (Tower_Health);
		}
		if (col.gameObject.tag == "attacker4") {
			TowerTakeDamage (3);
			Debug.Log (Tower_Health);
		}
		if(Tower_Health<=0){
			Destroy(col.collider.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		audsrc = GetComponent<AudioSource> ();
		TowerStartHealth = 5000;
		if (PlayerPrefs.GetString ("houselevelattack") == "2") {
			TowerStartHealth = 7500;
			node.SetActive (false);
			node2.SetActive (true);
		}
		Tower_Health = TowerStartHealth;
	}

	public void TowerTakeDamage(float amount){
		Tower_Health -= amount;

		towerhealthbar.fillAmount = Tower_Health/TowerStartHealth;

		if(Tower_Health<=0){
			audsrc.PlayOneShot (destoysnd, 0.9f);
			DieTower ();
		}
	}

	void DieTower(){
		if (gameObject.name == "Tower 3") {
			//button = GetComponentInChildren<Button> ();
			button.interactable = false;
			cross2.SetActive (true);
		} else if(gameObject.name == "Tower 2") {
			//button2 = GetComponentInChildren<Button> ();
			button2.interactable = false;
			cross1.SetActive (true);
		}
		GameObject explode = (GameObject)Instantiate (explodeprefab,gameObject.transform.position,gameObject.transform.rotation);
		Destroy (explode, 2.0f);
		if (PlayerPrefs.GetInt ("Tower1") == 1) {
			PlayerPrefs.SetInt ("Tower2", 1);
		} else if(PlayerPrefs.GetInt ("Tower1") == 0) {
			PlayerPrefs.SetInt ("Tower1", 1);
		}
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		towerhealthbar.fillAmount = Tower_Health/TowerStartHealth;
	}
}
