using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePuzzle : MonoBehaviour {

	public GameObject puzzleObj;
	private Puzzle puzzle;

	void Start()
	{
		puzzle = puzzleObj.GetComponent<Puzzle> ();
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.transform.gameObject.tag == "Player")
		{
			if(!puzzle.isSucceeded)
			{
				Debug.Log ("Player touched me!!!!!");
				puzzleObj.SetActive (true);
			}
		}
	}
		
}
