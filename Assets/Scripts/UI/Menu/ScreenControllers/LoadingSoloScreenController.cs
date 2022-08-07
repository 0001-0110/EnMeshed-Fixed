using UnityEngine.SceneManagement;
using Photon.Realtime;

public class LoadingSoloScreenController : ModularScreenController
{
    private const string GameSceneName = "GameScene";
    private const string SoloRoomName = "SoloRoom";

    private MultiplayerController multiplayerController;

    public override void Awake()
    {
        base.Awake();
        debugTags.Add(DebugTag.Multiplayer);

        multiplayerController = MultiplayerController.Instance;
    }

    public async void OnEnable()
    {
        SetMode(ScreenMode.Loading);
        // TODO comment this
        if (await multiplayerController.StartOfflineMode() && await multiplayerController.CreateRoom(SoloRoomName))
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }
}
