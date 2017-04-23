using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ustawienia : MonoBehaviour
{

    public bool dzwiek;
    public bool muzyka;
    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
		if (!PlayerPrefs.HasKey ("Dzwiek")) 
		{
			PlayerPrefs.SetInt ("Dzwiek", 1);
		}
		if (!PlayerPrefs.HasKey ("Muzyka")) 
		{
			PlayerPrefs.SetInt ("Muzyka", 1);
		}
		if (!PlayerPrefs.HasKey ("Highscore")) 
		{
			PlayerPrefs.SetInt ("Highscore", 0);
		}
    }

    public void odegrajDzwiek(AudioClip sound)
    {
		if (PlayerPrefs.GetInt("Dzwiek") == 1)
        {
            source.PlayOneShot(sound);
        }

    }

    public void wlaczMuzyke(AudioClip sound)
    {
		if (PlayerPrefs.GetInt("Muzyka") == 1)
        {
            source.PlayOneShot(sound);
        }

    }
}
