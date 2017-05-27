using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ulepszenia : MonoBehaviour {

	[System.Serializable]
	public struct upgrades
	{
		public string name;
		public Button button;
		[HideInInspector]
		public Text costText;
		public Image upgradeImage;
		public int cost;
	}

	[SerializeField]
	public upgrades[] upgradesArray;

	public Text infoText;
	public Text pointsText;
	public GameObject gameOverMenu;
	public GameObject upgradeMenu;
	int actualMoney;

	public void BackPress()
	{
		gameOverMenu.SetActive (true);
		upgradeMenu.SetActive (false);
	}

	int MoneyPref()
	{
		return PlayerPrefs.GetInt ("Money");
	}

	IEnumerator UpdateMoney()
	{
		do 
		{
			if (actualMoney < MoneyPref ()) {
				actualMoney++;
			} else
				actualMoney--;
			pointsText.text = "Dostępna kasa: " + actualMoney.ToString ();
			yield return new WaitForSeconds (0.01f);
		} 
		while (actualMoney != MoneyPref ());
	}

	void ShowMoney()
	{
		StartCoroutine (UpdateMoney ());
	}

	public void UpgradeBuy (int i)
	{
		if (MoneyPref() < upgradesArray[i].cost) 
		{
			infoText.text = "Nie stać cię na zakup tego ulepszenia!";
		} 
		else 
		{
			PlayerPrefs.SetInt ("Money", MoneyPref() - upgradesArray[i].cost);
			PlayerPrefs.SetInt (upgradesArray[i].name, 1);
			infoText.text = "Zakupiono ulepszenie!";
			ShowMoney ();
			UpgradeBought (i);
		}
	}

	void UpgradeBought (int i)
	{
		upgradesArray[i].costText.text = "Kupione";
		upgradesArray[i].button.interactable = false;
	}

	void CheckIfBought (int i)
	{
		if (PlayerPrefs.GetInt (upgradesArray [i].name) == 1) 
		{
			UpgradeBought (i);
		} 
		else 
		{
			upgradesArray [i].costText.text = "Koszt: " + upgradesArray [i].cost;
		}
	}

	void CheckIfKeyExist(string name)
	{
		if(!PlayerPrefs.HasKey(name))
		{
			PlayerPrefs.SetInt (name, 0);
		}
	}

	void Start()
	{
		actualMoney = MoneyPref ();
		ShowMoney ();
		for (int i = 0; i < upgradesArray.Length; i++) 
		{
			upgradesArray [i].costText = upgradesArray [i].button.GetComponentInChildren<Text> ();
			CheckIfKeyExist (upgradesArray[i].name);
			CheckIfBought (i);
		}
	}
}
