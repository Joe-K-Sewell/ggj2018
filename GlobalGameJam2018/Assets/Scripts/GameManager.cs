using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public int numCell = 0;
	public int numLaptop = 0;
	public int numTablet = 0;

	public float score = 0;

	// Use this for initialization
	void Awake () {
		// ensure only one GameManager exists at a time
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);

		// make sure the manager isn't destoryed between scenes
		DontDestroyOnLoad (gameObject);
	}

}
