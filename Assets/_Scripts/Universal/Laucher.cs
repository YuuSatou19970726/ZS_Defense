using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ZSDefense;

namespace ZSDefense
{
    public class Laucher : MonoBehaviourPunCallbacks
    {
        private static Laucher instance;
        public static Laucher Instance => instance;

        private bool hasSetNick = false;

        [SerializeField]
        private string[] allMap;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            this.CloseMenus();
            Debug.Log("Connecting to Network...");

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void CreateRoom()
        {
            string roomName = "";

            if (!string.IsNullOrEmpty(roomName))
            {
                RoomOptions options = new RoomOptions
                {
                    MaxPlayers = 8,
                };

                PhotonNetwork.CreateRoom(roomName, options);
                this.CloseMenus();
                //UI open Loading
                Debug.Log("Creating Room...");
            }
        }

        private void CloseMenus()
        {

        }

        private void CloseErrorScreen()
        {
            this.CloseMenus();
            //UI open Main Menu
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            this.CloseMenus();
            //UI open Loading
            Debug.Log("Leaving Room");
        }

        public void JoinRoom(RoomInfo roomInfo)
        {
            if (PhotonNetwork.IsConnected)
            { PhotonNetwork.JoinRoom(roomInfo.Name); }
            else
            {
                Debug.LogWarning("Unable to join room because not connected to Photon network.");
                return;
            }
            this.CloseMenus();
            //UI open Loading
        }

        public void SetNickName()
        {
            string playerName = "";
            if (!string.IsNullOrEmpty(playerName))
            {
                PhotonNetwork.NickName = playerName;
                PlayerPrefs.SetString(PlayerPrefTags.PLAYER_NAME, playerName);
                this.CloseMenus();
                //UI open Main Menu
                this.hasSetNick = true;
            }
        }

        public void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int newLevel = Random.Range(0, allMap.Length);
                PhotonNetwork.LoadLevel(allMap[newLevel]);
            }
        }

        public void QuickJoin()
        {
            RoomOptions options = new RoomOptions
            {
                MaxPlayers = 8,
            };
            PhotonNetwork.CreateRoom("Demo", options);
            this.CloseMenus();
            //UI open Loading
        }

        //override
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;

            Debug.Log("Joining Lobby....");
        }

        public override void OnJoinedLobby()
        {
            this.CloseMenus();
            //UI open Main Menu Screen

            PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

            if (!hasSetNick)
            {
                if (PlayerPrefs.HasKey(PlayerPrefTags.PLAYER_NAME))
                    Debug.Log(PlayerPrefs.GetString(PlayerPrefTags.PLAYER_NAME));
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString(PlayerPrefTags.PLAYER_NAME);
            }
        }

        public override void OnJoinedRoom()
        {
            this.CloseMenus();
            //UI open Room Screen
            Debug.Log(PhotonNetwork.CurrentRoom.Name);

            //List Player

            if (PhotonNetwork.IsMasterClient)
                Debug.Log("Master");
            else
                Debug.Log("Not Master");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError($"Join Room Failed: {message} (Error: {returnCode})");
            this.CloseErrorScreen();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Failed To Create Room: {message}");
            this.CloseErrorScreen();
        }

        public override void OnLeftRoom()
        {
            this.CloseMenus();
            //UI open Main Menu
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {

        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            //List Player
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.IsMasterClient)
                Debug.Log("Master");
            else
                Debug.Log("Not Master");
        }

    }
}