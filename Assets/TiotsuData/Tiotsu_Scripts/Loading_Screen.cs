using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_Screen : MonoBehaviour {

	public GameObject loadingscreen;
	public GameObject attckpnl;
	public Slider slider;
	public Text progresstext;

	public void LoadLevel(int SceneIndex){
		StartCoroutine (LoadAsynchronously(SceneIndex));
	}

	IEnumerator LoadAsynchronously(int SceneIndex){
		AsyncOperation operation = SceneManager.LoadSceneAsync (SceneIndex);
		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress/.9f);
			loadingscreen.SetActive (true);
			slider.value = progress;
			Debug.Log (operation.progress);
			progresstext.text = Mathf.RoundToInt(progress * 100) + "%";

			yield return null;
		}
	}
	public void ServerPlayButton (int SceneIndex) {
		attckpnl.SetActive (false);
		PlayerPrefs.SetString("auraattack",Random.Range(400,600).ToString());
		PlayerPrefs.SetString("nameattack","TiotsuBot");
		PlayerPrefs.SetString("yunkattack",Random.Range(150,250).ToString());
		StartCoroutine (LoadAsynchronously(SceneIndex));
	}


	// Update is called once per frame
	public void LocalPlayButton (int SceneIndex) {
		attckpnl.SetActive (false);
		StartCoroutine (LoadAsynchronously(SceneIndex));
	}

}
