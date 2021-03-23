using System;
using System.Collections.Generic;
using MissionAcomplished.Cards;

namespace MissionAcomplished
{
    public class DeckManager
    {
        protected Queue<byte> deck;

        public int Count { get => this.deck.Count; }
        public bool Empty { get => this.deck.Count == 0; }

        public DeckManager(byte[] deck)
        {
            this.deck = new Queue<byte>(deck);
        }

        public DeckManager() : this(DeckManager.Shuffle(DeckManager.GenerateDeckStatic())) { }

        public int Draw()
        {
            return this.deck.Dequeue();
        }

        public byte[] ToByteArray()
        {
            return this.deck.ToArray();
        }

        public void FromByteArray(byte[] cards) {
            this.deck = new Queue<byte>(cards);
        }

        public static byte[] GenerateDeckStatic()
        {
            int cardCount = Constants.RANK_COUNT * Constants.COLOR_COUNT * Constants.CARD_OF_EACH_COLOR_AND_RANK_COUNT;
            var deck = new byte[cardCount];

            int index = 0;
            for (int rank = 0; rank < Constants.RANK_COUNT; rank++)
            {
                for (int color = 0; color < Constants.COLOR_COUNT; color++)
                {
                    for (int rep = 0; rep < Constants.CARD_OF_EACH_COLOR_AND_RANK_COUNT; rep++)
                    {
                        deck[index++] = (byte)CardBase.GetValueFromRankAndColor(rank, color);
                    }
                }
            }

            return deck;
        }

        public static byte[] Shuffle(byte[] deck)
        {
            Random rnd = new Random();
            int n = deck.Length;
            while (n > 1)
            {
                int k = rnd.Next(n);
                n--;
                byte temp = deck[k];
                deck[k] = deck[n];
                deck[n] = temp;
            }

            return deck;
        }
    }
}
