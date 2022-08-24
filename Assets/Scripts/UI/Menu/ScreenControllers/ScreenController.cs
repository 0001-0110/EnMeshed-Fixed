using UnityEngine;

public abstract class ScreenController : DebugMonoBehaviour
{
    /// <summary>
    /// Use this to select the screen that is going to be visible when going back to the menu
    /// </summary>
    /// <remarks>
    /// <para> If null, ScreenControllers will ignore it </para>
    /// <para> Changing this value while in the menu scene will do nothing, use OpenScreen </para>
    /// </remarks>
    public static GameObject ActiveScreen;

    public override void Awake()
    {
        base.Awake();
        defaultDebugTag = DebugTag.UI;
    }

    public virtual void Start()
    {
        // If ActiveScreen is null, keep the same active state for all screenControllers
        if (ActiveScreen != null)
            gameObject.SetActive(gameObject == ActiveScreen);
    }

    public void OpenScreen(GameObject screen)
    {
        ActiveScreen = screen;
        screen.SetActive(true);
        gameObject.SetActive(false);
        LogMessage($"ActiveScreen is {ActiveScreen}");
    }

    public void Quit()
    {
        // TODO safely quit multiplayer ?
#if UNITY_EDITOR
        // Application.Quit works only when in a build
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
