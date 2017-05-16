using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle2 : Puzzle {

	public GameObject puzzle2;
	public Text timerCountdown;
	private float timeLeft;
	public float timeLeftValue = 5.0f;
	private float timeDestroy;
	public float timeDestroyValue = 1.0f;

	void OnEnable () 
	{
		Debug.Log ("Puzzle2 begins!");
		puzzle2.SetActive (true);
		timeLeft = timeLeftValue;
		timeDestroy = timeDestroyValue;
		timerCountdown.gameObject.SetActive (true);
	}

	void Update () 
	{
		timeLeft -= Time.deltaTime;
		timerCountdown.text = "" + Mathf.Round (timeLeft);

		if ((timeLeft < 0)&&(!isSucceeded))
		{
			Debug.Log ("MISSION FAILED!");
			timerCountdown.text = "FAIL!";
			puzzle2.SetActive (false);
			timeDestroy -= Time.deltaTime;
		} 

		if (isSucceeded)
		{
			{
				timerCountdown.text = "Success!";
				puzzle2.SetActive (false);
				timeDestroy -= Time.deltaTime;
			}
		}
		if (timeDestroy < 0)
		{
			timerCountdown.gameObject.SetActive (false);
			gameObject.SetActive (false);
		}
	}
}
