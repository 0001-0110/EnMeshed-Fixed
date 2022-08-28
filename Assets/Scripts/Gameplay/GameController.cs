using System;
using UnityEngine;
using Photon.Pun;

public class GameController : DebugMonoBehaviour
{
    public GameObject PlayerPrefab;
    public PlayerController LocalPlayer { get; private set; }

    private GameMultiplayerController gameMultiplayerController;
    private ItemController itemController;
    private MapController mapController;
    private UIController UIController;

    public override void Awake()
    {
        gameMultiplayerController = GameMultiplayerController.Instance;
        itemController = ItemController.Instance;
        mapController = MapController.Instance;
        UIController = UIController.Instance;

        if (!gameMultiplayerController.IsConnectedToRoom)
            throw new Exception("Cannot play before connecting to a room\nI bet you forgot to switch back to the MenuScene before testing");

        base.Awake();
        defaultDebugTag = DebugTag.Gameplay;
    }

    public void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        LocalPlayer = PhotonNetwork.Instantiate(PlayerPrefab.name, mapController.GetPlayerSpawnPosition(), Quaternion.identity).GetComponent<PlayerController>();
        LogMessage("Spawned a new player", DebugTag.Entities);
    }

    private void SpawnEVA()
    {
        // InstantiateRoomObject to avoid EVA leaving the game in case of the host disconnecting
        PhotonNetwork.InstantiateRoomObject(PlayerPrefab.name, mapController.GetPlayerSpawnPosition(), Quaternion.identity);
    }

    public void StartGame()
    {
        gameMultiplayerController.StartGame();
        //UIController.
    }
}
