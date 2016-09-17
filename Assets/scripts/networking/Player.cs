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
        if( photonView.isMine )
        {
            transform.SetParent( GameObject.Find( "Camera (head)" ).transform, false );
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
            // We own this player: send the others our data
            //stream.SendNext( this.IsFiring );
            //stream.SendNext( this.Health );
        }
        else
        {
            // Network player, receive data
            //this.IsFiring = (bool)stream.ReceiveNext();
            //this.Health = (float)stream.ReceiveNext();
        }
    }

    #endregion
}
