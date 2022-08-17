using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public static bool Initialised { get; private set; }
    public static DebugController Instance { get; private set; }

    // These variables are only used in the editor
    [SerializeField]
    private bool debugDebug;
    [SerializeField]
    private bool debugUI;
    [SerializeField]
    private bool debugLanguage;
    [SerializeField]
    private bool debugMultiplayer;
    [SerializeField]
    private bool debugGameplay;
    [SerializeField]
    private bool debugLevel;
    [SerializeField]
    private bool debugInventory;
    [SerializeField]
    private bool debugEntities;
    [SerializeField]
    private bool debugAI;

    private Dictionary<DebugTag, bool> activeDebugTags = new Dictionary<DebugTag, bool>();

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"ERROR - 22 | There is multiple DebugController, but it should be only one");
            Debug.LogWarning($"WARNING - 22 | The previous DebugController has been replaced with a new one");
        }
        Instance = this;

        // This gameObject is not going to be destroyed when loading another scene
        DontDestroyOnLoad(gameObject);
        // Only log debugs if this build is a debug build
        Debug.unityLogger.logEnabled = Debug.isDebugBuild;

        activeDebugTags = new Dictionary<DebugTag, bool>()
        {
            [DebugTag.Debug] = debugDebug,
            [DebugTag.UI] = debugUI,
            [DebugTag.Language] = debugLanguage,
            [DebugTag.Multiplayer] = debugMultiplayer,
            [DebugTag.Gameplay] = debugGameplay,
            [DebugTag.Level] = debugLevel,
            [DebugTag.Inventory] = debugInventory,
            [DebugTag.Entities] = debugEntities,
            [DebugTag.AI] = debugAI,
        };
        if (activeDebugTags.Count != Enum.GetNames(typeof(DebugTag)).Length)
            Debug.LogWarning($"activeDebugTags is missing some DebugTags");

        Initialised = true;
    }

    private bool IsDebugTagActive(List<DebugTag> debugTags)
    {
        if (activeDebugTags == null)
            Debug.LogError($"{gameObject} is trying to log debug, but activeDebugTags has not been set correclty");
        if (debugTags == null || debugTags.Count == 0)
            Debug.LogError("The list of debug tag is null or empty");

        foreach (DebugTag debugTag in debugTags)
            if (activeDebugTags[debugTag])
                return true;
        return false;
    }

    public void LogMessage(string message, List<DebugTag> debugTags)
    {
        if (IsDebugTagActive(debugTags))
            Debug.Log(message);
    }

    public void LogMessage(string message, UnityEngine.Object context, List<DebugTag> debugTags)
    {
        if (IsDebugTagActive(debugTags))
            Debug.Log(message, context);
    }

    public void LogWarning(string message, List<DebugTag> debugTags)
    {
        if (IsDebugTagActive(debugTags))
            Debug.LogWarning(message);
    }

    public void LogWarning(string message, UnityEngine.Object context, List<DebugTag> debugTags)
    {
        if (IsDebugTagActive(debugTags))
            Debug.LogWarning(message, context);
    }

    public void LogError(string message, List<DebugTag> debugTags)
    {
        if (IsDebugTagActive(debugTags))
            Debug.LogError(message);
    }

    public void LogError(string message, UnityEngine.Object context, List<DebugTag> debugTags)
    {
        if (IsDebugTagActive(debugTags))
            Debug.LogError(message, context);
    }
}
