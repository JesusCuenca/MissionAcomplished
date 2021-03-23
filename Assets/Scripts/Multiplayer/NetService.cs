using SWNetwork;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace MissionAcomplished.Multiplayer
{
    [Serializable]
    public class GameDataEvent : UnityEvent<EncryptedData> { }
    [Serializable]
    public class CardPlayedEvent : UnityEvent<CardPlayed> { }


    public class NetService : MonoBehaviour
    {
        public const string PROPERTY_NAME_GAME_DATA = "GameData";
        public const string GAME_STATE_CHANGED = "GameStateChanged";
        public const string GAME_CARD_PLAYED = "GameCardPlayed";

        // Events fired to avoid NetService and MultiplayerController referencing each other.
        public GameDataEvent OnGameDataReadyEvent = new GameDataEvent();
        public GameDataEvent OnGameDataChangedEvent = new GameDataEvent();
        public UnityEvent OnGameStateChangedEvent = new UnityEvent();
        public CardPlayedEvent OnCardPlayedEvent = new CardPlayedEvent();

        // SWNetwork agents.
        public RoomPropertyAgent roomPropertyAgent;
        public RoomRemoteEventAgent roomRemoteEventAgent;

        private void Awake()
        {
            roomPropertyAgent = FindObjectOfType<RoomPropertyAgent>();
            roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
        }

        // ----------------
        // Public accessors
        // ----------------
        public void EnableRoomProperty()
        {
            this.roomPropertyAgent.Initialize();
        }

        // --------------------
        // Room property events
        // --------------------
        public void OnGameDataReady()
        {
            EncryptedData data = this.roomPropertyAgent
                .GetPropertyWithName(PROPERTY_NAME_GAME_DATA).GetValue<EncryptedData>();
            this.OnGameDataReadyEvent.Invoke(data);
        }

        public void OnGameDataChanged()
        {
            EncryptedData data = this.roomPropertyAgent
                .GetPropertyWithName(PROPERTY_NAME_GAME_DATA).GetValue<EncryptedData>();
            this.OnGameDataChangedEvent.Invoke(data);
        }

        // --------------------
        // Room agent events
        // --------------------
        public void OnGameStateChanged()
        {
            this.OnGameStateChangedEvent.Invoke();
        }

        public void OnCardPlayed() { }
    }
}
