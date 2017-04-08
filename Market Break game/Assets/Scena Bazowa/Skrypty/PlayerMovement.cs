using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D myRigidbody;
	private Rigidbody2D tlum;
    public float jumpSpeed;
    public float moveSpeed;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
		tlum = GameObject.FindGameObjectWithTag ("Tlum").GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {

        

        HandleMovement();
	}

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector3.up * jumpSpeed;
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
			tlum.AddForce (new Vector2 (100, 0) * 40 * Time.deltaTime);
			//tlum.Translate (Vector2.right * Time.deltaTime * 10);
		}
	}
}
