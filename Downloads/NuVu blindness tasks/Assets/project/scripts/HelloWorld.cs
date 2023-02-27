using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour {
	void OnTriggerEnter(Collider collision)
	{
		Debug.Log ("You put the pasta in the bowl");
	}

	void Start(){
		Debug.Log ("???");
	}
}
