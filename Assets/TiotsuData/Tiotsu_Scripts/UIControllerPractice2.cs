using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIControllerPractice2 : MonoBehaviour {

	public Button army1_btn;
	Image army_btnnewfeature;
	public float amount;

	// Use this for initialization
	void Start () {

		Button army_btn = GetComponent<Button> ();
		army_btnnewfeature= army1_btn.GetComponentInChildren<Image> ();
		army1_btn.interactable = false;
		army_btnnewfeature.fillAmount = 0;

	}

	// Update is called once per frame
	void Update () {
		if (!army1_btn.interactable) {
			on_army1_ui (amount);
		}
		if (PlayerPrefs.GetInt ("armybuttonreset") == 1&&army1_btn.interactable) {
			army1_btn.interactable = false;
			army_btnnewfeature.fillAmount = 0;
		}

	}

	void on_army1_ui(float amt)
	{
		if (!army1_btn.interactable) {
			army_btnnewfeature.fillAmount += amt;
			if (army_btnnewfeature.fillAmount == 1) {
				army1_btn.interactable = true;
				PlayerPrefs.SetInt ("armybuttonresetspawn",0);
			} else {
				army1_btn.interactable = false;
				PlayerPrefs.SetInt ("armybuttonreset",3);
			}
		}
	}



}
