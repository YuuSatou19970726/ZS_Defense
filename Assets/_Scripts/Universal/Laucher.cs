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

        [SerializeField]
        private MainMenuUI mainMenuUI;

        List<RoomInfo> roomInfos;

        private bool hasSetNick = false;

        private string[] allMap = { SceneTags.AREA_0 };

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            this.mainMenuUI.SetLoadingText("Connecting to Network...");
            this.mainMenuUI.OpenLoadingScreen();

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void SetNickName()
        {
            string playerName = this.mainMenuUI.GetNameInput();
            if (!string.IsNullOrEmpty(playerName))
            {
                PhotonNetwork.NickName = playerName;
                PlayerPrefs.SetString(PlayerPrefTags.PLAYER_NAME, playerName);
                this.mainMenuUI.OpenMenu();
                this.hasSetNick = true;
            }
        }

        public void CreateRoom()
        {
            string roomName = Random.Range(0, 1000).ToString();

            if (!string.IsNullOrEmpty(roomName))
            {
                RoomOptions options = new RoomOptions
                {
                    MaxPlayers = 8,
                };

                PhotonNetwork.CreateRoom(roomName, options);

                this.mainMenuUI.SetLoadingText("Creating Room...");
                this.mainMenuUI.OpenLoadingScreen();
            }
        }

        public void QuickJoin()
        {
            Debug.Log(this.roomInfos.Count);
            for (int i = 0; i < this.roomInfos.Count; i++)
            {
                Debug.Log(this.roomInfos[i].Name);
            }

            if (this.roomInfos.Count > 0)
            {
                this.JoinRoom(this.roomInfos[Random.Range(0, this.roomInfos.Count)].Name);
            }
            else
            {
                RoomOptions options = new RoomOptions
                {
                    MaxPlayers = 8,
                };
                PhotonNetwork.CreateRoom(Random.Range(0, 1000).ToString(), options);
            }


            this.mainMenuUI.SetLoadingText("Join Room...");
            this.mainMenuUI.OpenLoadingScreen();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int newLevel = Random.Range(0, allMap.Length);
                PhotonNetwork.LoadLevel(allMap[newLevel]);
            }
        }

        private void JoinRoom(string roomName)
        {
            if (PhotonNetwork.IsConnected)
            { PhotonNetwork.JoinRoom(roomName); }
            else
            {
                Debug.LogWarning("Unable to join room because not connected to Photon network.");
                return;
            }
            this.mainMenuUI.SetLoadingText("Join Room...");
            this.mainMenuUI.OpenLoadingScreen();
        }

        private void CloseErrorScreen()
        {
            this.mainMenuUI.OpenMenu();
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            this.mainMenuUI.SetLoadingText("Leaving Room...");
            this.mainMenuUI.OpenLoadingScreen();
        }

        //Pun Callback
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;

            Debug.Log("Joining Lobby....");
        }

        public override void OnJoinedLobby()
        {
            this.mainMenuUI.OpenMenu();

            PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

            if (!hasSetNick)
            {
                this.mainMenuUI.OpenNameInputScreenScreen();
                if (PlayerPrefs.HasKey(PlayerPrefTags.PLAYER_NAME))
                    this.mainMenuUI.SetNameInput(PlayerPrefs.GetString(PlayerPrefTags.PLAYER_NAME));
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString(PlayerPrefTags.PLAYER_NAME);
            }
        }

        public override void OnJoinedRoom()
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name);
            this.StartGame();

            //List Player

            // if (PhotonNetwork.IsMasterClient)
            //     Debug.Log("Master");
            // else
            //     Debug.Log("Not Master");
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
            this.mainMenuUI.OpenMenu();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            this.roomInfos = roomList;
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
            // if (PhotonNetwork.IsMasterClient)
            //     Debug.Log("Master");
            // else
            //     Debug.Log("Not Master");
        }

    }
}