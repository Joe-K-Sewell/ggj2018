using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public static Player Instance;
	public Slider healthSlider;

	int startingHealth = 100;
	int currentHealth;

	// Use this for initialization
	void Start () 
	{
		Instance = this;
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentHealth <= 0) 
		{
			Die ();
		}
	}

	void Die() {
		BulletHellGame.Instance.GameOver ();
		Destroy (gameObject);
	}

	public void TakeDamage(int dmg)
	{
		currentHealth -= dmg;
		healthSlider.value = currentHealth;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name == "TopWall") 
		{
			//Debug.Log ("Hello");
			TakeDamage (10);
		}
	}

	public float GetHealth () {
		return currentHealth;
	}
}
