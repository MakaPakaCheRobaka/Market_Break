﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    private Rigidbody2D tlum;
    public GameObject Q; //QTE 
	public GameObject GameCanvas;
    public GameObject Game_Over; //Ekran Game Over
	public GameObject UpgradeCanvas;
    public GameObject Pasek_QTE;
    public GameObject Tap;
    public Image pasek; // pasek QTE
    public Transform gracz;
    public Transform punkt_startowy;
    public Text Wynik;          //tekst z wynikiem
    public Text Wynik_game_over;     //tekst z wynikiem w ekranie game over
    public Text Naj_Wynik;           //tekst z najlepszym wynikiem
    public GameObject Wynik2;  //wynik podczas rozgrywki
    public float jumpSpeed;	// Moc skoku
    public float moveSpeed;	// Prędkość postaci
    public float wys_pos; // zmienna która pokazuje aktualną wysokość postaci
    public float skok_poz_str; // zmienna zapamiętyje wysokość startową postaci
    public int potk; // zmienna która przechowuje ile razy gracz wpadł na przeszkodę
    public bool wolny = false; // zmienna która przechowuje info czy gracz się uwolnił przed tłumem
    public bool zlapany = false; // zmienna która przechowuje info czy tłum złąpał gracza
    public float wynik;           // zmienna przechowuje info o wyniku gracza
    public AudioClip uderzenieDzwiek; // dźwięk uderzenie w przeszkodę
    public AudioClip muzyka; // dźwięk muzyki
	public AudioClip skokDzwiek; // dźwięk skoku
	public AudioClip highscoreDzwiek; // dźwięk nowego rekordu
	public AudioClip gameoverDzwiek; // dźwięk przegranej gry
	public AudioClip tlumDzwiek; // odgłosy tłumu
    Ustawienia ust; // ustawienia dźwięku, muzyki i odgrywanie dźwięków
    public float QTE_speed; // szybkość napełniania się paska QTE na korzyść tłumu
	bool jump = false;
	bool highscore = false;
	bool game_over;
    public  Vector3 spawnerpos;
    public GameObject spawner;
	public Animator newRecord;	// Animacja pobicia rekordu
	int jumps = 0; // Ilość dostępnych skoków

    // Use this for initialization
    void Start()
    {
		newRecord = GameObject.Find("NewRecordText").GetComponent<Animator> ();
		game_over = false;
        ust = GameObject.Find("Ustawienia").GetComponent<Ustawienia>();
        myRigidbody = GetComponent<Rigidbody2D>();
        tlum = GameObject.FindGameObjectWithTag("Tlum").GetComponent<Rigidbody2D>();
        skok_poz_str = myRigidbody.position.y;
        potk = 0;                       // liczba potknięć = 0
        pasek.fillAmount = 0.50f;       // ustawienie paska w połowie
        Q.SetActive(false);             //pasek QTE jest niewidoczny
        Tap.SetActive(false);           //Tap jest niewidoczny
		GameCanvas.SetActive(true);
        Game_Over.SetActive(false);     //pasek GAme Over jest niewidoczny
		UpgradeCanvas.SetActive(false);
        wynik = 0f;                     //zerowanie wyniku
        QTE_speed = 0.2f;
        ust.wlaczMuzyke(muzyka);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        spawnerpos = spawner.transform.position;

        wys_pos = myRigidbody.position.y; // aktualna wysokość postaci

        if (potk >= 3)  // jeśli gracz potknoł się 3 razy uruchamia się QTE
        {
            Wynik2.SetActive(false);
            QTE();
            spawnerpos.y = -5f;
            spawner.transform.position = spawnerpos;
			if ((zlapany) && (!game_over))  // jeśli gracz przegrał QTE wyświetla się ekran Game Over z wynikiem i przyciskami menu i restartu
            {
				int newPoints = PlayerPrefs.GetInt ("Points") + (int)wynik;	// Sumowanie wyniku do ogólnej liczby punktów
				PlayerPrefs.SetInt ("Points", newPoints);	// Ustawianie nowej liczby punktów

				Pasek_QTE.SetActive(false);
				game_over = true;
				ust.odegrajDzwiek (gameoverDzwiek);
				if(highscore == true)
				{
					PlayerPrefs.SetInt("Highscore", (int)wynik);
					highscore = false;
				}
                Wynik2.SetActive(false);
                //Pasek_QTE.SetActive(false);
                Wynik_game_over.text = "Twój wynik : " + Mathf.Round(wynik);
                Naj_Wynik.text= "Najlepszy wynik : " + PlayerPrefs.GetInt("Highscore");

				GameCanvas.SetActive (false);
                Game_Over.SetActive(true);
                
            }

            if (wolny) // jeśli gracz wygrał QTE biegnie dalej a tłum się odsuwa
            {
                tlum.AddForce(new Vector2(-300, 0) * 40 * Time.deltaTime); //odsunięcie tłumu
                Q.SetActive(false);    //pasek QTE jest niewidoczny  
                Tap.SetActive(false);
                Wynik2.SetActive(true);
                potk = 0;
                wolny = false;
                pasek.fillAmount = 0.5f;
                QTE_speed = QTE_speed + 0.01f;
                spawnerpos.y = 0f;
                spawner.transform.position = spawnerpos;
            }
        }
        else
        {
            wynik = Vector3.Distance(gracz.position, punkt_startowy.position); // wynik jest to dystans jaki pokonał gracz od punktu startowego
            //Wynik.text = "" + Mathf.Round(wynik);  // wyświetla zaokrąglony wynik
			Wynik.text = "Wynik: " + Mathf.Round(wynik) + " Rekord: " + PlayerPrefs.GetInt("Highscore");
			if((wynik > PlayerPrefs.GetInt("Highscore")) && (highscore == false))
			{
				newRecord.SetTrigger ("NewRecord");
				highscore = true;
				ust.odegrajDzwiek (highscoreDzwiek);
                Wynik.color=Color.green;
			}
            HandleMovement();
        }


    }

    private void HandleMovement()	// Funkcja odpowiedzialna za ruch w prawo i wykonywanie skoku
    {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
			if (jumps > 0) 
			{
				jumps--;
				ust.odegrajDzwiek(skokDzwiek);
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpSpeed);
			}
		} 
		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);
    }


    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Przeszkoda")	//	Dotknięcie przeszkody
        {
			ust.odegrajDzwiek(uderzenieDzwiek);
            target.gameObject.AddComponent<Rigidbody2D>();
            target.collider.isTrigger = true;
            tlum.AddForce(new Vector2(150, 0) * 40 * Time.deltaTime);
            potk++;                                                     // zwiększam liczbe wpadnięć na przeszkodę
            //tlum.Translate (Vector2.right * Time.deltaTime * 10);
        }

		if (target.gameObject.tag == "Ground") // Odnawianie ilości skoków przy zetknięciu z ziemią
		{
			if ((PlayerPrefs.GetInt ("DoubleJump") == 1) && (jumps < 2)) 
			{
				jumps = 2;
			}
			if ((PlayerPrefs.GetInt ("DoubleJump") == 0) && (jumps < 1)) 
			{
				jumps = 1;
			}
		}
    }

	void OnTriggerEnter2D(Collider2D target)	// Sprawdzanie, czy przed graczem jest przeszkoda
	{
		if(PlayerPrefs.GetInt("FirstTime") == 1)
		{
			if(target.gameObject.tag == "Przeszkoda")
			{
				Tap.SetActive(true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D target)		// Sprawdzanie, czy przed graczem nie ma już przeszkody
	{
		if (PlayerPrefs.GetInt ("FirstTime") == 1) 
		{
			if (target.gameObject.tag == "Przeszkoda") 
			{
				Tap.SetActive (false);
			}
		}
	}

    void QTE()
    {

        Q.SetActive(true); //włącza się QTE
        Tap.SetActive(true);
        pasek.fillAmount = pasek.fillAmount + QTE_speed * Time.deltaTime;  // pasek wypełania się na korzyść tłumu

        if (Input.GetMouseButtonDown(0)) //gracz musi szybko klikać aby się uwolnić od tłumu
        {
            pasek.fillAmount = pasek.fillAmount - 0.055f;
        }

        if (pasek.fillAmount < 0.01)
        {
            wolny = true;
        }

        if (pasek.fillAmount >= 1)
        {
            zlapany = true;
        }

    }

}
