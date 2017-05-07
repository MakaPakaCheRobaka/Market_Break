using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    //private Rigidbody2D tlum;
	Transform tlum;
    public GameObject Q; //QTE 
	public GameObject GameCanvas;
    public GameObject Game_Over; //Ekran Game Over
	public GameObject UpgradeCanvas;
    public GameObject Pasek_QTE;
    public GameObject Tap;
    public Image pasek; // pasek QTE
    public Transform punkt_startowy;
    public Text Wynik;          //tekst z wynikiem
    public Text Wynik_game_over;     //tekst z wynikiem w ekranie game over
    public Text Naj_Wynik;           //tekst z najlepszym wynikiem
	public Image superPowerText;
	public Image doubleJumpText;
    public float jumpSpeed;	// Moc skoku
    public float moveSpeed;	// Prędkość postaci
    public int potk; // zmienna która przechowuje ile razy gracz wpadł na przeszkodę
    public bool wolny = false; // zmienna która przechowuje info czy gracz się uwolnił przed tłumem
    public bool zlapany = false; // zmienna która przechowuje info czy tłum złąpał gracza
    public float wynik;           // zmienna przechowuje info o wyniku gracza
    public AudioClip uderzenieDzwiek; // dźwięk uderzenie w przeszkodę
    public AudioClip muzyka; // dźwięk muzyki
	public AudioClip skokDzwiek; // dźwięk skoku
	public AudioClip highscoreDzwiek; // dźwięk nowego rekordu
	public AudioClip gameoverDzwiek; // dźwięk przegranej gry
    Ustawienia ust; // ustawienia dźwięku, muzyki i odgrywanie dźwięków
    public float QTE_speed; // szybkość napełniania się paska QTE na korzyść tłumu
	bool highscore = false;
	bool game_over;
    public  Vector3 spawnerpos;
    public GameObject spawner;
	public Animator newRecord;	// Animacja pobicia rekordu
	int jumps = 0; // Ilość dostępnych skoków
	int superCharge = 0; // Poziom naładowania super mocy
	bool superPowerIsActive = false;	//	Sprawdzanie czy super moc jest aktywna
	Animator gAnim;	//	Animacja postaci
	public Tips tips;
	public float tlumSpeed;

    // Use this for initialization
    void Start()
    {
		gAnim = GetComponent<Animator> ();
		newRecord = GameObject.Find("NewRecordText").GetComponent<Animator> ();
		game_over = false;
        ust = GameObject.Find("Ustawienia").GetComponent<Ustawienia>();
        myRigidbody = GetComponent<Rigidbody2D>();
        tlum = GameObject.FindGameObjectWithTag("Tlum").GetComponent<Transform>();
        pasek.fillAmount = 0.50f;       // ustawienie paska w połowie
        Q.SetActive(false);             //pasek QTE jest niewidoczny
        Tap.SetActive(false);           //Tap jest niewidoczny
		GameCanvas.SetActive(true);
        Game_Over.SetActive(false);     //pasek GAme Over jest niewidoczny
		UpgradeCanvas.SetActive(false);
        wynik = 0f;                     //zerowanie wyniku
        QTE_speed = 0.2f;
        ust.wlaczMuzyke(muzyka);
		if (PlayerPrefs.GetInt ("DoubleJump") == 0) 
		{
			doubleJumpText.transform.parent.gameObject.SetActive (false);
		}
		if (PlayerPrefs.GetInt ("SuperPower") == 0) 
		{
			superPowerText.transform.parent.gameObject.SetActive (false);
		}
    }

	void SuperPower()
	{
		if ((superCharge < 1000) && (!superPowerIsActive)) 
		{
			superCharge++;
			superPowerText.fillAmount = (float)superCharge / 1000;
		} 
		else if(superCharge == 1000)
		{
			superCharge = 0;
			superPowerIsActive = true;
		}
	}

    // Update is called once per frame
    void LateUpdate()
	{
		if (!ust.paused)	//	Sprawdzanie, czy gra nie została spauzowana 
		{
			tlum.position = Vector2.Lerp(tlum.position, new Vector2 (transform.position.x - potk - 1, tlum.position.y), tlumSpeed * Time.deltaTime);

			if (PlayerPrefs.GetInt ("SuperPower") == 1)	//	Sprawdzanie, czy ulepszenie Super Mocy jest aktywne 
			{
				SuperPower ();
			}
				
			if (PlayerPrefs.GetInt ("DoubleJump") == 1) //	Poniżej wypełnianie ikony podwójnego skoku na podstawie ilości dostępnych skoków
			{
				doubleJumpText.fillAmount = (float)jumps / 2;
			}

			if (myRigidbody.velocity.y < 0) //	Włączanie odpowiedniej animacji skoku na podstawie kierunku lotu
			{
				gAnim.SetBool ("JumpDown", true);
				gAnim.SetBool ("JumpUp", false);
			}

			spawnerpos = spawner.transform.position;

			if (potk == 0) // Jeśli gracz potknął się 3 razy uruchamia się QTE
			{  
				Wynik.gameObject.SetActive (false);
				QTE ();
				spawnerpos.y = -5f;
				spawner.transform.position = spawnerpos;
				if ((zlapany) && (!game_over)) // Jeśli gracz przegrał QTE wyświetla się ekran Game Over z wynikiem i przyciskami menu i restartu
				{  
					int newPoints = PlayerPrefs.GetInt ("Points") + (int)wynik;	// Sumowanie wyniku do ogólnej liczby punktów
					PlayerPrefs.SetInt ("Points", newPoints);	// Ustawianie nowej liczby punktów
					Pasek_QTE.SetActive (false);
					game_over = true;
					ust.odegrajDzwiek (gameoverDzwiek);
					if (highscore == true) //	Ustawianie nowego rekordu
					{
						PlayerPrefs.SetInt ("Highscore", (int)wynik);
						highscore = false;
					}
					Wynik.gameObject.SetActive (false);
					Wynik_game_over.text = "Twój wynik : " + Mathf.Round (wynik);
					Naj_Wynik.text = "Najlepszy wynik : " + PlayerPrefs.GetInt ("Highscore");
					GameCanvas.SetActive (false);
					Game_Over.SetActive (true);
				}

				if (wolny) // Jeśli gracz wygrał QTE biegnie dalej a tłum się odsuwa
				{ 
					Q.SetActive (false);    //Pasek QTE jest niewidoczny  
					Tap.SetActive (false);
					Wynik.gameObject.SetActive (true);
					potk = 3;
					wolny = false;
					pasek.fillAmount = 0.5f;
					QTE_speed = QTE_speed + 0.01f;
					spawnerpos.y = 0f;
					spawner.transform.position = spawnerpos;
				}
			} 
			else 
			{
				wynik = Vector3.Distance (transform.parent.position, punkt_startowy.position); // Wynik jest to dystans jaki pokonał gracz od punktu startowego
				Wynik.text = "Wynik: " + Mathf.Round (wynik) + " Rekord: " + PlayerPrefs.GetInt ("Highscore");	//	Wyświetlanie wyniku podczas gry
				//	Poniżej informacja o pobiciu rekordu
				if ((wynik > PlayerPrefs.GetInt ("Highscore")) && (highscore == false)) 
				{
					newRecord.SetTrigger ("NewRecord");
					highscore = true;
					ust.odegrajDzwiek (highscoreDzwiek);
					Wynik.color = Color.green;
				}
				HandleMovement ();
			}
		}
    }

    private void HandleMovement()	// Funkcja odpowiedzialna za ruch w prawo i wykonywanie skoku
    {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) 
		{
			if (jumps > 0) 
			{
			jumps--;
			gAnim.SetBool ("JumpUp", true);
			gAnim.SetBool ("JumpDown", false);
			ust.odegrajDzwiek(skokDzwiek);
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpSpeed);
			}
		} 
		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);
    	}


    void OnCollisionEnter2D(Collision2D target)
    {
		if (target.gameObject.tag == "Ground") // Odnawianie ilości skoków przy zetknięciu z ziemią i włączenie animacji biegu
		{
			gAnim.SetBool ("JumpDown", false);
			if ((PlayerPrefs.GetInt ("DoubleJump") == 1) && (jumps < 2)) 
			{
				jumps = 2;
			}
			if ((PlayerPrefs.GetInt ("DoubleJump") == 0) && (jumps < 1)) 
			{
				jumps = 1;
			}
		}
			
        if (target.gameObject.tag == "Przeszkoda")	//	Dotknięcie przeszkody
        {
			target.gameObject.AddComponent<Rigidbody2D> ();
			Rigidbody2D tRigidbody = target.gameObject.GetComponent<Rigidbody2D> ();
			ust.odegrajDzwiek (uderzenieDzwiek);
			target.collider.isTrigger = true;
			if (!superPowerIsActive) 
			{
				potk--;
			} 
			else 
			{
				tRigidbody.AddForce (new Vector2 (1000, 200) * 100 * Time.deltaTime);
				superPowerIsActive = false;
			}
        }
    }

	void OnTriggerEnter2D(Collider2D target)	// Sprawdzanie, czy przed graczem jest przeszkoda
	{
		if(PlayerPrefs.GetInt("Tips") == 1)
		{
			if(target.gameObject.tag == "Przeszkoda")
			{
				tips.tips (2);
			}
		}
	}

    void QTE()
    {
		tips.tips (3);
        Q.SetActive(true); //Włącza się QTE
        Tap.SetActive(true);
        pasek.fillAmount = pasek.fillAmount + QTE_speed * Time.deltaTime;  // Pasek wypełania się na korzyść tłumu

        if (Input.GetMouseButtonDown(0)) //Gracz musi szybko klikać aby się uwolnić od tłumu
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
