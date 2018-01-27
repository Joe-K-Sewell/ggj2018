using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public Slider healthSlider;

	int startingHealth = 100;
	int currentHealth;

	// Use this for initialization
	void Start () 
	{
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;

	}

	// Update is called once per frame
	void Update () 
	{
		if (currentHealth <= 0) 
		{
			Destroy (gameObject);
		}
	}

	public void TakeDamage(int dmg)
	{
		currentHealth -= dmg;
		healthSlider.value = currentHealth;

	}
}
