using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable, ExecuteInEditMode()]
public class Event : MonoBehaviour {

    public int actType;
    public int currActive = -1;
    AudioSource source;
    public EventManager evMan;
    public bool warunek;
    public int con;
    public int przełącznikWarunek;
    public bool warunekStan;
    public int akt = 1;
    public string[] aktywacja = { "Kliknięcie", "Auto" };
    public string[] cond = { "Przełącznik", "Zmienna" };
    public Rigidbody2D rBody;
    Vector2 mousePos;


    public enum zdarzenie
    {
        ZmianaSceny,
        ZmianaKoloru,
        Przełącznik,
        Warunek,
        OdegrajDźwięk,
        Muzyka,
        WłączWyłączObiekt,
        ZniszczObiekt,
        WyjdźZGry
    };

    [Serializable]
    public struct events
    {
        public string sceneName;
        public zdarzenie zdrz;
        public GameObject objectDestroy;
        public AudioClip sound;
        public bool audioLoop;
        public bool audioOnOff;
        public int przełącznikZmiana;
        public bool przełącznikStan;
        public int przełącznikWarunek;
        public bool wrStan;
        public int warKoniec;
        public GameObject onOffObj;
        public bool onOff;
        public int eventLevel;
        public war wr;
    };

    public enum war
    {
        Przełącznik,
        Zmienna
    }

    public List<events> evs = new List<events>();

	// Use this for initialization
	void Start () {
        evMan = FindObjectOfType < EventManager >();
        source = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        if (akt == 1)
        {
            StartCoroutine(eventStart());
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if(akt == 0)
        {
            StartCoroutine(eventStart());
        }
    }

    IEnumerator audioOff()
    {
        for (int j = (int)source.volume * 10; j >= 0; j--)
        {
            source.volume = (float)j / 10;
            yield return new WaitForSeconds((float)0.3);
            Debug.Log(j);
        }
        source.clip = null;
        evMan.Wyłącz(0);
    }

    IEnumerator eventStart()
    {
        /*if (warunek == true)
        {
            if (con == 0)
            {
                if (Przełączniki.przelacznik[przełącznikWarunek] != warunekStan)
                {
                    yield break;
                }
            }
        }*/
        evMan.eventActive.Add(false);
        Debug.Log("Event Start");
        for (int i = 0; i < evs.Count; i++)
        {
            //PM.ruch = true;
            Debug.Log(i + " " + evs[i].zdrz);
            evMan.eventActive[0] = true;
            currActive = i;
            switch (evs[i].zdrz)
            {
                case zdarzenie.ZmianaSceny:
                    {
                        SceneManager.LoadScene(evs[i].sceneName);
                        evMan.Wyłącz(0);
                        break;
                    }
                case zdarzenie.Przełącznik:
                    {
                        evMan.przelacznik[evs[i].przełącznikZmiana] = evs[i].przełącznikStan;
                        evMan.Wyłącz(0);
                        break;
                    }
                case zdarzenie.Warunek:
                    {
                        if (evMan.przelacznik[evs[i].przełącznikWarunek] != evs[i].wrStan) i = evs[i].warKoniec;
                        evMan.Wyłącz(0);
                        break;
                    }
                case zdarzenie.OdegrajDźwięk:
                    {
                        source.PlayOneShot(evs[i].sound);
                        while (source.isPlaying) yield return null;
                        evMan.Wyłącz(0);
                        break;
                    }
                case zdarzenie.Muzyka:
                    {
                        if (evs[i].audioOnOff)
                        {
                            source.clip = evs[i].sound;
                            if (evs[i].audioLoop) source.loop = true;
                            else source.loop = false;
                            source.Play();
                            evMan.Wyłącz(0);
                        }
                        else
                        {
                            StartCoroutine(audioOff());
                        }
                        break;
                    }
                case zdarzenie.WłączWyłączObiekt:
                    {
                        if (evs[i].onOff == true)
                        {
                            evs[i].onOffObj.SetActive(true);
                        }
                        else evs[i].onOffObj.SetActive(false);
                        evMan.Wyłącz(0);
                        break;
                    }
                case zdarzenie.ZniszczObiekt:
                    {
                        evMan.Wyłącz(0);
                        DestroyObject(evs[i].objectDestroy);
                        break;
                    }
                case zdarzenie.WyjdźZGry:
                    {
                        Application.Quit();
                        break;
                    }
            }
            while (evMan.eventActive[0])
            {
                yield return null;
            }
        }
        Input.ResetInputAxes();
        evMan.eventActive.RemoveAt(0);
        yield break;
    }
}
