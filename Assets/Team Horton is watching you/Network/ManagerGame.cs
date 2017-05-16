// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Launcher.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in "PUN Basic tutorial" to handle typical game management requirements
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

namespace ExitGames.Demos.DemoAnimator
{
	/// <summary>
	/// Game manager.
	/// Connects and watch Photon Status, Instantiate Player
	/// Deals with quiting the room and the game
	/// Deals with level loading (outside the in room synchronization)
	/// </summary>
	public class ManagerGame : Photon.MonoBehaviour {


		#region Public Variables

		static public ManagerGame Instance;

		[Tooltip("The prefab to use for representing the player")]
		public GameObject playerPrefab;
		public List<GameObject> puzzlePrefabs;
		public GameObject beginRoom;
		public GameObject endRoom;
		public int nbPuzzles;
		#endregion

		#region Private Variables

		private GameObject instance;

		#endregion

		#region MonoBehaviour CallBacks

		public Transform spawnerContainer;
		public List<Transform> spawners;
		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{

			spawners = new List<Transform>(spawnerContainer.GetComponentsInChildren<Transform> ());
			int randomSpawn = UnityEngine.Random.Range (0, spawners.Count);
			Instance = this;

			// in case we started this demo with the wrong scene being active, simply load the menu scene
			if (!PhotonNetwork.connected)
			{
				//SceneManager.LoadScene("Name and Room");

				//return;
			}
			/*
			if (playerPrefab == null) { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
				
				Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			} else {
				

				if (PlayerManager.LocalPlayerInstance==null)
				{
					Debug.Log("We are Instantiating LocalPlayer from "+SceneManagerHelper.ActiveSceneName);

			*/		// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
			PhotonNetwork.Instantiate("Prefab/Player/" + this.playerPrefab.name, spawners[randomSpawn].position, Quaternion.identity, 0);
			spawners.RemoveAt (randomSpawn);
			/*	}else{ 	

					Debug.Log("Ignoring scene load for "+ SceneManagerHelper.ActiveSceneName);
				}

				
			}
			*/

			if (PhotonNetwork.isMasterClient) {
				GameObject current = beginRoom;
				GameObject next = null;
				Out outComp = null;
				In inComp = null;
				Vector3 distance = Vector3.zero;
				for (int i = 0; i < nbPuzzles; i++) {
					int alea = UnityEngine.Random.Range (0, puzzlePrefabs.Count);
					Debug.Log (puzzlePrefabs [alea].name);
					next = PhotonNetwork.Instantiate ("Prefab/Corridors/" + puzzlePrefabs [alea].name, Vector3.zero, Quaternion.identity, 0);
					outComp = current.GetComponentInChildren<Out> ();
					inComp = next.GetComponentInChildren<In> ();
					next.transform.forward = outComp.point.forward;
					distance = inComp.point.position - outComp.point.position;
					next.transform.position -= distance;
					current = next;
				}
				outComp = current.GetComponentInChildren<Out> ();
				inComp = endRoom.GetComponentInChildren<In> ();
				next.transform.forward = outComp.point.forward;
				distance = inComp.point.position - outComp.point.position;
				endRoom.transform.position -= distance;
			}

		}

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity on every frame.
		/// </summary>
		void Update()
		{
			/*
			// "back" button of phone equals "Escape". quit app if that's pressed
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				QuitApplication();
			}
			*/
		}

		#endregion

		#region Photon Messages
		/*
		/// <summary>
		/// Called when a Photon Player got connected. We need to then load a bigger scene.
		/// </summary>
		/// <param name="other">Other.</param>
		public void OnPhotonPlayerConnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting

			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected

				LoadArena();
			}
		}

		/// <summary>
		/// Called when a Photon Player got disconnected. We need to load a smaller scene.
		/// </summary>
		/// <param name="other">Other.</param>
		public void OnPhotonPlayerDisconnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerDisconnected() " + other.NickName ); // seen when other disconnects

			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected

				LoadArena();
			}
		}
		*/

		/// <summary>
		/// Called when the local player left the room. We need to load the launcher scene.
		/// </summary>
		public virtual void OnLeftRoom()
		{
			SceneManager.LoadScene("RootScene");
		}

		#endregion

		#region Public Methods

		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}

		public void QuitApplication()
		{
			Application.Quit();
		}

		#endregion

		#region Private Methods
		/*
		void LoadArena()
		{
			if ( ! PhotonNetwork.isMasterClient ) 
			{
				Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
			}

			Debug.Log( "PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount ); 

			PhotonNetwork.LoadLevel("PunBasics-Room for "+PhotonNetwork.room.PlayerCount);
		}
		*/
		#endregion

	}

}
