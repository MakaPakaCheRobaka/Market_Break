using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public enum Choice {Start, Opcje, Wyjście };
    public Choice choice;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        if(choice == Choice.Start)
        {
            Debug.Log("1");
            SceneManager.LoadScene("QTE_1_scene");
        }
        else if(choice == Choice.Opcje)
        {
            Debug.Log("2");
        }
        else
        {
            Debug.Log("3");
            Application.Quit();
        }
    }
}
