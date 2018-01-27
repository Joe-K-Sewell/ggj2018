using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {


	public GameObject bulletPrefab;
	public Transform[] bulletSpawns;
	public float timeBetweenSpawns = .1f;

	int randomSpawnerSelected = 0;
	float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;
		if (timer <= 0) //This should occur roughly 10 times per second
		{
			FireBulletRandomly ();
			timer = timeBetweenSpawns;
		}
	}


	void FireBulletRandomly()
	{
		randomSpawnerSelected = Random.Range (0, 8);


		Instantiate (bulletPrefab, bulletSpawns[randomSpawnerSelected].position,Quaternion.identity ,null);
	}
}
