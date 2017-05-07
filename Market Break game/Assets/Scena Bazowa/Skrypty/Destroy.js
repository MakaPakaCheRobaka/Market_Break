#pragma strict

var destroyTime = 5;
public var ust : Ustawienia; 

function Start()
{
	ust = GameObject.FindWithTag("Ustawienia").GetComponent("Ustawienia");
}

function DestroyObject()
{
	Destroy(gameObject);
}

function Update() 
{
	if(!IsInvoking("DestroyObject"))
	{
	Invoke("DestroyObject", destroyTime);
	}
	if(ust.spawnerPause)
	{
	CancelInvoke("DestroyObject");
	}
}
