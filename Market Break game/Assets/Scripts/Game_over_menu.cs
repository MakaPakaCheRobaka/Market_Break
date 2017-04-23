using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_over_menu : MonoBehaviour
{
    public void GameOverMenu()   // po kliknięciu przycisku menu gra ładuje scene z Manu
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameOverRestart()  // po kliknięciu przycisku restart gra ładuje scene jeszcze raz
    {
        SceneManager.LoadScene("scena");
    }
}

    