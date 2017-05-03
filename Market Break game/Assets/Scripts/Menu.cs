using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	Text text;
	Ustawienia ust;
	public AudioClip menuMusic;

	// Use this for initialization
	void Start () {
		ust = GameObject.Find ("Ustawienia").GetComponent<Ustawienia> ();
		text = GameObject.Find ("PrzyciskDźwięk").GetComponentInChildren<Text> ();
		ust.wlaczMuzyke (menuMusic);
		if (PlayerPrefs.GetInt("Dzwiek") == 1) 
		{
			text.text = "DŹWIĘK: ON";
		}
		else 
		{
			text.text = "DŹWIĘK: OFF";
		}
		text = GameObject.Find ("PrzyciskMuzyka").GetComponentInChildren<Text> ();

		if (PlayerPrefs.GetInt("Muzyka") == 1) 
		{
			text.text = "MUZYKA: ON";
		}
		else 
		{
			text.text = "MUZYKA: OFF";
		}
	}

	public void pressStart()
	{
		Debug.Log ("Click");
		SceneManager.LoadScene ("scena");
	}

	public void pressDzwiek()
	{
		text = GameObject.Find ("PrzyciskDźwięk").GetComponentInChildren<Text> ();
		if (PlayerPrefs.GetInt("Dzwiek") == 1) 
		{
			PlayerPrefs.SetInt("Dzwiek", 0);
			text.text = "DŹWIĘK: OFF";
		}
		else 
		{
			PlayerPrefs.SetInt("Dzwiek", 1);
			text.text = "DŹWIĘK: ON";
		}
	}

	public void pressMuzyka()
	{
		text = GameObject.Find ("PrzyciskMuzyka").GetComponentInChildren<Text> ();
		if (PlayerPrefs.GetInt("Muzyka") == 1) 
		{
			PlayerPrefs.SetInt("Muzyka", 0);
			text.text = "MUZYKA: OFF";
			ust.resetSound ();
		}
		else 
		{
			PlayerPrefs.SetInt("Muzyka", 1);
			text.text = "MUZYKA: ON";
			ust.resetSound ();
			ust.wlaczMuzyke (menuMusic);
		}
	}

	public void pressWyjscie()
	{
		Application.Quit();
	}
}
