using UnityEngine.SceneManagement;
using Photon.Realtime;

public class LoadingSoloScreenController : ModularScreenController
{
    private const string GameSceneName = "GameScene";
    private const string SoloRoomName = "SoloRoom";

    private MenuMultiplayerController menuMultiplayerController;

    public override void Awake()
    {
        base.Awake();
        debugTags.Add(DebugTag.Multiplayer);

        menuMultiplayerController = MenuMultiplayerController.Instance;
    }

    public async void OnEnable()
    {
        SetMode(ScreenMode.Loading);
        // TODO comment this
        if (await menuMultiplayerController.StartOfflineMode() && await menuMultiplayerController.CreateRoom(SoloRoomName))
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }
}
