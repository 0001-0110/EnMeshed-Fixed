using UnityEngine;

public class LoadingMenuScreenController : ScreenController
{
    private MultiplayerController multiplayerController;

    public GameObject UserNameScreen;

    public override void Awake()
    {
        base.Awake();

        multiplayerController = MultiplayerController.Instance;
    }

    public async void OnEnable()
    {
        // If not connected to the master server, attempt the connection
        if (!multiplayerController.IsConnectedAndReady)
            await multiplayerController.ConnectToMaster();
        // Change screen, don't care if the connection was a success or not
        // since the main menu is the one handling the case where we are not connected
        OpenScreen(UserNameScreen);
    }
}
