using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFromGame : MonoBehaviour {

	public GameObject pauseCanvas;
	public Ustawienia ust;
	public Rigidbody2D pRigidbody;

	public void pressPause()
	{
		pRigidbody.velocity = Vector2.zero;
		ust.globalPause (true);
		pauseCanvas.SetActive (true);
		transform.parent.gameObject.SetActive (false);
	}
}
