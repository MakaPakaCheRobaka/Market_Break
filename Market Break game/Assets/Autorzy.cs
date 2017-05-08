using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Autorzy : MonoBehaviour {

    public Text a1;
    public Text a2;
    public Text a3;
    public Text a4;
    // Use this for initialization
    

   public void Pokaż_autorów()
    {
        a1.text = "Jakub Bartkowiak ";
        a2.text = "Jakub Cibail";
        a3.text = "Aleksander Lewandowski";
        a4.text = "Hubert Owocki";

    }
}
