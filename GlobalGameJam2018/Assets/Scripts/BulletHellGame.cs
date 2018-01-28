using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellGame : MonoBehaviour {

	public static BulletHellGame Instance;
    public GameObject panel;

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
		yield return new WaitForSeconds (surviveTime);
		GameOver ();
    }

	public void GameOver () {
		Time.timeScale = 0;
		Debug.Log ("Game Over");
		//GameManager.Instance.score += Player.Instance.GetHealth ();
		// transition
	}
}
