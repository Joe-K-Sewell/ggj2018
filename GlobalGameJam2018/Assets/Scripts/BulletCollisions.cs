using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		//Debug.Log (col.gameObject.name);
		if (col.gameObject.CompareTag("Player"))
		{
		FindObjectOfType<Player> ().TakeDamage (10);
			Destroy(gameObject);
		}

		if (col.gameObject.CompareTag("Enemy"))
		{
			FindObjectOfType<Enemy> ().TakeDamage (10);
			Destroy(gameObject);
		}

		if (col.gameObject.CompareTag("Bullet"))
		{
			Destroy(gameObject);
		}

	}
}
