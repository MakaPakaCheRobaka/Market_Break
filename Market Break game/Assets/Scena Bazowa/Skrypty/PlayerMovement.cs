﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D myRigidbody;
    public float jumpSpeed;
    public float moveSpeed;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
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
}