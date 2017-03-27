using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Event))]
public class EventInspector : Editor {

    Event myTarget;

    void dodajZdarzenie(Event myTarget)
    {
        myTarget.evs.Add(new Event.events()
        {
            sceneName = "",
            zdrz = Event.zdarzenie.Muzyka,
            objectDestroy = null,
            sound = null,
            audioLoop = false,
            audioOnOff = true,
            przełącznikZmiana = 0,
            przełącznikStan = false,
            przełącznikWarunek = 0,
            wrStan = true,
            onOffObj = null,
            onOff = true,
        });
    }

    void usunZdarzenie(int usun)
    {
        myTarget.evs.RemoveAt(usun);
    }

    void upDown(int i, int zmiana)
    {
        dodajZdarzenie(myTarget);

        Event.events temp = myTarget.evs[i];
        myTarget.evs[i] = myTarget.evs[i + zmiana];
        myTarget.evs[i + zmiana] = temp;

        usunZdarzenie(myTarget.evs.Count - 1);

    }

    void events(Event myTarget, int eventLevel)
    {
        for (int i = 0; i < myTarget.evs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            Event.events temp = myTarget.evs[i];
            EditorGUI.indentLevel = temp.eventLevel;
            temp.eventLevel = 0;

            temp.zdrz = (Event.zdarzenie)EditorGUILayout.EnumPopup("Zdarzenie " + i.ToString(), temp.zdrz);
            if (GUILayout.Button("+"))
            {
                if (i < myTarget.evs.Count - 1) upDown(i, 1);
                temp = myTarget.evs[i];
            }
            if (GUILayout.Button("-"))
            {
                if (i > 0) upDown(i, -1);
                temp = myTarget.evs[i];
            }
            if (GUILayout.Button("Usuń"))
            {
                usunZdarzenie(i);
                temp = myTarget.evs[i];
            }
            EditorGUILayout.EndHorizontal();

            if (temp.zdrz == Event.zdarzenie.ZmianaSceny)
            {
                temp.sceneName = EditorGUILayout.TextField(temp.sceneName);
            }
            else if (temp.zdrz == Event.zdarzenie.Przełącznik)
            {
                temp.przełącznikZmiana = EditorGUILayout.IntField(temp.przełącznikZmiana);
                temp.przełącznikStan = EditorGUILayout.Toggle("Stan", temp.przełącznikStan);
            }
            else if (temp.zdrz == Event.zdarzenie.Warunek)
            {
                temp.wr = (Event.war)EditorGUILayout.EnumPopup(temp.wr);
                if (temp.wr == Event.war.Przełącznik)
                {
                    EditorGUILayout.BeginHorizontal();
                    temp.przełącznikWarunek = EditorGUILayout.IntField("Numer: ", temp.przełącznikWarunek);
                    temp.wrStan = EditorGUILayout.Toggle(temp.wrStan);
                    EditorGUILayout.EndHorizontal();
                    temp.warKoniec = EditorGUILayout.IntField("Koniec index:", temp.warKoniec);
                    if ((temp.warKoniec < i + 1) || (temp.warKoniec > myTarget.evs.Count - 1)) temp.warKoniec = i + 1;
                }
                for (int j = i + 1; j <= temp.warKoniec; j++)
                {
                    Event.events temp2 = myTarget.evs[j];
                    temp2.eventLevel = temp.eventLevel + 1;
                    myTarget.evs[j] = temp2;
                }
            }
            else if (temp.zdrz == Event.zdarzenie.OdegrajDźwięk)
            {
                temp.sound = (AudioClip)EditorGUILayout.ObjectField(temp.sound, typeof(AudioClip), true);
            }
            else if (temp.zdrz == Event.zdarzenie.Muzyka)
            {
                temp.audioOnOff = EditorGUILayout.Toggle("Włącz/Wyłącz", temp.audioOnOff);
                if (temp.audioOnOff)
                {
                    temp.sound = (AudioClip)EditorGUILayout.ObjectField(temp.sound, typeof(AudioClip), true);
                    temp.audioLoop = EditorGUILayout.Toggle("Zapętlić? ", temp.audioLoop);
                }
            }
            else if (temp.zdrz == Event.zdarzenie.WłączWyłączObiekt)
            {
                temp.onOffObj = (GameObject)EditorGUILayout.ObjectField(temp.onOffObj, typeof(GameObject), true);
                temp.onOff = EditorGUILayout.Toggle(temp.onOff);
            }
            else if (temp.zdrz == Event.zdarzenie.ZniszczObiekt)
            {
                temp.objectDestroy = (GameObject)EditorGUILayout.ObjectField(temp.objectDestroy, typeof(GameObject), true);
            }
            myTarget.evs[i] = temp;
        }
    }

    public override void OnInspectorGUI()
    {
        myTarget = (Event)target;
        myTarget.warunek = EditorGUILayout.Toggle("Warunek", myTarget.warunek);
        if (myTarget.warunek)
        {
            myTarget.con = EditorGUILayout.Popup("Warunek", myTarget.con, myTarget.cond);
            switch (myTarget.con)
            {
                case 0:
                    {
                        EditorGUILayout.BeginHorizontal();
                        myTarget.przełącznikWarunek = EditorGUILayout.IntField("Numer: ", myTarget.przełącznikWarunek);
                        myTarget.warunekStan = EditorGUILayout.Toggle(myTarget.warunekStan);
                        EditorGUILayout.EndHorizontal();
                        break;
                    }
            }
        }
        EditorGUILayout.Separator();
        myTarget.akt = EditorGUILayout.Popup("Aktywacja", myTarget.akt, myTarget.aktywacja);

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Obecnie aktywny: " + myTarget.currActive);

        events(myTarget, 0);

        if (GUILayout.Button("Dodaj Zdarzenie"))
        {
            dodajZdarzenie(myTarget);
        }

        if (GUI.changed)
        {
            EditorGUILayout.IntField(myTarget.akt);
            EditorUtility.SetDirty(target);
        }
    }
}
