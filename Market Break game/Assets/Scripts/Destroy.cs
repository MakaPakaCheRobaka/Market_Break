using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public int destroyTime = 5;
	Ustawienia settings;

	// Use this for initialization
	void Start () 
	{
		settings = GameObject.FindWithTag ("Ustawienia").GetComponent<Ustawienia> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!IsInvoking("DestroyObject"))
		{
			Invoke("DestroyObject", destroyTime);
		}
		if(settings.spawnerPause)
		{
			CancelInvoke("DestroyObject");
		}
	}

	void DestroyObject()
	{
		Destroy(gameObject);
	}
}
