using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	Ustawienia ust;
	public GameObject gameCanvas;

	// Use this for initialization
	void Start () {
		
		gameCanvas.SetActive (false);
		ust = GameObject.Find("Ustawienia").GetComponent<Ustawienia> ();
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
		PlayerMovement pMovement = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();
		pMovement.pauseVelocity (false);
		ust.globalPause (false);
		gameCanvas.SetActive (true);
		transform.parent.gameObject.SetActive (false);
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
