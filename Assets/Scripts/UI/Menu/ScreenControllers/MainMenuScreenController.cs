using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreenController : ScreenController
{
    private MenuMultiplayerController menuMultiplayerController;

    public Button MultiplayerButton;

    public override void Awake()
    {
        base.Awake();
        menuMultiplayerController = MenuMultiplayerController.Instance;
    }

    public void OnEnable()
    {
        if (menuMultiplayerController.OfflineMode)
            menuMultiplayerController.StopOfflineMode();
        // This button only works if the connection to the master server has been established
        // DONE possible problems may arise if the connection is lost while in the main menu, additional checks are required
        if (!menuMultiplayerController.IsConnectedAndReady)
        {
            MultiplayerButton.interactable = false;
            RetryConnection();
        }
    }

    private async void RetryConnection(int refreshDelay = 2000)
    {
        // Keep trying to connect until this gameObject is no longer active or the connection is succesful
        // We have to check if this is null to avoid errors when the screen is destroyed (Loading a new scene or exiting the game)
        while (this != null && gameObject.activeInHierarchy && !menuMultiplayerController.IsConnectedAndReady)
        {
            LogMessage("DEBUG - 22 | Retrying connection to master server", DebugTag.Multiplayer);
            MultiplayerButton.interactable = await menuMultiplayerController.ConnectToMaster();
            await Task.Delay(refreshDelay);
        }
    }

    public void OpenLobby(GameObject lobbyScreen)
    {
        if (!menuMultiplayerController.IsConnectedAndReady)
        {
            MultiplayerButton.interactable = false;
            RetryConnection();
        }
        else
            OpenScreen(lobbyScreen);
    }
}
