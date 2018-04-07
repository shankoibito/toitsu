using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;

public class MapZoomAndReload : MonoBehaviour {

	BasicMap _map;
	Camera _camera;
	Vector3 _cameraStartPos;
	public Slider zoomslider;
	float oldslidervalue;



	void Awake()
	{
		_camera = Camera.main;
		_cameraStartPos = _camera.transform.position;
		_map = FindObjectOfType<BasicMap>();
	}

	public void ReloadButton(){
		PlayerPrefs.SetInt ("reloadmap",1);
	}
	void Reload(float value)
	{
		//_camera.transform.position = _cameraStartPos;
		_map.Initialize(_map.CenterLatitudeLongitude, (int)value);
	}

	// Use this for initialization
	void Start () {zoomslider.value=18;
		oldslidervalue = zoomslider.value;
	}
	
	// Update is called once per frame
	void Update () {
		if (zoomslider.value != oldslidervalue) {
			oldslidervalue = zoomslider.value;
			Reload (zoomslider.value);
		}
		if(PlayerPrefs.GetInt("reloadmap")==1){
			Reload (18);
			PlayerPrefs.SetInt ("reloadmap",0);
		}
	}
}
