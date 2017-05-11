using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ulepszenia : MonoBehaviour {

	Text infoText;
	Text pointsText;
	Text doubleJumpCostText;
	Button doubleJumpButton;
	Text superPowerCostText;
	Button superPowerButton;
	public GameObject gameOverCanvas;
	public GameObject upgradeCanvas;

	public int doubleJumpCost;
	public int superPowerCost;

	public void DoubleJumpBuy()
	{
		if (PlayerPrefs.GetInt ("Points") < doubleJumpCost) 
		{
			infoText.text = "Nie stać cię na zakup tego ulepszenia!";
		} 
		else 
		{
			PlayerPrefs.SetInt ("Points", PlayerPrefs.GetInt ("Points") - doubleJumpCost);
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

	public void SuperPowerBuy()
	{
		if (PlayerPrefs.GetInt ("Points") < superPowerCost) 
		{
			infoText.text = "Nie stać cię na zakup tego ulepszenia!";
		} 
		else 
		{
			PlayerPrefs.SetInt ("Points", PlayerPrefs.GetInt ("Points") - superPowerCost);
			PlayerPrefs.SetInt ("SuperPower", 1);
			infoText.text = "Zakupiono ulepszenie!";
			SuperPowerBought ();
		}

	}

	void SuperPowerBought()
	{
		superPowerCostText.text = "Kupione";
		superPowerButton.interactable = false;
	}

	public void backPress()
	{
		gameOverCanvas.SetActive (true);
		upgradeCanvas.SetActive (false);
	}

	void Update()
	{
		pointsText.text = "Dostępne punkty: " + PlayerPrefs.GetInt ("Points").ToString ();
	}

	void Start()
	{
		if (!PlayerPrefs.HasKey ("DoubleJump")) 
		{
			PlayerPrefs.SetInt ("DoubleJump", 0);
		}

		if (!PlayerPrefs.HasKey ("SuperPower")) 
		{
			PlayerPrefs.SetInt ("SuperPower", 0);
		}

		infoText = GameObject.Find ("InfoText").GetComponent<Text> ();
		pointsText = GameObject.Find ("PointsText").GetComponent<Text> ();
		doubleJumpCostText = GameObject.Find ("DoubleJumpCostText").GetComponent<Text> ();
		doubleJumpButton = GameObject.Find ("DoubleJumpButton").GetComponent<Button> ();
		superPowerCostText = GameObject.Find ("SuperPowerCostText").GetComponent<Text> ();
		superPowerButton = GameObject.Find ("SuperPowerButton").GetComponent<Button> ();

		if (PlayerPrefs.GetInt ("DoubleJump") == 1) 
		{
			DoubleJumpBought ();
		} 
		else 
		{
			doubleJumpCostText.text = "Koszt: " + doubleJumpCost;
		}

		if (PlayerPrefs.GetInt ("SuperPower") == 1) 
		{
			SuperPowerBought ();
		}
		else 
		{
			superPowerCostText.text = "Koszt: " + superPowerCost;
		}
	}
}
