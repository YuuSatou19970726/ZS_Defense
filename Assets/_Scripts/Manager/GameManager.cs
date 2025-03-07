using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZSDefense
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        private static GameManager instance;
        public static GameManager Instance => instance;

        [SerializeField]
        private GameObject playerPrefab;

        private void Start()
        {
            if (instance == null)
                instance = this;

            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene(SceneTags.MAIN_MENU);
                return;
            }

            if (playerPrefab == null) return;

            if (PhotonNetwork.InRoom && PlayerManager.LocalPlayerInstance == null)
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }

        public override void OnJoinedRoom()
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                LoadArena();
            }
        }
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(SceneTags.MAIN_MENU);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        void LoadArena()
        {
            // if (!PhotonNetwork.IsMasterClient) return;

            PhotonNetwork.LoadLevel(SceneTags.AREA_0);
        }
    }
}