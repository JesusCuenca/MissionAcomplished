using UnityEngine;

namespace MissionAcomplished.Multiplayer.Player
{
    public class BasePlayerController : MonoBehaviour
    {
        public string playerName = "Player name";
        public int playerAvatar = 0;
        public bool playerIsHost = false;

        public virtual void Initialize(string name, int avatar, bool host)
        {
            this.playerName = name;
            this.playerAvatar = avatar;
            this.playerIsHost = host;
        }

        public virtual void Initialize(PlayerCustomData data, bool host)
        {
            Initialize(data.name, data.avatar, host);
        }
    }
}
