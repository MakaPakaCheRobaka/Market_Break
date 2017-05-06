using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	Text textDzwiek;
	Text textMuzyka;
	Ustawienia ust;
	public AudioClip menuMusic;

	// Use this for initialization
	void Start () {
		ust = GameObject.Find ("Ustawienia").GetComponent<Ustawienia> ();
		textDzwiek = GameObject.Find ("PrzyciskDźwięk").GetComponentInChildren<Text> ();
		textMuzyka = GameObject.Find ("PrzyciskMuzyka").GetComponentInChildren<Text> ();
		ust.wlaczMuzyke (menuMusic);

		if (!PlayerPrefs.HasKey ("Points")) 
		{
			PlayerPrefs.SetInt ("Points", 0);
		}

		if (!PlayerPrefs.HasKey ("FirstTime")) // Sprawdzanie, czy gra została włączona po raz pierwszy na urządzeniu
		{
			PlayerPrefs.SetInt ("FirstTime", 1);
		} 
		else PlayerPrefs.SetInt ("FirstTime", 0);

		if (PlayerPrefs.GetInt("Dzwiek") == 0) // Ustawianie tekstu na przycisku dźwięku
		{
			textDzwiek.text = "DŹWIĘK: OFF";
		}

		if (PlayerPrefs.GetInt("Muzyka") == 0) // Ustawianie tekstu na przycisku muzyki
		{
			textMuzyka.text = "MUZYKA: OFF";
		}
	}

	public void pressStart() // Funkcja odpowiedzialna za kliknięcie przycisku start
	{
		SceneManager.LoadScene ("scena");
	}

	public void pressDzwiek() // Funkcja odpowiedzialna za kliknięcie przycisku dźwięku
	{
		if (PlayerPrefs.GetInt("Dzwiek") == 1) 
		{
			PlayerPrefs.SetInt("Dzwiek", 0);
			textDzwiek.text = "DŹWIĘK: OFF";
		}
		else 
		{
			PlayerPrefs.SetInt("Dzwiek", 1);
			textDzwiek.text = "DŹWIĘK: ON";
		}
	}

	public void pressMuzyka() // Funkcja odpowiedzialna za kliknięcie przycisku muzyki
	{
		if (PlayerPrefs.GetInt("Muzyka") == 1) 
		{
			PlayerPrefs.SetInt("Muzyka", 0);
			textMuzyka.text = "MUZYKA: OFF";
			ust.resetSound ();
		}
		else 
		{
			PlayerPrefs.SetInt("Muzyka", 1);
			textMuzyka.text = "MUZYKA: ON";
			ust.resetSound ();
			ust.wlaczMuzyke (menuMusic);
		}
	}

	public void pressWyjscie() // Funkcja odpowiedzialna za kliknięcie przycisku wyjścia
	{
		Application.Quit();
	}
}
