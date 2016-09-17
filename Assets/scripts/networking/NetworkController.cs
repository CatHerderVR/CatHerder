using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
    string _room = "Cat Herder Room";

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings( "0.1" );
    }

    void OnJoinedLobby()
    {
        Debug.Log( "joined lobby" );

        RoomOptions roomOptions = new RoomOptions() { };
        PhotonNetwork.JoinOrCreateRoom( _room, roomOptions, TypedLobby.Default );
    }

    void OnJoinedRoom()
    {
        Debug.Log( "OnJoinedRoom" );
        PhotonNetwork.Instantiate( "Player", Vector3.zero, Quaternion.identity, 0 );
    }
}