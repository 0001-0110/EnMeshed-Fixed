using UnityEngine;

public abstract class DebugMonoBehaviour : MonoBehaviour
{
    protected DebugController debugController;
    protected DebugTag defaultDebugTag;

    public virtual void Awake()
    {
        debugController = DebugController.Instance;
    }

    protected void LogMessage(string message, DebugTag debugTag)
    {
        if (debugController.IsDebugTagActive(debugTag))
            Debug.Log($"Message: {debugTag} - {message}");
    }

    protected void LogMessage(string message, GameObject context, DebugTag debugTag)
    {
        if (debugController.IsDebugTagActive(debugTag))
            Debug.Log($"Message: {debugTag} - {message}", context);
    }

    protected void LogMessage(string message)
    {
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.Log($"Message: {defaultDebugTag} - {message}");
    }

    protected void LogMessage(string message, GameObject context)
    {
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.Log($"Message: {defaultDebugTag} - {message}", context);
    }

    protected void LogWarning(string message, DebugTag debugTag, params string[] potentialFixes)
    {
        string potentialFixesString = potentialFixes.Length == 0 ? "" : $"\n- {string.Join("\n- ", potentialFixes)}";
        if (debugController.IsDebugTagActive(debugTag))
            Debug.LogWarning($"Warning: {debugTag} - {message}{potentialFixesString}");
    }

    protected void LogWarning(string message, GameObject context, DebugTag debugTag, params string[] potentialFixes)
    {
        string potentialFixesString = potentialFixes.Length == 0 ? "" : $"\n- {string.Join("\n- ", potentialFixes)}";
        if (debugController.IsDebugTagActive(debugTag))
            Debug.LogWarning($"Warning: {debugTag} - {message}{potentialFixesString}", context);
    }

    protected void LogWarning(string message, params string[] potentialFixes)
    {
        string potentialFixesString = potentialFixes.Length == 0 ? "" : $"\n- {string.Join("\n- ", potentialFixes)}";
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.LogWarning($"Warning: {defaultDebugTag} - {message}{potentialFixesString}");
    }

    protected void LogWarning(string message, GameObject context, params string[] potentialFixes)
    {
        string potentialFixesString = potentialFixes.Length == 0 ? "" : $"\n- {string.Join("\n- ", potentialFixes)}";
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.LogWarning($"Warning: {defaultDebugTag} - {message}{potentialFixesString}", context);
    }

    protected void LogError(string message, DebugTag debugTag, params string[] potentialFixes)
    {
        string potentialFixesString = potentialFixes.Length == 0 ? "" : $"\n- {string.Join("\n- ", potentialFixes)}";
        if (debugController.IsDebugTagActive(debugTag))
            Debug.LogError($"Error: {debugTag} - {message}{potentialFixesString}");
    }

    protected void LogError(string message, GameObject context, DebugTag debugTag, params string[] potentialFixes)
    {
        string potentialFixesString = potentialFixes.Length == 0 ? "" : $"\n- {string.Join("\n- ", potentialFixes)}";
        if (debugController.IsDebugTagActive(debugTag))
            Debug.LogError($"Error: {debugTag} - {message}{potentialFixesString}", context);
    }

    protected void LogError(string message, params string[] potentialFixes)
    {
        string potentialFixesString = potentialFixes.Length == 0 ? "" : $"\n- {string.Join("\n- ", potentialFixes)}";
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.LogError($"Error: {defaultDebugTag} - {message}{potentialFixesString}");
    }

    protected void LogError(string message, GameObject context, params string[] potentialFixes)
    {
        string potentialFixesString = potentialFixes.Length == 0 ? "" : $"\n- {string.Join("\n- ", potentialFixes)}";
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.LogError($"Error: {defaultDebugTag} - {message}{potentialFixesString}", context);
    }
}