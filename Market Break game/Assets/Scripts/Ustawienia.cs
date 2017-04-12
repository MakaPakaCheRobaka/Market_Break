using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ustawienia : MonoBehaviour {

	AudioSource source;

	void Start ()
	{
		//Ustawienie dźwięku i muzyki na włączone jeśli nie zostały wcześniej ustawione
		if (!PlayerPrefs.HasKey ("Dźwięk")) 
		{
			PlayerPrefs.SetInt ("Dźwięk", 1);
		}
		if (!PlayerPrefs.HasKey ("Muzyka")) 
		{
			PlayerPrefs.SetInt ("Muzyka", 1);
		}
		source = GetComponent<AudioSource> ();
	}

	//Funkcja odpowiadająca za odtwarzanie dźwięków

	public void odegrajDzwiek(AudioClip sound)
	{
		if (PlayerPrefs.GetInt("Dźwięk") == 1) //Sprawdzanie czy dźwięk jest włączony
		{
			source.PlayOneShot (sound);
		}
	}
}
