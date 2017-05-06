using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ulepszenia : MonoBehaviour {

	Text infoText;
	Text pointsText;
	Text doubleJumpCostText;
	Button doubleJumpButton;
	public GameObject gameOverCanvas;
	public GameObject upgradeCanvas;

	public void DoubleJumpBuy()
	{
		if (PlayerPrefs.GetInt ("Points") < 1000) 
		{
			infoText.text = "Nie stać cię na zakup tego ulepszenia!";
		} 
		else 
		{
			PlayerPrefs.SetInt ("Points", PlayerPrefs.GetInt ("Points") - 1000);
			PlayerPrefs.SetInt ("DoubleJump", 1);
			infoText.text = "Zakupiono ulepszenie!";
			DoubleJumpBought ();
		}
		
	}

	void DoubleJumpBought()
	{
		doubleJumpCostText.text = "Kupione";
		doubleJumpButton.interactable = false;
	}

	public void backPress()
	{
		gameOverCanvas.SetActive (true);
		upgradeCanvas.SetActive (false);
	}

	void Start()
	{
		infoText = GameObject.Find ("InfoText").GetComponent<Text> ();
		pointsText = GameObject.Find ("PointsText").GetComponent<Text> ();
		doubleJumpCostText = GameObject.Find ("DoubleJumpCostText").GetComponent<Text> ();
		doubleJumpButton = GameObject.Find ("DoubleJumpButton").GetComponent<Button> ();
		pointsText.text = "Dostępne punkty: " + PlayerPrefs.GetInt ("Points").ToString();

		if (PlayerPrefs.GetInt ("DoubleJump") == 1) 
		{
			DoubleJumpBought ();
		} 
		else 
		{
			doubleJumpCostText.text = "Koszt: 1000";
		}
	}
}
