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
        // TODO potential fixes
        if (debugController.IsDebugTagActive(debugTag))
            Debug.LogWarning($"Warning: {debugTag} - {message}");
    }

    protected void LogWarning(string message, GameObject context, DebugTag debugTag, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (debugController.IsDebugTagActive(debugTag))
            Debug.LogWarning($"Warning: {debugTag} - {message}", context);
    }

    protected void LogWarning(string message, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.LogWarning($"Warning: {defaultDebugTag} - {message}");
    }

    protected void LogWarning(string message, GameObject context, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.LogWarning($"Warning: {defaultDebugTag} - {message}", context);
    }

    protected void LogError(string message, DebugTag debugTag, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (debugController.IsDebugTagActive(debugTag))
            Debug.LogError($"Error: {debugTag} - {message}");
    }

    protected void LogError(string message, GameObject context, DebugTag debugTag, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (debugController.IsDebugTagActive(debugTag))
            Debug.LogError($"Error: {debugTag} - {message}", context);
    }

    protected void LogError(string message, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.LogError($"Error: {defaultDebugTag} - {message}");
    }

    protected void LogError(string message, GameObject context, params string[] potentialFixes)
    {
        // TODO potential fixes
        if (debugController.IsDebugTagActive(defaultDebugTag))
            Debug.LogError($"Error: {defaultDebugTag} - {message}", context);
    }
}