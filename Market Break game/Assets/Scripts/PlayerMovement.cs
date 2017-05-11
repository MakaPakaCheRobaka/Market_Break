using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	//Obiekty gracza
    private Rigidbody2D myRigidbody;
	Animator gAnim;	//	Animacja postaci
	public float jumpPower;	// Moc skoku
	public float moveSpeed;	// Prędkość postaci
	[HideInInspector]
	public int slip = 3; // zmienna która przechowuje ile razy gracz wpadł na przeszkodę
	int jumps = 0; // Ilość dostępnych skoków
	Vector2 savedVelocity;

	//Obiekty tłumu
	public float crowdSpeed;
	Transform crowd;

	//Canvasy
	public GameObject gameOverCanvas; //Ekran Game Over
	public GameObject gameCanvas;
	public GameObject upgradeCanvas;

	//Obiekty QTE
	public GameObject qteObject; //QTE 

	//Obiekty wyniku
    public Text scoreText;          //tekst z wynikiem
    public Text scoreGameOverText;     //tekst z wynikiem w ekranie game over
    public Text highscoreText;           //tekst z najlepszym wynikiem
	[HideInInspector]
	public bool isHighscore = false;
	public Animator newRecordAnim;	// Animacja pobicia rekordu
	public float scoreValue;           // zmienna przechowuje info o wyniku gracza

	//Obiekty dźwięków i muzyki
	public AudioClip hitSound; // dźwięk uderzenie w przeszkodę
	public AudioClip gameMusic; // dźwięk muzyki
	public AudioClip jumpSound; // dźwięk skoku
	public AudioClip highscoreSound; // dźwięk nowego rekordu
	public AudioClip gameOverSound; // dźwięk przegranej gry

	//Obiekty ulepszeń
	public Image superPowerText;
	public Image doubleJumpText;
	int superCharge = 0; // Poziom naładowania super mocy
	bool superPowerIsActive = false;	//	Sprawdzanie czy super moc jest aktywna

	//Pozostałe obiekty
    Ustawienia settings; // ustawienia dźwięku, muzyki i odgrywanie dźwięków
	public Tips tips;

    // Use this for initialization
    void Start()
    {
		settings = GameObject.FindWithTag ("Ustawienia").GetComponent<Ustawienia> ();
		gAnim = GetComponent<Animator> ();
		newRecordAnim = GameObject.Find("NewRecordText").GetComponent<Animator> ();
        myRigidbody = GetComponent<Rigidbody2D>();
        crowd = GameObject.FindGameObjectWithTag("Tlum").GetComponent<Transform>();
		gameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);     //pasek GAme Over jest niewidoczny
		upgradeCanvas.SetActive(false);
        scoreValue = 0f;                     //zerowanie wyniku
        settings.wlaczMuzyke(gameMusic);
		if (PlayerPrefs.GetInt ("DoubleJump") == 0) 
		{
			doubleJumpText.transform.parent.gameObject.SetActive (false);
		}
		if (PlayerPrefs.GetInt ("SuperPower") == 0) 
		{
			superPowerText.transform.parent.gameObject.SetActive (false);
		}
    }

	public void pauseVelocity(bool onOff)
	{
		if (onOff) 
		{
			savedVelocity = myRigidbody.velocity;
			myRigidbody.isKinematic = true;
			myRigidbody.velocity = Vector2.zero;
		} 
		else 
		{
			myRigidbody.isKinematic = false;
			myRigidbody.velocity = savedVelocity;
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
    void Update()
	{
		if (myRigidbody.velocity.y < 0) //	Włączanie odpowiedniej animacji skoku na podstawie kierunku lotu
		{
			gAnim.SetBool ("JumpDown", true);
			gAnim.SetBool ("JumpUp", false);
		}
		if (!settings.movementPause)	//	Sprawdzanie, czy gra nie została spauzowana 
		{
			crowd.position = Vector2.Lerp(crowd.position, new Vector2 (transform.position.x - slip - 2, crowd.position.y), crowdSpeed * Time.deltaTime);

			if (PlayerPrefs.GetInt ("SuperPower") == 1)	//	Sprawdzanie, czy ulepszenie Super Mocy jest aktywne 
			{
				SuperPower ();
			}
				
			if (PlayerPrefs.GetInt ("DoubleJump") == 1) //	Poniżej wypełnianie ikony podwójnego skoku na podstawie ilości dostępnych skoków
			{
				doubleJumpText.fillAmount = (float)jumps / 2;
			}
				
			if (slip == 0) // Jeśli gracz potknął się 3 razy uruchamia się QTE
			{  
				scoreText.gameObject.SetActive (false);
				qteObject.SetActive (true);
			} 
			else 
			{
				scoreValue = Vector3.Distance (transform.parent.position, crowd.position); // Wynik jest to dystans jaki pokonał gracz od punktu startowego
				scoreText.text = "Wynik: " + Mathf.Round (scoreValue) + " Rekord: " + PlayerPrefs.GetInt ("Highscore");	//	Wyświetlanie wyniku podczas gry
				//	Poniżej informacja o pobiciu rekordu
				if ((scoreValue > PlayerPrefs.GetInt ("Highscore")) && (isHighscore == false)) 
				{
					newRecordAnim.SetTrigger ("NewRecord");
					isHighscore = true;
					settings.odegrajDzwiek (highscoreSound);
					scoreText.color = Color.green;
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
			settings.odegrajDzwiek(jumpSound);
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpPower);
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
			Rigidbody2D tRigidbody = target.gameObject.GetComponent<Rigidbody2D> ();
			settings.odegrajDzwiek (hitSound);
			target.collider.isTrigger = true;
			if (!superPowerIsActive) 
			{
				slip--;
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
}
