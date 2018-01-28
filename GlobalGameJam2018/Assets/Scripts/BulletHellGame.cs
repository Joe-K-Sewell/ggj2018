using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellGame : MonoBehaviour {

    public GameObject panel;

	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 0;	
	}
	
	// Update is called once per frame
	
    public void onStart()
    {
        panel.SetActive(false);
        StartGame();
    }

    void StartGame()
    {
        Time.timeScale = 1;
    }
}
