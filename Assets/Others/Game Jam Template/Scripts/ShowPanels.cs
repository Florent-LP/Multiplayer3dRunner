using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public float animationSpeed=0.5f;
	private Animator cameraAnim;
	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject netWorkPanelStep1;

	void Awake ()
	{
		cameraAnim = Camera.main.GetComponent<Animator> ();
	}


	public void ShowNetworkPanel()
	{
		cameraAnim.SetBool ("IsZoom",true);
		StartCoroutine (showNetwork());

	}
	IEnumerator showNetwork()
	{
		menuPanel.SetActive (false);
		yield return new WaitForSeconds(animationSpeed);
		netWorkPanelStep1.SetActive(true);
	}

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		cameraAnim.SetBool ("IsZoom",true);
		StartCoroutine (showOptions());

	}

	IEnumerator showOptions()
	{
		menuPanel.SetActive (false);
		yield return new WaitForSeconds(animationSpeed);
		optionsPanel.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		cameraAnim.SetBool ("IsZoom",false);
		StartCoroutine (hideOptions());

	}

	IEnumerator hideOptions()
	{
		optionsPanel.SetActive (false);
		yield return new WaitForSeconds(animationSpeed);
		menuPanel.SetActive(true);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
	}
}
