using UnityEngine;

public class MapController : DebugMonoBehaviour
{
	public static MapController Instance { get; private set; }

	[SerializeField]
	private RoomController playerSpawnRoom;
	[SerializeField]
	private RoomController EVASpawnRoom;


	public override void Awake()
	{
		base.Awake();
		defaultDebugTag = DebugTag.Level;

		if (Instance != null)
		{
			LogWarning($"There is multiple {this}s, but it should be only one");
			LogWarning($"The previous {Instance} has been replaced with the new one");
		}
		Instance = this;
	}

	public Vector2 GetPlayerSpawnPosition()
	{
		// TODO
		return playerSpawnRoom.GetComponent<RectTransform>().anchoredPosition;
	}

	public Vector2 GetEVASpawnPosition()
	{
		// TODO
		return EVASpawnRoom.GetComponent<RectTransform>().anchoredPosition;
	}
}
