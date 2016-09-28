﻿// --------------------------------------------------------------------------------------------------------------------
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

namespace ExitGames.Demos.DemoAnimator
{
	/// <summary>
	/// Game manager.
	/// Connects and watch Photon Status, Instantiate Player
	/// Deals with quiting the room and the game
	/// Deals with level loading (outside the in room synchronization)
	/// </summary>
	public class GameManager : Photon.MonoBehaviour {

		#region Public Variables

		static public GameManager Instance;

        public GameObject roomPrefab;
		public GameObject playerPrefab;
        public GameObject catToyPrefab;

		#endregion

		#region Private Variables

		private GameObject instance;

        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            Instance = this;

            GameObject.Instantiate<GameObject>( Resources.Load<GameObject>( "Room" ) );
        }


        public void SpawnPlayer()
        { 
			//// in case we started this demo with the wrong scene being active, simply load the menu scene
			//if (!PhotonNetwork.connected)
			//{
			//	SceneManager.LoadScene("PunBasics-Launcher");

			//	return;
			//}

			if (playerPrefab == null) { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
				
				Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			} else {
				

				if (PlayerManager.LocalPlayerInstance==null)
				{
					Debug.Log("We are Instantiating LocalPlayer from "+SceneManagerHelper.ActiveSceneName);

					// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
					PhotonNetwork.Instantiate(this.catToyPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
				}else{

					Debug.Log("Ignoring scene load for "+ SceneManagerHelper.ActiveSceneName);
				}

				
			}

		}

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity on every frame.
		/// </summary>
		void Update()
		{
			// "back" button of phone equals "Escape". quit app if that's pressed
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				QuitApplication();
			}

            //if(Input.GetKeyUp(KeyCode.V))
            //{
            //    GameObject.Find( "SteamVR" ).SetActive( true );
            //    GameObject.Find( "Test Camera" ).SetActive( false );
            //}

            if( Input.GetKeyUp( KeyCode.Space ) )
            {
                GetComponent<CatSpawner>().SpawnCat();
            }

        }

        #endregion

        #region Photon Messages

        /// <summary>
        /// Called when a Photon Player got connected. We need to then load a bigger scene.
        /// </summary>
        /// <param name="other">Other.</param>
        public void OnPhotonPlayerConnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerConnected() " + other.name ); // not seen if you're the player connecting

			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected

			}
		}

		/// <summary>
		/// Called when a Photon Player got disconnected. We need to load a smaller scene.
		/// </summary>
		/// <param name="other">Other.</param>
		public void OnPhotonPlayerDisconnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerDisconnected() " + other.name ); // seen when other disconnects

			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected
				
			}
		}

		/// <summary>
		/// Called when the local player left the room. We need to load the launcher scene.
		/// </summary>
		public virtual void OnLeftRoom()
		{
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
		#endregion

	}

}