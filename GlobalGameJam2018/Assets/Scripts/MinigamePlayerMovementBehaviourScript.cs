using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayerMovementBehaviourScript : MonoBehaviour {

    public float HorizontalVelocity;
    public float VerticalVelocity;

    public float HorizontalBound;
    public float VerticalBound;


	bool isHit = false;
    private Rigidbody2D _rigidBody2D;
	int randomDirectionNumber;
	float hitStrength = 100f;
	float timer;
	float hitDelay = 1f;
	// Use this for initialization
	void Start () {
        _rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transform.right);
        float horizontal = HorizontalVelocity * Input.GetAxis("Horizontal");
        float vertical = VerticalVelocity * Input.GetAxis("Vertical");

		if (isHit) 
		{
			timer -= Time.deltaTime;
			if (timer <= 0) 
			{
				isHit = false;
			}
		}

		else if(!isHit)
		{
       	 _rigidBody2D.velocity = new Vector2(horizontal, vertical);
		}
			
    }

	public void TakeBulletHit()
	{
		isHit = true;
		timer = hitDelay;
		randomDirectionNumber = Random.Range (1, 8);

		transform.eulerAngles = new Vector3 (0, 0, 360 / randomDirectionNumber);
		_rigidBody2D.AddForce (transform.right * hitStrength);
	}
}
