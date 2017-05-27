using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	[System.Serializable]
	public struct hints
	{
		public string name;
		public Sprite texture;
		[TextArea(1,5)]
		public string text;
	}
	public GameObject hintObject;
	public Text hintTitle;
	public Image hintImage;
	public Text hintText;
	Canvas canvas;

	[SerializeField]
	public hints[] hintsArray;

	public IEnumerator LoadScene(string sceneName, bool showHint)
	{
		if (showHint) 
		{
			hintObject.SetActive (true);
			int random = Random.Range (0, hintsArray.Length);
			hintTitle.text = hintsArray [random].name;
			hintImage.sprite = hintsArray [random].texture;
			hintText.text = hintsArray [random].text;
			yield return new WaitForSeconds (0.5f);
		}
		AsyncOperation sceneLoad = SceneManager.LoadSceneAsync (sceneName);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
