using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour {

	public GameObject[] enemy;
	public PlayerMovement player;
	public int spawnTimeMin;
	public int spawnTimeMax;

	void addEnemy()
	{
		Debug.Log ("addEnemy");
		int random = Random.Range (0, enemy.Length);
		Instantiate (enemy [random], new Vector2 (player.transform.position.x + 20, 0), Quaternion.identity);
	}

	IEnumerator spawner()
	{
		while(!player.game_over)
		{
		Debug.Log ("Spawn");
		addEnemy ();
		int spawnTime = Random.Range (spawnTimeMin, spawnTimeMax);
		yield return new WaitForSeconds (spawnTime);
		}
	}


	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();
		StartCoroutine (spawner ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
