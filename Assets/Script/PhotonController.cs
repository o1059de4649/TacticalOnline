using System.Collections;

using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon;




public class PhotonController : MonoBehaviourPunCallbacks
{

    public GameObject player;

    public GameObject myplayer;

    public int _TeamNumber;
    public Canvas loginGUI;
    public Camera _startCamera;

    //
    public Text room_name_dropdwon;
    public Text _inputField_text;
   

    public void Start()
    {
        
        PhotonNetwork.PhotonServerSettings.StartInOfflineMode = false;
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.GameVersion = "1";//Application.version;
        PhotonNetwork.ConnectUsingSettings();

       
    }

    public void Update()
    {
        Debug.Log(PhotonNetwork.IsConnected);

    }

    public override void OnConnected()
    {
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected(" + cause + ")");
    }

    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
    }

    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
    }

    public override void OnRegionListReceived(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionListReceived");
        regionHandler.PingMinimumOfRegions(this.OnRegionPingCompleted, null);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
    }

    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Join");
    }

    public override void OnLeftLobby()
    {
    }

    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public override void OnCreatedRoom()
    {
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
       // PhotonNetwork.CreateRoom(null);
    }

    //UIボタン群
    public void NewCreateRoomButton()
    {
        if (_TeamNumber == 0)
        {
            return;
        }

        PhotonNetwork.CreateRoom(_inputField_text.text, null,PhotonNetwork.CurrentLobby);
        loginGUI.enabled = false;
        _startCamera.enabled = false;
    }

    public void PlayButton()
    {
        if (_TeamNumber == 0)
        {
            return;
        }


        PhotonNetwork.JoinRoom(room_name_dropdwon.text);
    }

    public void Team1Deside()
    {
        _TeamNumber = 1;

       

        
       
    }

    public void Team2Deside()
    {
        _TeamNumber = 2;
        
        
    }
    //ここまでUiボタン群

        /*
    void OnReceivedRoomListUpdate()
    {
        // 既存のRoomを取得.
        RoomInfo[] roomInfo = PhotonNetwork.GetRoomList();
        if (roomInfo == null || roomInfo.Length == 0) return;

        // 個々のRoomの名前を表示.
        for (int i = 0; i < roomInfo.Length; i++)
        {
            Debug.Log((i).ToString() + " : " + roomInfo[i].name);
        }
    }*/


    public void OnPhotonRandomJoinFailed()
    {

       // PhotonNetwork.CreateRoom(null);

    }



   public override void OnJoinedRoom()
    {

        myplayer = PhotonNetwork.Instantiate(

            player.name,

            new Vector3(0f, 3f, 0f),

            Quaternion.identity,

            0

         );

        myplayer.GetComponent<MovePlayer>()._teamNumber = _TeamNumber;


    }


    

    
   

    public override void OnLeftRoom()
    {
    }


    /// <summary>A callback of the RegionHandler, provided in OnRegionListReceived.</summary>
    /// <param name="regionHandler">The regionHandler wraps up best region and other region relevant info.</param>
    private void OnRegionPingCompleted(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionPingCompleted " + regionHandler.BestRegion);
        Debug.Log("RegionPingSummary: " + regionHandler.SummaryToCache);
       
    }



}
