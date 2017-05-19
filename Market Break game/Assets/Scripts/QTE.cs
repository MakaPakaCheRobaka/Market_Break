using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour 
{
	public GameObject qteGraphics;
	public Image qteBarFill; // pasek QTE
	public float qteSpeed; // szybkość napełniania się paska QTE na korzyść tłumu
	public float qteSpeedRaise;
	[HideInInspector]
	public bool isGameOver;
	Ustawienia settings; // ustawienia dźwięku, muzyki i odgrywanie dźwięków
	public GameObject gameOverCanvas; //Ekran Game Over
	public GameObject gameCanvas;
	public PlayerMovement player;
	public AudioClip gameOverSound; // dźwięk przegranej gry
	public Text scoreGameOverText;     //tekst z wynikiem w ekranie game over
	public Text highscoreText;           //tekst z najlepszym wynikiem

	void Start()
	{
		settings = GameObject.FindWithTag ("Ustawienia").GetComponent<Ustawienia> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();
	}

	void OnEnable()
	{
		qteGraphics.SetActive(true);
		gameCanvas.SetActive (false);
		qteBarFill.fillAmount = 0.50f;       // ustawienie paska w połowie
	}

	void Update()
	{
		bool qteEnd = false; // zmienna która przechowuje info czy tłum złąpał gracza
		//if(!qteGraphics.activeSelf == false) qteGraphics.SetActive(true);
		//if (!gameCanvas.activeSelf == true) gameCanvas.SetActive (false);
		if ((!settings.QTEPause) && (!qteEnd)) 
		{
			if(!settings.movementPause) settings.movementPause = true;
			if(!settings.spawnerPause) settings.spawnerPause = true;
			qteBarFill.fillAmount = qteBarFill.fillAmount + qteSpeed * Time.deltaTime;  // Pasek wypełania się na korzyść tłumu

			if (Input.GetMouseButtonDown (0)) 
			{ //Gracz musi szybko klikać aby się uwolnić od tłumu
				qteBarFill.fillAmount = qteBarFill.fillAmount - 0.055f;
			}

			if (qteBarFill.fillAmount < 0.01) 
			{
				qteEnd = true;
				QTEStop (false);
			}

			else if (qteBarFill.fillAmount >= 1) 
			{
				qteEnd = true;
				QTEStop (true);
			}
		}
	}

	void QTEStop(bool wasCaught)
	{
		qteGraphics.SetActive(false);
		if (wasCaught) // Jeśli gracz przegrał QTE wyświetla się ekran Game Over z wynikiem i przyciskami menu i restartu
		{  
			int newPoints = PlayerPrefs.GetInt ("Points") + (int)player.scoreValue;	// Sumowanie wyniku do ogólnej liczby punktów
			PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt("Money") + player.collectMoney);
			PlayerPrefs.SetInt ("Points", newPoints);	// Ustawianie nowej liczby punktów
			isGameOver = true;
			settings.odegrajDzwiek (gameOverSound);
			if (player.isHighscore == true) //	Ustawianie nowego rekordu
			{
				PlayerPrefs.SetInt ("Highscore", (int)player.scoreValue);
				player.isHighscore = false;
			}
			player.scoreText.gameObject.SetActive (false);
			scoreGameOverText.text = "Twój wynik : " + (int)player.scoreValue;
			highscoreText.text = "Najlepszy wynik : " + PlayerPrefs.GetInt ("Highscore");
			gameOverCanvas.SetActive (true);
		}

		else if (!wasCaught) // Jeśli gracz wygrał qTE biegnie dalej a tłum się odsuwa
		{ 
			settings.movementPause = false;
			settings.spawnerPause = false;
			player.scoreText.gameObject.SetActive (true);
			player.slip = 3;
			qteSpeed += qteSpeedRaise;
			gameCanvas.SetActive (true);
		}
		gameObject.SetActive (false);
	}
}
