using UnityEngine;
using Photon.Pun;

public class ItemController : DebugMonoBehaviour
{
	public static ItemController Instance { get; private set; }

	public override void Awake()
	{
		base.Awake();
		defaultDebugTag = DebugTag.Inventory;

		if (Instance != null)
		{
			LogWarning($"There is multiple {this}s, but it should be only one");
			LogWarning($"The previous {Instance} has been replaced with the new one");
		}
		Instance = this;
	}

	public void InstantiateItem(GameObject item, Vector3 position, Quaternion rotation)
	{
		PhotonNetwork.InstantiateRoomObject(item.name, position, rotation);
	}

	public void InstantiateItem(GameObject item, Vector3 position)
	{
		InstantiateItem(item, position);
	}
}
