using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ustawienia : MonoBehaviour {

	public bool dzwiek;
	public bool muzyka;
	AudioSource source;

	void Awake ()
	{
		source = GetComponent<AudioSource> ();
		DontDestroyOnLoad (gameObject);
		dzwiek = true;
		muzyka = true;
	}

	public void odegrajDzwiek(AudioClip sound)
	{
		if (dzwiek == true) 
		{
			source.PlayOneShot (sound);
		}
	}
}
