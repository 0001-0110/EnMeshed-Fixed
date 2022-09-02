using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenuController : DebugMonoBehaviour
{
    private MultiplayerController MultiplayerController;

    // TODO implement offline mode for testing
    [Tooltip("Choose wether to laucnh the game as online or offline")]
    public bool Online;

    [Tooltip("Use this to force the user name, leave empty for generated user name")]
    public string UserName;

    public override async void Awake()
    {
        base.Awake();
        defaultDebugTag = DebugTag.Multiplayer;

        MultiplayerController = MultiplayerController.Instance;

        if (UserName == string.Empty)
            MultiplayerController.LocalPlayer.NickName = $"TEST_{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
        else
            MultiplayerController.LocalPlayer.NickName = UserName;

        await LaunchGame(Online);
    }

    private async Task LaunchGame(bool online)
    {
        if (online)
        {
            if (await MultiplayerController.ConnectToMaster())
                if (await MultiplayerController.JoinRandomOrCreateRoom())
                    LogMessage("Succesfully created or joined an online room");
                else
                    LogError("Could not connect to room");
            else
                LogError("Could not connect to master");
        }
        else
        {
            // Offline
            if (await MultiplayerController.StartOfflineMode())
                if (await MultiplayerController.CreateRoom("SoloRoom"))
                    LogMessage("Succesfully created an offline room");
                else
                    LogError("Could not create room");
            else
                LogError("Could not start offline mode");
        }
    }
}
