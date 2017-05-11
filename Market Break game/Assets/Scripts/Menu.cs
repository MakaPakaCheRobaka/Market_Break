using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	Text textDzwiek;
	Text textMuzyka;
	Text textTips;
	Ustawienia ust;
	public AudioClip menuMusic;
	Tips tips;

	// Use this for initialization
	void Start () 
	{
		ust = GameObject.Find ("Ustawienia").GetComponent<Ustawienia> ();
		textDzwiek = GameObject.Find ("PrzyciskDźwięk").GetComponentInChildren<Text> ();
		textMuzyka = GameObject.Find ("PrzyciskMuzyka").GetComponentInChildren<Text> ();
		textTips = GameObject.Find ("PrzyciskWskazówki").GetComponentInChildren<Text> ();
		tips = GameObject.Find ("Fade").GetComponent<Tips> ();
		ust.wlaczMuzyke (menuMusic);

		if (!PlayerPrefs.HasKey ("Points")) // Ustawianie zmiennej odpowiedzialnej za ogólne punkty
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

		if (PlayerPrefs.GetInt ("Tips") == 1)
			PlayerPrefs.SetInt ("Tips", 0);

		//	Poniżej dodanie dwóch wskazówek do listy
		tips.tips (0);
		tips.tips (1);
		if (PlayerPrefs.GetInt("Tips") == 0) textTips.text = "WSKAZÓWKI: OFF"; // Ustawianie tekstu na przycisku wskazówek
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

	public void pressTips()	//	Funkcja odpowiedzialna za kliknięcie przycisku wskazówek
	{
		if (PlayerPrefs.GetInt("Tips") == 1) 
		{
			PlayerPrefs.SetInt("Tips", 0);
			textTips.text = "WSKAZÓWKI: OFF";
		}
		else 
		{
			PlayerPrefs.SetInt("Tips", 1);
			textTips.text = "WSKAZÓWKI: ON";
		}
	}

	public void pressWyjscie() // Funkcja odpowiedzialna za kliknięcie przycisku wyjścia
	{
		Application.Quit();
	}
}
