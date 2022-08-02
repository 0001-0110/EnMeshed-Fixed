using UnityEngine;

public abstract class DebugMonoBehavior : MonoBehaviour
{
    protected DebugController debugController;
    public DebugTag[] DebugTags;

    public virtual void Awake()
    {
        debugController = GameObject.Find("DebugController").GetComponent<DebugController>();
    }

    protected void LogMessage(string message)
    {
        debugController.LogMessage(message, gameObject, DebugTags);
    }

    protected void LogMessage(string message, Object context)
    {
        debugController.LogMessage(message, context, DebugTags);
    }

    protected void LogWarning(string message)
    {
        debugController.LogWarning(message, gameObject, DebugTags);
    }

    protected void LogWarning(string message, Object context)
    {
        debugController.LogWarning(message, context, DebugTags);
    }

    protected void LogError(string message)
    {
        debugController.LogError(message, gameObject, DebugTags);
    }

    protected void LogError(string message, Object context)
    {
        debugController.LogError(message, context, DebugTags);
    }
}