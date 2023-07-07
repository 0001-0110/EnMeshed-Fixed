using UnityEngine;
using Photon.Pun;

public abstract class DebugMonoBehaviourPunCallbacks : MonoBehaviourPunCallbacks
{
	protected DebugController debugController;
	protected const DebugTag defaultDebugTag = DebugTag.Multiplayer;

	public virtual void Awake()
	{
		debugController = DebugController.Instance;
	}

	protected void LogMessage(string message, DebugTag debugTag = defaultDebugTag)
	{
		if (debugController.IsDebugTagActive(debugTag))
			Debug.Log($"Message:{debugTag} - {message}");
	}

	protected void LogWarning(string message, DebugTag debugTag = defaultDebugTag, params string[] potentialFixes)
	{
		// TODO potential fixes
		if (debugController.IsDebugTagActive(debugTag))
			Debug.LogWarning($"Warning:{debugTag} - {message}");
	}

	protected void LogError(string message, DebugTag debugTag = defaultDebugTag, params string[] potentialFixes)
	{
		// TODO potential fixes
		if (debugController.IsDebugTagActive(debugTag))
			Debug.LogError($"Error:{debugTag} - {message}");
	}
}