using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour {

	Text tip;	// Tekst obecnie wyświetlanej wskazówki
	Animator fAnim;	// Animacja wygaszania ekranu i wyświetlania tekstu
	public bool isWorking = false;	// Sprawdzanie, czy jest aktualnie wyświetlana wskazówka
	Ustawienia ust;	// Obiekt z ustawieniami

	List<int> idList = new List<int> ();	// Lista identyfikatorów wskazówek do wyświetlenia

	//Poniżej struktura zawierająca teksty wskazówek i zmienną sprawdzającą, czy dana wskazówka została już użyta
	[System.Serializable]
	public struct tipsTextStruct
	{
	[TextArea(1,5)]
	public string tipsText;
	public bool wasUsed;
	}
		
	public tipsTextStruct[] tipsTexts;	//Tablica powyższych struktur

	void Start () 
	{
		ust = GameObject.Find ("Ustawienia").GetComponent<Ustawienia> ();
		fAnim = GetComponent<Animator> ();
		tip = GetComponentInChildren<Text> ();
	}
		
	public void tips (int id)
	{
		if(PlayerPrefs.GetInt("Tips") == 1)
		{
		idList.Add (id);	// Dodawanie id do listy oczekujących wskazówek
		}
	}

	public void tipsPause()
	{
		ust.globalPause (true);
	}

	public void tipsUnpause()
	{
		ust.globalPause (false);
	}
		
	void Update () 
	{
		/*Poniżej sprawdzanie, czy są wskazówki do wyświetlenia, czy żadna 
		wskazówka nie jest obecnie wyświetlana i czy pierwsza wskazówka na liście nie została już użyta.
		W warunku włączana jest animacja wskazówki, ustawiany jest tekst wskazówki, wskazówka zostaje uznana
		jako już użyta i później zostaje usunięta z listy identyfikatorów.*/
		if ((idList.Count > 0) && (!isWorking) && (tipsTexts [idList [0]].wasUsed == false)) 
		{
			isWorking = true;
			fAnim.SetTrigger ("FadeIn");
			tip.text = tipsTexts [idList [0]].tipsText;
			tipsTexts [idList [0]].wasUsed = true;
			idList.RemoveAt (0);
		}
		else if (idList.Count > 0) //Usunięcie wskazówki z listy w przypadku, gdy została już użyta wcześniej 
		{
			if(tipsTexts [idList [0]].wasUsed == true) idList.RemoveAt (0);
		}
		if (isWorking) //Pominięcie wskazówki
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				fAnim.SetTrigger ("Skip");
			}
		}
	}
}
