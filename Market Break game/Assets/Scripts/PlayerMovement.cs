using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    public int collectMoney;

	//Obiekty tłumu
	Transform crowd;

	//Canvasy
	public GameObject gameOverCanvas; //Ekran Game Over
	public GameObject gameCanvas;
	public GameObject upgradeCanvas;
	public GameObject qteCanvas; //QTE 

	//Obiekty wyniku
    public Text scoreText;          //tekst z wynikiem
    public Text MoneyText;
	public Text highscoreText;
	[HideInInspector]
	public bool isHighscore = false;
	public Animator newRecordAnim;	// Animacja pobicia rekordu
	public float scoreValue;           // zmienna przechowuje info o wyniku gracza

	//Obiekty dźwięków i muzyki
	public AudioClip hitSound; // dźwięk uderzenia w przeszkodę
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

    // Use this for initialization
    void Start()
    {
		settings = GameObject.FindWithTag ("Ustawienia").GetComponent<Ustawienia> ();
		gAnim = GetComponent<Animator> ();
		newRecordAnim = GameObject.Find("NewRecordText").GetComponent<Animator> ();
        myRigidbody = GetComponent<Rigidbody2D>();
        crowd = GameObject.FindGameObjectWithTag("Tlum").GetComponent<Transform>();
        scoreValue = 0f;                     //zerowanie wyniku
        settings.wlaczMuzyke(gameMusic);
		highscoreText.text = "Rekord: " + PlayerPrefs.GetInt ("Highscore");
		if (!PlayerPrefs.HasKey ("Money")) 
		{
			PlayerPrefs.SetInt ("Money", 0);
		}
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
    void LateUpdate()
	{
		if (myRigidbody.velocity.y < 0)  //	Włączanie odpowiedniej animacji skoku na podstawie kierunku lotu
		{
			gAnim.SetBool ("JumpDown", true);
			gAnim.SetBool ("JumpUp", false);
		}
		if (!settings.movementPause)	//	Sprawdzanie, czy gra nie została spauzowana 
		{
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
				qteCanvas.SetActive (true);
			} 
			else 
			{
				scoreValue = Vector3.Distance (transform.parent.position, crowd.position); // Wynik jest to dystans jaki pokonał gracz od punktu startowego
				scoreText.text = "Wynik: " + Mathf.Round (scoreValue);
				//	Poniżej informacja o pobiciu rekordu
				if ((scoreValue > PlayerPrefs.GetInt ("Highscore")) && (isHighscore == false)) 
				{
					newRecordAnim.SetTrigger ("NewRecord");
					isHighscore = true;
					settings.odegrajDzwiek (highscoreSound);
					scoreText.color = Color.green;
				}
			}
			HandleMovement ();
		}
    }

	public void Jump()
	{
		if (jumps > 0) 
		{
			Debug.Log ("Jump");
			jumps--;
			gAnim.SetBool ("JumpUp", true);
			gAnim.SetBool ("JumpDown", false);
			settings.odegrajDzwiek(jumpSound);
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpPower);
		}
	}

    private void HandleMovement()	// Funkcja odpowiedzialna za ruch w prawo i wykonywanie skoku
    {
		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);
    }

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Money")
		{
			Debug.Log("PlayerHitMoney");
			target.gameObject.SetActive(false);
			collectMoney += 1;
			MoneyText.text = "Kasa: " + collectMoney;
		}
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

 
}
