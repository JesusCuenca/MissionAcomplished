using System;
namespace MissionAcomplished.Multiplayer.Player
{
    [Serializable]
    public struct PlayerCustomData
    {
        public string name;

        public int avatar;

        public PlayerCustomData(string name, int avatar)
        {
            this.name = name;
            this.avatar = avatar;
        }
    }
}
