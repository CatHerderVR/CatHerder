// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerManager.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in DemoAnimator to deal with the networked player instance
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

#if UNITY_5 && (!UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2 && !UNITY_5_3) || UNITY_6
#define UNITY_MIN_5_4
#endif

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Player manager.
/// Handles fire Input and Beams.
/// </summary>
public class Player : Photon.PunBehaviour, IPunObservable
{
    #region Public Variables

    [Tooltip( "The local player instance. Use this to know if the local player is represented in the Scene" )]
    public static GameObject LocalPlayerInstance;

    public GameObject avatar;

    public Transform playerGlobal;
    public Transform playerLocal;

    #endregion

    #region Private Variables
    #endregion

    #region MonoBehaviour CallBacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    public void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
        if( photonView.isMine )
        {
            LocalPlayerInstance = gameObject;
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad( gameObject );
    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    public void Start()
    {
        Debug.Log( "Player Start" );

        if( photonView.isMine )
        {
            playerGlobal = GameObject.Find( "Camera (eye)" ) ? GameObject.Find( "Camera (eye)" ).transform : GameObject.Find( "Main Camera" ).transform;
            //playerLocal = GameObject.Find( "SteamVR" ) ? GameObject.Find( "Camera (head)" ).transform : GameObject.Find( "Test Camera" ).transform;

            //if( GameObject.Find("SteamVR") && GameObject.Find( "SteamVR" ).activeSelf)
            //    transform.SetParent( GameObject.Find( "Camera (head)" ).transform, false );
            //else
            //    transform.SetParent( GameObject.Find( "Test Camera" ).transform, false );

            //playerGlobal = Camera.main.transform;
            playerLocal = Camera.main.transform;

            transform.SetParent( playerGlobal );

            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.Euler( 0, 0, 0 );
        }
    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity on every frame.
    /// Process Inputs if local player.
    /// Show and hide the beams
    /// Watch for end of game, when local player health is 0.
    /// </summary>
    public void Update()
    {
    }
    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info )
    {
        if( stream.isWriting )
        {
            stream.SendNext( playerGlobal.position );
            stream.SendNext( playerGlobal.rotation );
            //stream.SendNext( playerLocal.localPosition );
            //stream.SendNext( playerLocal.localRotation );
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            //avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            //avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    #endregion
}
