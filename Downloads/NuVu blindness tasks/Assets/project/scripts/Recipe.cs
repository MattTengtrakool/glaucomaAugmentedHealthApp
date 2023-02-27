using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour {
	string[] ingredients = new string[] 
	{
		"Pasta_Pot",
		"Marinara",
		"Salt"
	};

	int index = 0;

	bool winner = false;


	void Update()
	{
		if (index == ingredients.Length && !winner) {
			Debug.Log ("You've accomplished your task");
			winner = true;
		}
	}


	public bool AddIngredient(string ing)
	{
		string correctIngredient = ingredients [index];
		if (correctIngredient == ing) {
			Debug.Log ("added " + ing);
			index++;
			return true;
		}
		else {
			Debug.Log ("That's not the correct ingredient");
			return false;
		}
	}

 
}