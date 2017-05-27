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
	public Loading loadScene;

	void resetSound()
	{
		ust = GameObject.Find ("Ustawienia").GetComponent<Ustawienia> ();
		ust.resetSound ();
	}

    public void GameOverMenu()   // po kliknięciu przycisku menu gra ładuje scene z Manu
    {
		resetSound ();
		StartCoroutine (loadScene.LoadScene("Menu", true));
    }

    public void GameOverRestart()  // po kliknięciu przycisku restart gra ładuje scene jeszcze raz
    {
		resetSound ();
		StartCoroutine (loadScene.LoadScene("scena", false));
    }

	public void GameOverUpgrade()
	{
		gameOverCanvas.SetActive (false);
		upgradeCanvas.SetActive (true);
	}
}

    