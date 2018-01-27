using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float lifeTime = 3f;


	Rigidbody2D rb2d;
	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Move Up
		//rb2d.velocity = new Vector2 (0, 5);
		transform.Translate(0,4 *Time.deltaTime ,0);
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
