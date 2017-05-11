using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {

	AudioSource crowdSource;
	PlayerMovement player;
	public float crowdSpeed;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();
		crowdSource = GetComponent<AudioSource> ();
		if (PlayerPrefs.GetInt ("Dzwiek") == 0)
			crowdSource.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = Vector2.Lerp(transform.position, new Vector2 (player.transform.position.x - player.slip - 2, transform.position.y), crowdSpeed * Time.deltaTime);
	}
}
