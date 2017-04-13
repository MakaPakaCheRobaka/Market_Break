using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    private Rigidbody2D tlum;
    public GameObject Q; //QTE 
    public GameObject Game_Over; //Ekran Game Over 
    public GameObject Pasek_QTE;
    public Image pasek; // pasek QTE
    public Transform gracz;
    public Transform punkt_startowy;
    public Text Wynik;          //tekst z wynikiem
    public Text Wynik_game_over;     //tekst z wynikiem w ekranie game over
    public GameObject Wynik2;  //wynik podczas rozgrywki
    public float jumpSpeed;
    public float moveSpeed;
    public float wys_pos; // zmianna która pokazuje aktualną wysokość postaci
    public float skok_poz_str; // zmianna zapamiętyje wysokość startową postaci
    public int potk; // zmienna która przechowuje ile razy gracz wpadł na przeszkodę
    public bool wolny = false; // zmianna która przechowuje info czy gracz się uwolnił przed tłumem
    public bool zlapany = false; // zmianna która przechowuje info czy tłum złąpał gracza
    public float wynik;           // zmienna przechowuje info o wyniku gracza
    public AudioClip uderzenieDzwiek; // dźwięk uderzenie w przeszkodę
    Ustawienia ust; // ustawienia dźwięku, muzyki i odgrywanie dźwięków
    public float QTE_speed; // szybkość napełniania się paska QTE na korzyść tłumu
	bool jump = false;

    // Use this for initialization
    void Start()
    {
        ust = GameObject.Find("Ustawienia").GetComponent<Ustawienia>();
        myRigidbody = GetComponent<Rigidbody2D>();
        tlum = GameObject.FindGameObjectWithTag("Tlum").GetComponent<Rigidbody2D>();
        skok_poz_str = myRigidbody.position.y;
        potk = 0;                       // liczba potknięć = 0
        pasek.fillAmount = 0.50f;       // ustawienie paska w połowie
        Q.SetActive(false);             //pasek QTE jest niewidoczny
        Game_Over.SetActive(false);     //pasek GAme Over jest niewidoczny
        wynik = 0f;                     //zerowanie wyniku
        QTE_speed = 0.2f;
   
    }

    // Update is called once per frame
    void LateUpdate()
    {



        wys_pos = myRigidbody.position.y; // aktualna wysokość postaci

        if (potk == 3)  // jeśli gracz potknoł się 3 razy uruchamia się QTE
        {
            Wynik2.SetActive(false);
            QTE();

            if (zlapany)  // jeśli gracz przegrał QTE wyświetla się ekran Game Over z wynikiem i przyciskami menu i restartu
            {
                Debug.Log("Złapany");
                Wynik2.SetActive(false);
                Pasek_QTE.SetActive(false);
                Wynik_game_over.text = "Twój wynik : " + Mathf.Round(wynik);
                Game_Over.SetActive(true);
                
            }

            if (wolny) // jeśli gracz wygrał QTE biegnie dalej a tłum się odsuwa
            {
                Debug.Log("Uwolnił");
                tlum.AddForce(new Vector2(-300, 0) * 40 * Time.deltaTime); //odsunięcie tłumu
                Q.SetActive(false);    //pasek QTE jest niewidoczny  
                Wynik2.SetActive(true);
                potk = 0;
                wolny = false;
                pasek.fillAmount = 0.5f;
                QTE_speed = QTE_speed + 0.01f;
            }
        }
        else
        {
            wynik = Vector3.Distance(gracz.position, punkt_startowy.position); // wynik jest to dystans jaki pokonał gracz od punktu startowego
            Wynik.text = "" + Mathf.Round(wynik);  // wyświetla zaokrąglony wynik 
            HandleMovement();
        }


    }

    private void HandleMovement()
    {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
			if (wys_pos < -1) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpSpeed);
			}
		} 
		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);
    }


    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Przeszkoda")
        {
            ust.odegrajDzwiek(uderzenieDzwiek);
            Debug.Log("Dotknięcie przeszkody");
            target.gameObject.AddComponent<Rigidbody2D>();
            target.collider.isTrigger = true;
            tlum.AddForce(new Vector2(150, 0) * 40 * Time.deltaTime);
            potk++;                                                     // zwiększam liczbe wpadnięć na przeszkodę
            //tlum.Translate (Vector2.right * Time.deltaTime * 10);
        }
    }

    void QTE()
    {

        Q.SetActive(true); //włącza się QTE

        pasek.fillAmount = pasek.fillAmount + QTE_speed * Time.deltaTime;  // pasek wypełania się na korzyść tłumu

        if (Input.GetMouseButtonDown(0)) //gracz musi szybko klikać aby się uwolnić od tłumu
        {
            pasek.fillAmount = pasek.fillAmount - 0.055f;
        }

        if (pasek.fillAmount < 0.01)
        {
            wolny = true;
            Debug.Log("Wolny");
        }

        if (pasek.fillAmount >= 1)
        {
            zlapany = true;
            Debug.Log("Złapany");
        }

    }

}
