using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : DebugMonoBehaviour
{
    public RoomController PlayerSpawnRoom;
    public RoomController EVASpawnRoom;

    public override void Awake()
    {
        base.Awake();
        debugTags.Add(DebugTag.Level);
    }

    public Vector2 GetPlayerSpawnPosition()
    {
        // TODO
        return PlayerSpawnRoom.transform.position;
    }

    public Vector2 GetEVASpawnPosition()
    {
        // TODO
        return EVASpawnRoom.transform.position;
    }
}
