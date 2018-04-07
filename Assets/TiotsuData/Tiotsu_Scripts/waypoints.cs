using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class waypoints : MonoBehaviour {
	public static Transform[] points;

	void Awake(){
		points = new Transform[transform.childCount];
		Array.Clear (points,0,points.Length);
		for (int i = 0; i < points.Length; i++) {
			points [i] = transform.GetChild (i);
		}
	}
}
