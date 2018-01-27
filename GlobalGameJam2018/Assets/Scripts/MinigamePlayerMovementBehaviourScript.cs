using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayerMovementBehaviourScript : MonoBehaviour {

    public float HorizontalVelocity;
    public float VerticalVelocity;

    public float HorizontalBound;
    public float VerticalBound;

    private Rigidbody2D _rigidBody2D;

	// Use this for initialization
	void Start () {
        _rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = HorizontalVelocity * Input.GetAxis("Horizontal");
        float vertical = VerticalVelocity * Input.GetAxis("Vertical");
        
        _rigidBody2D.velocity = new Vector2(horizontal, vertical);

        var newX = Mathf.Clamp(_rigidBody2D.position.x, -HorizontalBound, +HorizontalBound);
        var newY = Mathf.Clamp(_rigidBody2D.position.y, -VerticalBound, +VerticalBound);

        _rigidBody2D.position = new Vector2(newX, newY);
    }
}
