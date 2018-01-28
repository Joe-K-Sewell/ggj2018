using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

	float lifeTime = 3f;
	public int speed = 50;
	// Use this for initialization
	void Start () {
		transform.Rotate (0, 0, 90);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(0, -speed *Time.deltaTime ,0);
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
