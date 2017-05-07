using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	Ustawienia ust;
	public GameObject gameCanvas;
	public Animator fade;
	public Text textTips;

	// Use this for initialization
	void Start () {
		
		gameCanvas.SetActive (false);
		ust = GameObject.Find("Ustawienia").GetComponent<Ustawienia> ();
		if (PlayerPrefs.GetInt("Tips") == 0) textTips.text = "WSKAZÓWKI: OFF";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void resetSound()
	{
		ust.resetSound ();
	}

	public void pressContinue()
	{
		Input.ResetInputAxes();
		ust.globalPause (false);
		gameCanvas.SetActive (true);
		transform.parent.gameObject.SetActive (false);
	}

	public void pressTips()
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

	public void pressMenu()   // po kliknięciu przycisku menu gra ładuje scene z Manu
	{
		resetSound ();
		SceneManager.LoadScene("Menu");
	}

	public void pressRestart()  // po kliknięciu przycisku restart gra ładuje scene jeszcze raz
	{
		resetSound ();
		SceneManager.LoadScene("scena");
	}
}
