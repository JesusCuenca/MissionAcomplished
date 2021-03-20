using System.Collections;
using SWNetwork;
using UnityEngine;
using UnityEngine.UI;
using MissionAcomplished.Multiplayer.Player;

namespace MissionAcomplished.Multiplayer
{
    public class LobbyController : MonoBehaviour
    {
        public GameObject PlayerConfigPanel;
        public Button PlayerConfigButton;

        public GameObject GameConfigPanel;
        public Button GameConfigButton;

        public GameObject LobbyPanel;
        public GameObject LobbyPlayerList;
        public Button LobbyButton;

        public GameObject PlayerListPrefab;

        public string userName;
        public int? userAvatar = null;
        public string gameName;

        public int playerCount = 0;

        private void Start()
        {
            NetworkClient.Lobby.OnLobbyConnectedEvent += Lobby_OnLobbyConncetedEvent;
            NetworkClient.Lobby.OnRoomReadyEvent += Lobby_OnRoomReadyEvent;
            NetworkClient.Lobby.OnNewPlayerJoinRoomEvent += Lobby_OnNewPlayerJoinRoomEvent;
            NetworkClient.Lobby.OnPlayerLeaveRoomEvent += Lobby_OnPlayerLeaveRoomEvent;
        }

        private void OnDestroy()
        {
            NetworkClient.Lobby.OnLobbyConnectedEvent -= Lobby_OnLobbyConncetedEvent;
            NetworkClient.Lobby.OnRoomReadyEvent -= Lobby_OnRoomReadyEvent;
            NetworkClient.Lobby.OnNewPlayerJoinRoomEvent -= Lobby_OnNewPlayerJoinRoomEvent;
            NetworkClient.Lobby.OnPlayerLeaveRoomEvent -= Lobby_OnPlayerLeaveRoomEvent;
        }

        // ------------
        // GUI handlers
        // ------------
        // Player config panel
        public void GUI_UpdateUserName(string userName)
        {
            this.userName = userName;
            this.GUI_UpdatePlayerConfigContinueButton();
        }

        public void GUI_UpdateUserAvatar(int avatarIndex)
        {
            this.userAvatar = avatarIndex;
            this.GUI_UpdatePlayerConfigContinueButton();
        }

        public void GUI_AcceptUserConfig()
        {
            this.CheckPlayerIn();
        }

        private void GUI_UpdatePlayerConfigContinueButton()
        {
            this.PlayerConfigButton.interactable =
                !string.IsNullOrEmpty(this.userName) && this.userAvatar != null;
        }

        // Game config panel
        public void GUI_UpdateGameName(string gameName)
        {
            this.gameName = gameName;
            this.GameConfigButton.interactable =
                !string.IsNullOrEmpty(this.gameName) && this.gameName.Length >= 4;
        }

        public void GUI_FindAndJoinRoom()
        {
            this.FindAndJoinRoom();
        }

        public void GUI_CreateNewRoom()
        {
            this.CreateRoom();
        }

        // Lobby config panel
        public void GUI_StartGame()
        {
            StartCoroutine(LoadMultiplayerScene());
        }

        private IEnumerator LoadMultiplayerScene()
        {
            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Multiplayer");
            while (!async.isDone)
            {
                yield return null;
            }
        }

        private void GUI_UpdateStartGameButton()
        {
            this.LobbyButton.interactable = this.playerCount > 1;
        }

        private void GUI_EnableGameConfigPanel()
        {
            this.PlayerConfigPanel.SetActive(false);
            this.GameConfigPanel.SetActive(true);
        }

        private void GUI_EnableLobbyPanel()
        {
            this.GameConfigPanel.SetActive(false);
            this.LobbyPanel.SetActive(true);
            this.FetchPlayers();
        }

        // -------------------
        // Lobby functionality
        // -------------------
        private void CheckPlayerIn()
        {
            NetworkClient.Instance.CheckIn(this.userName, (bool success, string error) =>
                {
                    if (!success)
                    {
                        Debug.Log("Check-in failed: " + error);
                    }
                });
        }

        private void RegisterPlayer()
        {
            this.PlayerConfigButton.interactable = false;
            var userData = new PlayerCustomData(this.userName, this.userAvatar.Value);
            NetworkClient.Lobby.Register(userData, (success, reply, error) =>
                {
                    if (!success)
                    {
                        Debug.Log($"Failed to register: {error}");
                        this.PlayerConfigButton.interactable = true;
                        return;
                    }

                    Debug.Log($"Registered into the lobby: {reply.roomId}");
                    if (string.IsNullOrEmpty(reply.roomId))
                    {
                        // Si el jugador no está conectado a una room, mostrar el panel
                        // para que pueda crear una nueva o unirse a una existente
                        Debug.Log("El jugador no está conectado a una room");
                        GUI_EnableGameConfigPanel();
                    }
                    else if (reply.started)
                    {
                        // Si el jugador está conectado y la partida ha empezado,
                        // moverlo a la pantalla de la partida.
                        Debug.Log("Game already started. should join inmediatly");
                        return;
                    }
                    else
                    {
                        // Si el jugador está en room pero todavía no ha empezado,
                        // mostrar el panel del listado de jugadores.
                        Debug.Log("El jugador está conectado a una room que todavía no ha empezado");
                        GUI_EnableLobbyPanel();
                    }
                });
        }

        private void CreateRoom()
        {
            NetworkClient.Lobby.CreateRoom("", false, 4, 0, true, GameIndex.MakeRandomIndexData(),
                (bool success, string roomId, SWLobbyError error) =>
                {
                    if (!success)
                    {
                        Debug.Log($"Create room failed: {error}");
                        return;
                    }

                    Debug.Log($"Connected to room {roomId}");
                    GUI_EnableLobbyPanel();
                });
        }

        private void FindAndJoinRoom()
        {
            if (string.IsNullOrEmpty(this.gameName)) return;
            this.GameConfigButton.interactable = false;
            NetworkClient.Lobby.FilterRoom(GameIndex.MakeFilterData(this.gameName),
                1,
                (success, reply, error) =>
                {
                    if (!success)
                    {
                        Debug.Log($"Error filtering rooms :{error}");
                        this.GameConfigButton.interactable = true;
                        return;
                    }
                    if (reply.rooms.Count == 0)
                    {
                        Debug.Log("No rooms found with that name");
                        this.GameConfigButton.interactable = true;
                        return;
                    }

                    SWRoom room = reply.rooms[0];
                    JoinRoom(room.id);
                });
        }

        private void JoinRoom(string roomId)
        {
            NetworkClient.Lobby.JoinRoom(roomId, (successful, reply, error) =>
                {
                    if (successful)
                    {
                        Debug.Log($"Joined room {reply}");
                        GUI_EnableLobbyPanel();
                    }
                    else
                    {
                        Debug.Log($"Failed to Join room {error}");
                    }
                });
        }

        private void FetchPlayers()
        {
            NetworkClient.Lobby.GetPlayersInRoom((successful, reply, error) =>
                {
                    Debug.Log($"Got players {reply}");
                    if (successful)
                    {
                        foreach (Transform child in this.LobbyPlayerList.transform)
                        {
                            Destroy(child.gameObject);
                        }

                        foreach (SWPlayer player in reply.players)
                        {
                            PlayerCustomData data = player.GetCustomData<PlayerCustomData>();
                            Debug.Log("Player custom data: " + data);

                            GameObject playerGO = Instantiate(this.PlayerListPrefab, this.LobbyPlayerList.transform);
                            PlayerLobbyListController playerCtrl = playerGO.GetComponent<PlayerLobbyListController>();
                            playerCtrl.Initialize(data, player.id.Equals(reply.ownerId));
                        }
                        this.playerCount = reply.players.Count;
                        this.GUI_UpdateStartGameButton();
                    }
                    else
                    {
                        Debug.Log($"Failed to get players {error}");
                    }
                });
        }

        // --------------------
        // Lobby event handlers
        // --------------------
        private void Lobby_OnLobbyConncetedEvent()
        {
            Debug.Log("Lobby connected");
            RegisterPlayer();
        }

        private void Lobby_OnRoomReadyEvent(SWRoomReadyEventData eventData)
        {
            Debug.Log($"Room is ready: roomId= {eventData.roomId}");
        }

        private void Lobby_OnFailedToStartRoomEvent(
            SWFailedToStartRoomEventData eventData
        )
        {
            Debug.Log($"Failed to start room: {eventData}");
        }

        private void Lobby_OnNewPlayerJoinRoomEvent(SWJoinRoomEventData eventData)
        {
            Debug.Log($"New player joined room: {eventData}");
            this.FetchPlayers();
        }

        private void Lobby_OnPlayerLeaveRoomEvent(SWLeaveRoomEventData eventData)
        {
            Debug.Log($"Palyer leave room {eventData}");
            this.FetchPlayers();
        }
    }
}
