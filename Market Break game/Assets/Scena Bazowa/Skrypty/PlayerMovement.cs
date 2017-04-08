using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D myRigidbody;
	private Rigidbody2D tlum;
    public GameObject Q; //QTE 
    public Image pasek; // pasek QTE
    public float jumpSpeed;
    public float moveSpeed;
    public float wys_pos; // zmianna która pokazuje aktualną wysokość postaci
    public float skok_poz_str; // zmianna zapamiętyje wysokość startową postaci
    public int potk; // zmienna która przechowuje ile razy gracz wpadł na przeszkodę
    public bool wolny = false; // zmianna która przechowuje info czy gracz się uwolnił przed tłumem
    public bool zlapany = false; // zmianna która przechowuje info czy tłum złąpał gracza


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
		tlum = GameObject.FindGameObjectWithTag ("Tlum").GetComponent<Rigidbody2D> ();
        skok_poz_str = myRigidbody.position.y;
        potk = 0;                       // liczba potknięć = 0
        pasek.fillAmount = 0.50f;       // ustawienie paska w połowie
        Q.SetActive(false);           //pasek QTE jest niewidoczny

    }
	
	// Update is called once per frame
	void LateUpdate () {

        wys_pos = myRigidbody.position.y; // aktualna wysokość postaci

        if (potk == 3)  // jeśli gracz potknoł się 3 razy uruchamia się QTE
        {
            QTE();

            if (zlapany)  // jeśli gracz przegrał QTE scena jest restartowana
            {
                SceneManager.LoadScene("scena"); //ładowanie sceny od nowa
            }

            if (wolny) // jeśli gracz wygrał QTE biegnie dalej a tłum się odsuwa
            {              
                tlum.AddForce(new Vector2(-100, 0) * 40 * Time.deltaTime); //odsunięcie tłumu
                Q.SetActive(false);    //pasek QTE jest niewidoczny           
            }
        }
        else
        {
            HandleMovement();
        }

        
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (wys_pos < -1 )
            {
                myRigidbody.velocity = Vector3.up * jumpSpeed;
            }
        }
        else
        myRigidbody.velocity = Vector2.right * moveSpeed; //x=-1, y=0;
    }


	void OnCollisionEnter2D (Collision2D target)
	{
		if (target.gameObject.tag == "Przeszkoda") 
		{
			Debug.Log ("Dotknięcie przeszkody");
			target.gameObject.AddComponent<Rigidbody2D> ();
			target.collider.isTrigger = true;
			tlum.AddForce (new Vector2 (150, 0) * 40 * Time.deltaTime);
            potk++;                                                     // zwiększam liczbe wpadnięć na przeszkodę
            //tlum.Translate (Vector2.right * Time.deltaTime * 10);
        }
	}

    void QTE()
    {
                 
       Q.SetActive(true); //włącza się QTE

        pasek.fillAmount = pasek.fillAmount + 0.2f * Time.deltaTime;  // pasek wypełania się na korzyść tłumu

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
