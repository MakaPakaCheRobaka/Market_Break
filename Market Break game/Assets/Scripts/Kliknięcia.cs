using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kliknięcia : MonoBehaviour
{

    public Text klikniecia;
    public int klik = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            klik++;
            klikniecia.text = "kliknięcia : " + klik;
        }

        /*
        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                klik++;
                klikniecia.text = "kliknięcia : " + klik;
            }
        }
        */

        }
}