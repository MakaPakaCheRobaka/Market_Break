using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public enum Choice {Start, Dźwięk, Muzyka, Wyjście };
    public Choice choice;
	public string scena;

	Ustawienia ust;

	Text text;

	// Use this for initialization
	void Start () {
		ust = GameObject.Find ("Ustawienia").GetComponent<Ustawienia> ();
		text = gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
		if (choice == Choice.Start) 
		{
			Debug.Log ("1");
			SceneManager.LoadScene (scena);
		} 
		else if (choice == Choice.Dźwięk) 
		{
			if (ust.dzwiek == true) 
			{
				ust.dzwiek = false;
				text.text = "DŹWIĘK: OFF";
			}
			else 
			{
				ust.dzwiek = true;
				text.text = "DŹWIĘK: ON";
			}
		} 
		else if (choice == Choice.Muzyka) 
		{
			if (ust.muzyka == true) 
			{
				ust.muzyka = false;
				text.text = "MUZYKA: OFF";
			}
			else 
			{
				ust.muzyka = true;
				text.text = "MUZYKA: ON";
			}
		}
        else
        {
            Debug.Log("3");
            Application.Quit();
        }
    }
}
