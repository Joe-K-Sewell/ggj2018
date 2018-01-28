using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

	public void onStart () {
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);
	}

	public void onCredits () {
		SceneManager.LoadScene("Credits", LoadSceneMode.Single);
	}

	public void onQuit () {
		Application.Quit ();
	}
}
