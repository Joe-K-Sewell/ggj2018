using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BulletHellGame : MonoBehaviour {

	public static BulletHellGame Instance;
    public GameObject panel;
	public GameObject canvas;
	public GameObject videoPlayer;

	public float surviveTime = 5f;

	// Use this for initialization
	void Start ()
    {
		Instance = this;
        Time.timeScale = 0;	
	}
	
    public void onStart()
    {
        panel.SetActive(false);
		Time.timeScale = 1;
		StartCoroutine(StartGame());
    }

	IEnumerator StartGame()
    {
		yield return new WaitForSecondsRealtime (surviveTime);
		GameOver ();
    }

	IEnumerator EndGame () {
		Debug.Log ("end game");
		yield return new WaitForSecondsRealtime (3);
		Debug.Log ("load scene");
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);
		yield return 0;
	}

	public void GameOver () {
		Time.timeScale = 0;
		Debug.Log ("Game Over");
		Debug.Log (Player.Instance.GetHealth ());
		GameManager.Instance.score += Player.Instance.GetHealth ();
		canvas.SetActive (false);
		videoPlayer.GetComponent<VideoPlayer> ().targetCameraAlpha = 1;
		StartCoroutine (EndGame ());
	}
}
