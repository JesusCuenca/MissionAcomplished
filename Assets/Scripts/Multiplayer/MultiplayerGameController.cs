using UnityEngine;
using SWNetwork;
using System.Collections.Generic;
using MissionAcomplished.Multiplayer.Player;

namespace MissionAcomplished.Multiplayer
{
    public class MultiplayerGameController : MonoBehaviour
    {
        public DataManager dataManager;
        public NetService netService;

        private void Awake()
        {
            this.netService = FindObjectOfType<NetService>();
            NetworkClient.Lobby.GetPlayersInRoom((success, reply, error) =>
            {
                if (!success)
                {
                    Debug.Log($"Error retrieving the room players: {error}");
                    return;
                }

                var players = new List<MissionAcomplished.Player>();
                foreach (var swPlayer in reply.players)
                {
                    PlayerCustomData data = swPlayer.GetCustomData<PlayerCustomData>();
                    var player = new MissionAcomplished.Player(
                        swPlayer.id,
                        data.name,
                        data.avatar,
                        swPlayer.id.Equals(reply.ownerId),
                        null
                    );
                    players.Add(player);
                }

                this.dataManager = new DataManager(players);
                this.netService.EnableRoomProperty();
            });
        }

        // --------------------
        // Room property events
        // --------------------
        public void OnGameDataReady(EncryptedData data)
        {

        }

        public void OnGameDataChanged(EncryptedData data)
        {

        }

        // --------------------
        // Room agent events
        // --------------------
        public void OnGameStateChanged()
        {

        }
        public void OnCardPlayed() { }

    }
}
