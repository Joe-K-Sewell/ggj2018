using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	float lifeTime = 3f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		transform.Translate(-10 *Time.deltaTime,0 ,0);
		lifeTime -= Time.deltaTime;

		if (lifeTime <= 0) 
		{
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			Destroy (gameObject);
			FindObjectOfType<MinigamePlayerMovementBehaviourScript> ().TakeBulletHit ();
		}
	}
}
