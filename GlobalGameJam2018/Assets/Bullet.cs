using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


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
		rb2d.velocity = new Vector2 (0, 5);
	}
}
