using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour {

	Text tip;
	Animator fAnim;
	AnimatorStateInfo currentState;
	public bool isWorking = false;
	Ustawienia ust;

	List<int> idList = new List<int> ();

	[System.Serializable]
	public struct tipsTextStruct
	{
	[TextArea(1,5)]
	public string tipsText;
	public bool wasUsed;
	}

	public tipsTextStruct[] tipsTexts;

	// Use this for initialization
	void Start () 
	{
		ust = GameObject.Find ("Ustawienia").GetComponent<Ustawienia> ();
		fAnim = GetComponent<Animator> ();
		tip = GetComponentInChildren<Text> ();
	}
		
	public void tips (int id)
	{
		idList.Add (id);
	}

	public void Pause()
	{
		ust.paused = true;
	}

	public void UnPause()
	{
		ust.paused = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if ((idList.Count > 0) && (!isWorking) && (tipsTexts [idList [0]].wasUsed == false)) 
		{
			isWorking = true;
			fAnim.SetTrigger ("FadeIn");
			tip.text = tipsTexts [idList [0]].tipsText;
			tipsTexts [idList [0]].wasUsed = true;
			idList.RemoveAt (0);
		} 
		else if (idList.Count > 0) 
		{
			if(tipsTexts [idList [0]].wasUsed == false) idList.RemoveAt (0);
		}
		if (isWorking) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				fAnim.SetTrigger ("Skip");
			}
		}
	}
}
