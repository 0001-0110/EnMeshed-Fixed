public class SoloLoadingScreenController : GameLoadingScreenController
{
	private const string SoloRoomName = "SoloRoom";

	private MultiplayerController MultiplayerController;

	public override void Awake()
	{
		base.Awake();

		MultiplayerController = MultiplayerController.Instance;
	}

	public async void OnEnable()
	{
		// could probably do better
		if (await MultiplayerController.StartOfflineMode())
			await LoadRoom(MultiplayerController.CreateRoom(SoloRoomName));
		else
			SetMode(ScreenMode.FailedConnection);
	}
}
