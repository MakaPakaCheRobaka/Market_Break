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
	public GameObject gameOverCanvas;
	public GameObject upgradeCanvas;

	public void backPress()
	{
		gameOverCanvas.SetActive (true);
		upgradeCanvas.SetActive (false);
	}

	void Update()
	{
		pointsText.text = "Dostępna kasa: " + PlayerPrefs.GetInt ("Money").ToString ();
	}

	public void upgradeBuy (int i)
	{
		if (PlayerPrefs.GetInt ("Money") < upgradesArray[i].cost) 
		{
			infoText.text = "Nie stać cię na zakup tego ulepszenia!";
		} 
		else 
		{
			PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") - upgradesArray[i].cost);
			PlayerPrefs.SetInt (upgradesArray[i].name, 1);
			infoText.text = "Zakupiono ulepszenie!";
			upgradeBought (i);
		}
	}

	void upgradeBought (int i)
	{
		upgradesArray[i].costText.text = "Kupione";
		upgradesArray[i].button.interactable = false;
	}

	void Start()
	{
		for (int i = 0; i < upgradesArray.Length; i++) 
		{
			upgradesArray [i].costText = upgradesArray [i].button.GetComponentInChildren<Text> ();
			if(!PlayerPrefs.HasKey(upgradesArray[i].name))
			{
				PlayerPrefs.SetInt (upgradesArray [i].name, 0);
			}

			if (PlayerPrefs.GetInt (upgradesArray [i].name) == 1) 
			{
				upgradeBought (i);
			} 
			else 
			{
				upgradesArray [i].costText.text = "Koszt: " + upgradesArray [i].cost;
			}
		}
	}
}
