using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    public List<bool> eventActive = new List<bool>();
    public bool[] przelacznik = new bool[100];
	
    public void Włącz(int i)
    {
        eventActive[i] = true;
    }

    public void Wyłącz(int i)
    {
        eventActive[i] = false;
    }

    // Use this for initialization
	void Start () {
        for(int i = 0; i < przelacznik.Length; i++)
        {
            przelacznik[i] = false;
        }
    	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
