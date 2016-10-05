using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;

namespace CatHerder
{
	/// <summary>
	/// Game manager.
	/// Connects and watch Photon Status, Instantiate Player
	/// Deals with quiting the room and the game
	/// Deals with level loading (outside the in room synchronization)
	/// </summary>
	public class GameManager : Photon.MonoBehaviour {

        public static string[] CatTypes = { "Siamese", "Black", "Orange", "Striped" };
        public static string[] CatNames = { "Fluffy", "Boris", "Cheetoh", "Keekee", "Milo", "Bernie", "Kai", "Vincent", "Simon" };

        #region Public Variables

        static public GameManager Instance;

        public GameObject roomPrefab;
		public GameObject playerPrefab;
        public GameObject catToyPrefab;

        #endregion

        #region Private Variables

        Dictionary<string, GameObject> _kitties;
        KeywordRecognizer _keywordRecognizer;

        #endregion

        #region MonoBehaviour CallBacks

        void Awake()
        {
            _kitties = new Dictionary<string, GameObject>();

            //shuffle arrays to randomize
            Shuffle( CatNames );
        }

        void Start()
        {
            Instance = this;

            GameObject.Instantiate<GameObject>( roomPrefab );

            _keywordRecognizer = new KeywordRecognizer( CatNames );
            _keywordRecognizer.OnPhraseRecognized += OnKittyName;
            _keywordRecognizer.Start();
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
				

				if (Player.LocalPlayerInstance==null)
				{
					Debug.Log("We are Instantiating LocalPlayer from "+SceneManagerHelper.ActiveSceneName);

					// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
					//PhotonNetwork.Instantiate(this.catToyPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
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
                SpawnCat();
            }

            GameObject rightController = GameObject.Find( "Controller (right)" );
            GameObject leftController = GameObject.Find( "Controller (left)" );

            if( rightController != null )
            {
                var controllerRight = SteamVR_Controller.Input( (int)rightController.GetComponent<SteamVR_TrackedObject>().index );

                if( controllerRight.GetPressUp( SteamVR_Controller.ButtonMask.Touchpad ) == true )
                {
                    SpawnCat();
                }
            }

            if( leftController != null )
            {
                var controllerLeft = SteamVR_Controller.Input( (int)leftController.GetComponent<SteamVR_TrackedObject>().index );

                if( controllerLeft.GetPressUp( SteamVR_Controller.ButtonMask.Touchpad ) == true )
                {
                    SpawnCat();
                }
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

        void SpawnCat()
        {
            if( _kitties.Count < GameManager.CatNames.Length )
            {
                GameObject kitty = GameObject.Instantiate( Resources.Load<GameObject>( "Kitty/Kitty" ), new Vector3( Random.Range( -2, 2 ), 0, Random.Range( -2, 2 ) ), Quaternion.identity ) as GameObject;

                //randomly select skin
                kitty.GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load<Material>( "Kitty/" + GameManager.CatTypes[Random.Range( 0, GameManager.CatTypes.Length )] );

                string strName = GameManager.CatNames[_kitties.Count];
                kitty.GetComponentInChildren<CatLoves>().CatName = strName;
                kitty.name = "Kitty - " + strName;
                _kitties.Add( strName, kitty );
            }
        }

        void OnKittyName( PhraseRecognizedEventArgs args )
        {
            GameObject kitty;
            if( _kitties.TryGetValue( args.text, out kitty ) )
            {
                kitty.GetComponent<CatController>().MakeCatListen();
            }
        }

        void Shuffle( string[] arr )
        {
            for( int i = 0; i < arr.Length; i++ )
            {
                int rand = Random.Range( 0, arr.Length );
                string a = arr[i];
                string b = arr[rand];
                arr[i] = b;
                arr[rand] = a;
            }
        }

		#endregion

	}

}