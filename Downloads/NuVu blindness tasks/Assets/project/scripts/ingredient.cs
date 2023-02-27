using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingredient : MonoBehaviour {
	public Animator animator;
	public string animationName = "rotate";
	void Start(){
		Debug.Log (name);
	}
	void OnTriggerEnter(Collider col){
		Debug.Log (col.name);
		if (col.name == "bowl") 
		{ 
			if (FindObjectOfType<Recipe> ().AddIngredient (name) == true) {
				animator.SetTrigger (animationName);
			}



		}
	}
}
