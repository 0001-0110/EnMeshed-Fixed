using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
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
            // If we already have a debug controller, we need to destroy the new one
            // Duplicates are going to happen every time we go back to a scene where a multiplayer controller is instantiated
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // This gameObject is not going to be destroyed when loading another scene
        DontDestroyOnLoad(gameObject);

        // Only log debugs if this build is a debug build
        Debug.unityLogger.logEnabled = Debug.isDebugBuild;
        // Even tho this method is already called by OnValidate, OnValidate only works in editor
        // We must keep it for the builds
        SetActiveDebugTags();
    }

    public void OnValidate()
    {
        SetActiveDebugTags();
    }

    private void SetActiveDebugTags()
    {
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
    }

    public bool IsDebugTagActive(DebugTag debugTag)
    {
        return activeDebugTags[debugTag];
    }

    /*public void LogMessage(string message, UnityEngine.Object context, DebugTag debugTag)
    {
        // TODO potential fixes
        if (activeDebugTags[debugTag])
            Debug.Log($"Message:{debugTag} - {message}", context);
    }

    public void LogWarning(string message, UnityEngine.Object context, DebugTag debugTag, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (activeDebugTags[debugTag])
            Debug.LogWarning($"Warning:{debugTag} - {message}", context);
    }

    public void LogError(string message, UnityEngine.Object context, DebugTag debugTag, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (activeDebugTags[debugTag])
            Debug.LogError($"Error:{debugTag} - {message}", context);
    }*/
}
