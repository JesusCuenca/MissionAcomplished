using System;
using System.Collections;
using System.Collections.Generic;
using SWNetwork;

namespace MissionAcomplished.Piles
{
    public class PilesManager : IReadOnlyCollection<Queue<byte>>, IReadOnlyList<Queue<byte>>
    {
        public List<Queue<byte>> piles;

        public int Count => this.piles.Count;

        public Queue<byte> this[int index] => this.piles[index];

        public PilesManager()
        {
            this.piles = new List<Queue<byte>>{
                new Queue<byte>(),
                new Queue<byte>(),
                new Queue<byte>(),
                new Queue<byte>(),
            };
        }

        public byte[] GetPile(int index)
        {
            CheckIndexOutOfRange(index);

            return this.piles[index].ToArray();
        }

        public int DropIntoPile(int index, int cardValue)
        {
            CheckIndexOutOfRange(index);

            this.piles[index].Enqueue((byte)cardValue);
            return cardValue;
        }

        private void CheckIndexOutOfRange(int index)
        {
            if (index >= this.piles.Count)
            {
                throw new IndexOutOfRangeException($"There are only 4 piles (0 to 3), but {index} was given.");
            }

        }

        public byte[] ToByteArray()
        {
            SWNetworkMessage message = new SWNetworkMessage();
            foreach (var pile in this)
            {
                message.Push((byte)pile.Count);
                message.PushByteArray(pile.ToArray());
            }
            return message.ToArray();
        }

        public void FromByteArray(byte[] data)
        {
            SWNetworkMessage message = new SWNetworkMessage(data);
            for (int i = 0; i < 4; i++)
            {
                byte pileCardCount = message.PopByte();
                var cards = message.PopByteArray(pileCardCount);
                this.piles[i] = new Queue<byte>(cards);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<Queue<byte>> GetEnumerator()
        {
            return this.piles.GetEnumerator();
        }
    }
}
