using UnityEngine.UI;

public class PlayerLobbyListController : BasePlayerController
{
    public Image AvatarComp;
    public Image IsHostComp;
    public Text NameComp;

    public override void Initialize(string name, int avatar, bool host)
    {
        base.Initialize(name, avatar, host);
        this.NameComp.text = name;
        this.IsHostComp.enabled = host;
    }
}
