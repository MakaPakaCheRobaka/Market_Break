using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFromGame : MonoBehaviour {

	public GameObject pauseCanvas;
	public Ustawienia ust;
	public Rigidbody2D pRigidbody;

	public void pressPause()
	{
		PlayerMovement pMovement = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();
		pMovement.pauseVelocity (true);
		ust.globalPause (true);
		pauseCanvas.SetActive (true);
		transform.parent.gameObject.SetActive (false);
	}
}
