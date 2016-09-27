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
public class CatToyController : Photon.PunBehaviour, IPunObservable
{
    #region Public Variables

    [Tooltip( "The local player instance. Use this to know if the local player is represented in the Scene" )]
    public static GameObject LocalPlayerInstance;

    public GameObject ToyPrefab;

    Transform _controllerTransform;

    #endregion

    #region Private Variables
    bool _attached = false;
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
        GameObject.Instantiate( ToyPrefab, transform.FindChild( "ToyHolder" ), false );
    }

    void AttachToyToController()
    {
        GameObject controllerObj = GameObject.Find( "Controller (right)" );

        if( photonView.isMine && controllerObj != null)
        {
            _attached = true;

            _controllerTransform = controllerObj.transform;

            transform.SetParent( _controllerTransform );

            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.Euler( 0, 0, 0 );

            _controllerTransform.FindChild( "Model" ).gameObject.SetActive( false );
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
        if( !_attached )
            AttachToyToController();
    }
    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info )
    {
        if( stream.isWriting )
        {
            stream.SendNext( transform.position );
            stream.SendNext( transform.rotation );
            //stream.SendNext( _controllerTransform.position );
            //stream.SendNext( _controllerTransform.rotation );
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    #endregion
}
