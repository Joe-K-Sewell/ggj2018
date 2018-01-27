using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayerMovementBehaviourScript : MonoBehaviour {

    public float HorizontalVelocity;
    public float VerticalVelocity;

    public Rigidbody2D _rigidBody2D;

	// Use this for initialization
	void Start () {
        _rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = HorizontalVelocity * Input.GetAxis("Horizontal");
        float vertical = VerticalVelocity * Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log("Hello");
        }

        _rigidBody2D.velocity = new Vector2(horizontal, vertical);
	}
}
