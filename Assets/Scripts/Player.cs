using System;
using System.Collections.Generic;

namespace MissionAcomplished
{
    [Serializable]
    public class Player : IEquatable<Player>
    {
        public string Id;
        public string Name;
        public int Avatar;
        public bool Host;
        public List<int> Cards = new List<int>();

        public Player(string id, string name, int avatar, bool host, List<int> cards)
        {
            this.Id = id;
            this.Name = name;
            this.Avatar = avatar;
            this.Host = host;
            this.Cards = cards ?? new List<int>();
        }
        public Player() : this("", "", 0, false, null) { }

        public bool Equals(Player other)
        {
            return this.Id.Equals(other.Id);
        }
    }
}
