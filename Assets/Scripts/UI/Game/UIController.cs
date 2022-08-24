public class UIController : DebugMonoBehaviour
{
    public static UIController Instance { get; private set; }

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
    }
}
