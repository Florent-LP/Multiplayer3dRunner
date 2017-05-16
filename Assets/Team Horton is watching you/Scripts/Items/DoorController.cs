using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	private Animator animator;
	private bool canOpen;

	void Start () 
	{
		animator = GetComponent<Animator> ();
		canOpen = false;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.X) && canOpen) 
		{
			Debug.Log ("APPUIE SUR X!");
			animator.SetBool ("open", true);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.gameObject.tag == "Player")
		{
			Debug.Log ("Player touched me!!!!!");
			canOpen = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.transform.gameObject.tag == "Player")
		{
			Debug.Log ("GET OUT!!!!!");
			animator.SetBool ("open", false);
			canOpen = false;
		}
	}
}
