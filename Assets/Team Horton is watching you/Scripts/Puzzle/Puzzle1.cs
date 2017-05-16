using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle1 : Puzzle {

	public GameObject puzzle1;
	public Text timerCountdown;
	private float timeLeft;
	public float timeLeftValue = 5.0f;
	private float timeDestroy;
	public float timeDestroyValue = 1.0f;

	void OnEnable () 
	{
		Debug.Log ("Puzzle1 begins!");
		puzzle1.SetActive (true);
		timeLeft = timeLeftValue;
		timeDestroy = timeDestroyValue;
		timerCountdown.gameObject.SetActive (true);
		isSpotted = false;
	}
	
	void Update () 
	{
		timeLeft -= Time.deltaTime;
		timerCountdown.text = "" + Mathf.Round (timeLeft);

		if (isSpotted) 
		{
			Debug.Log ("SPOTTED BRO!");
			timerCountdown.text = "FAIL!";
			puzzle1.SetActive (false);
			timeDestroy -= Time.deltaTime;
		} 

		if ((timeLeft < 0)&&(!isSpotted))
		{
			{
				timerCountdown.text = "Success!";
				isSucceeded = true;
				puzzle1.SetActive (false);
				timeDestroy -= Time.deltaTime;
			}
		}
		if (timeDestroy < 0)
		{
			timerCountdown.gameObject.SetActive (false);
			isSpotted = false;
			gameObject.SetActive (false);
		}
	}
}
