using MissionAcomplished.Missions;
using MissionAcomplished.Piles;
using SWNetwork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MissionAcomplished
{
    [Serializable]
    public class EncryptedData
    {
        public byte[] data;
    }

    [Serializable]
    public class CardPlayed
    {
        public byte cardValue;
        public byte pileIndex;
    }

    public class DataManager
    {
        // Players and their data.
        public Dictionary<string, Player> players;
        // Deck of remaining cards
        public DeckManager cardsDeck;
        // Piles on board
        public PilesManager piles;
        // Deck of missions
        public MissionManager missions;

        public DataManager(List<Player> players)
        {
            this.players = new Dictionary<string, Player>();
            this.cardsDeck = new DeckManager();
            this.piles = new PilesManager();
            this.missions = new MissionManager();
            foreach (var player in players)
            {
                this.players.Add(player.Id, player);
            }
        }

        private byte[] ToByteArray()
        {
            SWNetworkMessage message = new SWNetworkMessage();
            message.Push((byte)this.cardsDeck.Count);
            message.PushByteArray(this.cardsDeck.ToByteArray());

            byte[] pilesAsByteArray = this.piles.ToByteArray();
            message.Push(pilesAsByteArray.Length);
            message.PushByteArray(pilesAsByteArray);
            message.PushUTF8LongString(UnityEngine.JsonUtility.ToJson(this.missions));

            return message.ToArray();
        }

        public void FromByteArray(byte[] data)
        {
            SWNetworkMessage message = new SWNetworkMessage(data);
            byte cardCount = message.PopByte();
            var cards = message.PopByteArray(cardCount);
            this.cardsDeck.FromByteArray(cards);

            byte pilesCount = message.PopByte();
            byte[] pilesAsByteArray = message.PopByteArray(pilesCount);
            this.piles.FromByteArray(pilesAsByteArray);

            string missionsAsString = message.PopUTF8LongString();
            this.missions = UnityEngine.JsonUtility.FromJson<MissionManager>(missionsAsString);
        }
    }
}
