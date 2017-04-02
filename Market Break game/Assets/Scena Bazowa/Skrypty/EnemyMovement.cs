using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Transform target;

    public float xMax;
    public float xMin;

    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Tlum").transform;
        xMax = target.GetComponent<Transform>().position.x;
        xMin = target.GetComponent<Transform>().position.x;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
       // if (myRigidbody.position.x )
       // {
            target.transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), transform.position.y, transform.position.y);
       // }
    }
}
