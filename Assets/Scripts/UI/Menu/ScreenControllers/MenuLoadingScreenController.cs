using UnityEngine;

public class MenuLoadingScreenController : ScreenController
{
	private MultiplayerController MultiplayerController;

	[SerializeField]
	private GameObject userNameScreen;

	public override void Awake()
	{
		base.Awake();

		MultiplayerController = MultiplayerController.Instance;
	}

	public async void OnEnable()
	{
		// If not connected to the master server, attempt the connection
		// No particular reason for this check, but we never know
		if (!MultiplayerController.IsConnectedToMaster)
			await MultiplayerController.ConnectToMaster();
		// Change screen, don't care if the connection was a success or not
		// since the main menu is the one handling the case where we are not connected
		// But we need to make sure the app is still running before switching screens
		if (this != null)
			OpenScreen(userNameScreen);
	}
}
