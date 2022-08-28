using Photon.Pun;
using Photon.Realtime;

public class GameMultiplayerController : DebugMonoBehaviour
{
    public static GameMultiplayerController Instance { get; private set; }
    public bool IsConnectedToRoom => PhotonNetwork.InRoom;
    public Room CurrentRoom => PhotonNetwork.CurrentRoom;
    public Player LocalPlayer => PhotonNetwork.LocalPlayer;

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

    public void ToggleVisibility()
    {
        CurrentRoom.IsVisible = !CurrentRoom.IsVisible;
    }

    public void StartGame()
    {
        CurrentRoom.IsOpen = false;
        CurrentRoom.IsVisible = false;
    }
}
