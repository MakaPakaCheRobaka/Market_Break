using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public enum Choice {Start, Dźwięk, Muzyka, Wyjście };
	public Choice choice;
	public string scena;

	Text text;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown()
	{
		//Ładowanie sceny bazowej w przypadku naciśnięcia przycisku startu
		if (choice == Choice.Start) 
		{
			Debug.Log ("1");
			SceneManager.LoadScene (scena);
		} 
		//Włączanie i wyłączanie dźwięku
		else if (choice == Choice.Dźwięk) 
		{
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
		//Włączanie i wyłączanie muzyki
		else if (choice == Choice.Muzyka) 
		{
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
		//Przycisk wyjścia z gry
		else
		{
			Debug.Log("3");
			Application.Quit();
		}
	}
}
