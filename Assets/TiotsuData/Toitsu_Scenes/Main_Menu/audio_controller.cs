 using UnityEngine;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
 public class audio_controller : MonoBehaviour {
	 
// public Slider mySlider;
 public AudioSource volumeAudio;
	public Slider mySlider;
 public void VolumeController(){
	 volumeAudio.volume = mySlider.value;
 }

	public void ON()
	{
		volumeAudio.volume = 1;


	}

	public void OFF()
	{

		volumeAudio.volume = 0;
	}

	public void Quit()
	{


		Application.Quit ();


	}
 }