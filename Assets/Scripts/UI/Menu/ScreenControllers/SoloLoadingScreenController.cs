public class SoloLoadingScreenController : GameLoadingScreenController
{
    private const string SoloRoomName = "SoloRoom";

    private MenuMultiplayerController menuMultiplayerController;

    public override void Awake()
    {
        base.Awake();

        menuMultiplayerController = MenuMultiplayerController.Instance;
    }

    public async void OnEnable()
    {
        // could probably do better
        if (await menuMultiplayerController.StartOfflineMode())
            await LoadRoom(menuMultiplayerController.CreateRoom(SoloRoomName));
        else
            SetMode(ScreenMode.FailedConnection);
    }
}
