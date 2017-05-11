using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour {

	public GameObject[] enemy;
	public PlayerMovement player;
	public QTE qte;
	Ustawienia ust;
	public float spawnTimeMin;
	public float spawnTimeMax;
	public float spawnTimeMinLimit;
	public float spawnTimeMaxLimit;
	bool prevPause;
	bool faster = false;

	void addEnemy()
	{
		int random = Random.Range (0, enemy.Length);
		Instantiate (enemy [random], new Vector2 (player.transform.position.x + 20, 0), Quaternion.identity);
	}

	IEnumerator spawner()
	{
		while(!qte.isGameOver)
		{
			if (!ust.spawnerPause) 
			{
				if(!prevPause)	addEnemy ();
				float spawnTime = Random.Range (spawnTimeMin, spawnTimeMax);
				yield return new WaitForSeconds (spawnTime);
			}
			else yield return new WaitUntil (() => !ust.spawnerPause);
			prevPause = ust.spawnerPause;
		}
	}


	// Use this for initialization
	void Start () 
	{
		ust = GameObject.FindWithTag ("Ustawienia").GetComponent<Ustawienia> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();
		StartCoroutine (spawner ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (((int)player.scoreValue % 100 == 0) && (faster == false)) 
		{
			Debug.Log ("Faster");
			faster = true;
			if (spawnTimeMin > spawnTimeMinLimit)
				spawnTimeMin -= 0.1f;
			if (spawnTimeMax > spawnTimeMaxLimit)
				spawnTimeMax -= 0.1f;
		} 
		else if (!((int)player.scoreValue % 100 == 0) && (faster == true))
			faster = false;
	}
}
