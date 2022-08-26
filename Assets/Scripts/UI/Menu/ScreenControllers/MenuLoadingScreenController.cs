using UnityEngine;

public class MenuLoadingScreenController : ScreenController
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
        // No particular reason for this check, but we never know
        if (!menuMultiplayerController.IsConnectedToMaster)
            await menuMultiplayerController.ConnectToMaster();
        // Change screen, don't care if the connection was a success or not
        // since the main menu is the one handling the case where we are not connected
        OpenScreen(UserNameScreen);
    }
}
