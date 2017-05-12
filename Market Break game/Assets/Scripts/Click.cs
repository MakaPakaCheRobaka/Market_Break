using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

	public bool jumpClick;

	void OnMouseDown()
	{
		jumpClick = true;
	}
}
