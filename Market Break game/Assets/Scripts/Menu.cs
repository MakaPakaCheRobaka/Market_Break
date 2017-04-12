using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GameObject.Find ("PrzyciskDźwięk").GetComponentInChildren<Text> ();
		if (PlayerPrefs.GetInt("Dźwięk") == 1) 
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
		if (PlayerPrefs.GetInt("Dźwięk") == 1) 
		{
			PlayerPrefs.SetInt("Dźwięk", 0);
			text.text = "DŹWIĘK: OFF";
		}
		else 
		{
			PlayerPrefs.SetInt("Dźwięk", 1);
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
		}
		else 
		{
			PlayerPrefs.SetInt("Muzyka", 1);
			text.text = "MUZYKA: ON";
		}
	}

	public void pressWyjscie()
	{
		Application.Quit();
	}
}
