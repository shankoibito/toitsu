using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_bg : MonoBehaviour {

	int i = 0; public float s = 0.0001f;
	void Update () {

		Vector3 pos = this.transform.position;
		if (i < 200) {
			pos.x += s;
			i++;
		} else if (i < 400) {
			pos.x -= s;
			i++;
		} else if (i < 450) {
			pos.y -= s;
			i++;
		} else if (i < 500) {
			pos.y += s;
			i++;
		} else if (i < 600) {
			pos.x -= s;
			pos.y -= s;
			i++;
		} else if (i < 700) {
			pos.x += s;
			pos.y += s;i++;
		} else {i = 0;}
		this.transform.position = pos;
	}
}
