using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	public void onTitle () {
		SceneManager.LoadScene("Title", LoadSceneMode.Single);
	}

	public void onQuit () {
		Application.Quit ();
	}

}
