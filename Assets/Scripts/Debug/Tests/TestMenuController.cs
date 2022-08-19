using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenuController : DebugMonoBehaviour
{
    private MenuMultiplayerController menuMultiplayerController;

    // TODO not the best solution
    public string GameSceneName;

    // TODO implement offline mode for testing
    [Tooltip("Choose wether to laucnh the game as online or offline")]
    public bool Online;

    [Tooltip("Use this to force the user name, leave empty for generated user name")]
    public string UserName;

    public override async void Awake()
    {
        base.Awake();

        menuMultiplayerController = MenuMultiplayerController.Instance;

        if (UserName == string.Empty)
            menuMultiplayerController.LocalPlayer.NickName = $"TEST_{DateTime.Now.Minute}_{DateTime.Now.Second}";
        else
            menuMultiplayerController.LocalPlayer.NickName = UserName;

        await LaunchGame(Online);
    }

    private async Task LaunchGame(bool online)
    {
        if (online)
        {
            if (await menuMultiplayerController.ConnectToMaster())
            {
                if (await menuMultiplayerController.JoinRandomOrCreateRoom())
                {
                    LogMessage("Succesfully created or joined an online room", DebugTag.Multiplayer);
                    SceneManager.LoadScene(GameSceneName);
                }
                else
                    LogError("Could not connect to room", DebugTag.Multiplayer);
            }
            else
                LogError("Could not connect to master", DebugTag.Multiplayer);
        }
        else
        {
            // Offline
            if (await menuMultiplayerController.StartOfflineMode())
                if (await menuMultiplayerController.CreateRoom("SoloRoom"))
                {
                    LogMessage("Succesfully created an offline room", DebugTag.Multiplayer);
                    SceneManager.LoadScene(GameSceneName);
                }
                else
                    LogError("Could not create room", DebugTag.Multiplayer);
            else
                LogError("Could not start offline mode", DebugTag.Multiplayer);
        }
    }
}
