using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	public Transform[] enemyPositions;

	float timeBetweenSwitches = 1f;
	float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;

		if (timer <= 0) 
		{
			ChangePosition ();
		}
	}

	void ChangePosition()
	{
		int enemyPosition = Random.Range (0, 3);
		print (enemyPosition);
		transform.position = enemyPositions [enemyPosition].position;
		Shoot ();
		timer = timeBetweenSwitches;
	}

	void Shoot()
	{
		Instantiate (bulletPrefab, bulletSpawn.position, Quaternion.identity, null);

	}
}
