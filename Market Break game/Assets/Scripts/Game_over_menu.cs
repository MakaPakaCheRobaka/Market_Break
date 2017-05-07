using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_over_menu : MonoBehaviour
{
	Ustawienia ust;
	public GameObject gameOverCanvas;
	public GameObject upgradeCanvas;
	public Tips tips;
	public Text textTips;

	void Start()
	{
		tips.tips (4);
		if (PlayerPrefs.GetInt("Tips") == 0) textTips.text = "WSKAZÓWKI: OFF";
	}

	void resetSound()
	{
		ust = GameObject.Find ("Ustawienia").GetComponent<Ustawienia> ();
		ust.resetSound ();
	}

    public void GameOverMenu()   // po kliknięciu przycisku menu gra ładuje scene z Manu
    {
		resetSound ();
        SceneManager.LoadScene("Menu");
    }

    public void GameOverRestart()  // po kliknięciu przycisku restart gra ładuje scene jeszcze raz
    {
		resetSound ();
        SceneManager.LoadScene("scena");
    }

	public void GameOverTips()
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

	public void GameOverUpgrade()
	{
		gameOverCanvas.SetActive (false);
		upgradeCanvas.SetActive (true);
	}
}

    