using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    [SerializeField]
    private float yMax;

    [SerializeField]
    private float yMin;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float xMax;


    private Transform target;

    /*
    private float xMax;
    private float xMin;
    */

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player").transform;
        xMax = target.GetComponent<Transform>().position.x;
        xMin = target.GetComponent<Transform>().position.x;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        xMax = target.GetComponent<Transform>().position.x;
        xMin = target.GetComponent<Transform>().position.x;
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin+3, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
	}
}
