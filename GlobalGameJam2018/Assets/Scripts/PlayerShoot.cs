using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public Transform bulletSpawn;
	public GameObject bulletPrefab;

	float timer = 0;
	float timeBetweenShots = .5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timer > 0) 
		{
			timer -= Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.F) && timer <= 0) 
		{
			Instantiate (bulletPrefab, bulletSpawn.position, Quaternion.identity, null);
			timer = timeBetweenShots;
		}
	}
}
