using UnityEngine;

public class UIController : DebugMonoBehaviour
{
    public static UIController Instance { get; private set; }

    private MultiplayerController multiplayerController;

    private GameObject EscapeMenuObject;

    public override void Awake()
    {
        base.Awake();
        defaultDebugTag = DebugTag.UI;

        if (Instance != null)
        {
            LogWarning($"There is multiple {this}s, but it should be only one");
            LogWarning($"The previous {Instance} has been replaced with the new one");
        }
        Instance = this;

        multiplayerController = MultiplayerController.Instance;
    }

    public async void LeaveRoom()
    {
        await multiplayerController.LeaveRoom();
    }

    public void QuitGame()
    {
        // Really useful ?
        LeaveRoom();

        // Application.Quit works only when in a build
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
