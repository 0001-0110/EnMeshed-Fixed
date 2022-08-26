using Photon.Pun;

public class GameMultiplayerController : DebugMonoBehaviour
{
    public static GameMultiplayerController Instance { get; private set; }
    public bool IsConnectedToRoom => PhotonNetwork.InRoom;

    public override void Awake()
    {
        base.Awake();
        defaultDebugTag = DebugTag.Multiplayer;

        if (Instance != null)
        {
            LogWarning($"There is multiple {this}s, but it should be only one");
            LogWarning($"The previous {Instance} has been replaced with the new one");
        }
        Instance = this;
    }
}
