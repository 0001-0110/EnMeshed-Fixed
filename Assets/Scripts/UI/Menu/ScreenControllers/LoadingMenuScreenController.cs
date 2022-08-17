using UnityEngine;

public class LoadingMenuScreenController : ScreenController
{
    private MenuMultiplayerController menuMultiplayerController;

    public GameObject UserNameScreen;

    public override void Awake()
    {
        base.Awake();

        menuMultiplayerController = MenuMultiplayerController.Instance;
    }

    public async void OnEnable()
    {
        // If not connected to the master server, attempt the connection
        if (!menuMultiplayerController.IsConnectedAndReady)
            await menuMultiplayerController.ConnectToMaster();
        // Change screen, don't care if the connection was a success or not
        // since the main menu is the one handling the case where we are not connected
        OpenScreen(UserNameScreen);
    }
}
