using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ustawienia : MonoBehaviour
{
    public AudioSource source;
	public bool paused = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();

		/* Poniżej sprawdzanie, czy zmienne odpowiedzialne za dźwięk, muzykę i najlepszy wynik istnieją,
		 * jeśli nie to dźwięk i muzyka są ustawiane domyślnie na włączone, a najlepszy wynik na 0 */

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
		if (!PlayerPrefs.HasKey ("Tips")) 
		{
			PlayerPrefs.SetInt ("Tips", 1);
		}
    }

	public void resetSound() // Funkcja odpowiedzialna za resetowanie komponentu AudioSource
	{
		source.enabled = false;
		source.enabled = true;
	}

    public void odegrajDzwiek(AudioClip sound) // Funkcja odpowiedzialna za odegranie pojedynczego dźwięku
    {
		if (PlayerPrefs.GetInt("Dzwiek") == 1)
        {
            source.PlayOneShot(sound);
        }

    }

    public void wlaczMuzyke(AudioClip sound) // Funkcja odpowiedzialna za włączenie muzyki
    {
		if (PlayerPrefs.GetInt("Muzyka") == 1)
        {
            source.PlayOneShot(sound);
        }

    }
}
