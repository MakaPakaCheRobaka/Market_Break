using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tłum_test : MonoBehaviour
{
    public float dist = 0; //dystans między tłumem a graczem
    public float d = 1f;  //odległość która umożliwia tłumowi złapanie gracza
    public Transform gracz;
    public Transform tłum;
    Vector3 startpos;     // pozycja startowa tłumu
    public Text klikniecia;  //liczba kliknięć bierząca
    public Text klikniecia2; //liczba kliknięć poprzednia
    public int klik = 0;
    public Image pasek;  // pasek QTE
    public GameObject QTE;
    public Text QTE_g;     // testowy licznik wygranych gracza
    public int QTE_G = 0;  // testowy licznik wygranych gracza
    public Text QTE_t;     // testowy licznik wygranych tłumu
    public int QTE_T = 0;  // testowy licznik wygranych tłumu
    public bool reset = false; // czy zresetować scene

    void Start()
    {
        startpos = transform.position; 
        pasek.fillAmount = 0.50f;       // ustawienie paska w połowie
        QTE.SetActive(false);           //pasek QTE jest niewidoczny
    }

    
    void Update()
    {
        dist = Vector3.Distance(gracz.position, tłum.position);  //dystans między tłumem a graczem


        
        
        if (dist > d) // tłum porusza się póki nie złapie gracza
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        }

        if (dist < d) // tłum złapał gracza
        {
            QTE.SetActive(true); //włącza się QTE

            pasek.fillAmount = pasek.fillAmount + 0.2f * Time.deltaTime;  // pasek wypełania się na korzyść tłumu

            if (Input.GetMouseButtonDown(0)) // zlicza ile kliknięć gracz potrzebuje aby wygrać
            {
                klik++;
                klikniecia.text = "kliknięcia : " + klik;
            }

            if (Input.GetMouseButtonDown(0)) //gracz musi szybko klikać aby się uwolnić od tłumu
            {
                pasek.fillAmount = pasek.fillAmount - 0.055f;
            }

            if(pasek.fillAmount < 0.01)
            {
                QTE_G++;
                QTE_g.text = ""+QTE_G;
                reset = true;
            }

            if(pasek.fillAmount >= 1)
            {
                QTE_T++;
                QTE_t.text = "" + QTE_T;
                reset = true;
            }

            if(reset)
            {
               transform.position = startpos;
               pasek.fillAmount = 0.50f;
               reset = false;
               QTE.SetActive(false);
               klikniecia2.text = "kliknięcia : " + klik;
               klik = 0;
               klikniecia.text = "kliknięcia : " + klik;
            }

        }



    }
}
