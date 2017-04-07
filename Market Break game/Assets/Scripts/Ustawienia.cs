using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ustawienia : MonoBehaviour {

	public bool dzwiek;
	public bool muzyka;

	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		dzwiek = true;
		muzyka = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
