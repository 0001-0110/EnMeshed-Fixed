using UnityEngine;

public abstract class ScreenController : DebugMonoBehaviour
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <remarks>
    /// <para> If null, ScreenControllers will ignore it </para>
    /// <para> Changing this value while in the menu scene will do nothing, use OpenScreen </para>
    /// </remarks>
    public static GameObject ActiveScreen;

    public override void Awake()
    {
        base.Awake();
        debugTags.Add(DebugTag.UI);
    }

    public override void Start()
    {
        base.Start();

        // If ActiveScreen is null, keep the same active state for all screenControllers
        if (ActiveScreen != null)
            gameObject.SetActive(gameObject == ActiveScreen);
    }

    public void OpenScreen(GameObject screen)
    {
        ActiveScreen = screen;
        screen.SetActive(true);
        gameObject.SetActive(false);
        LogMessage($"DEBUG - UI | ActiveScreen is {ActiveScreen}");
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
