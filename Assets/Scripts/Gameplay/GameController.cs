using System;
using UnityEngine;
using Photon.Pun;

public class GameController : DebugMonoBehaviour
{
    public GameObject PlayerPrefab;
    public PlayerController LocalPlayer { get; private set; }

    private MultiplayerController MultiplayerController;
    private ItemController itemController;
    private MapController mapController;
    private UIController UIController;

    public override void Awake()
    {
        if (MultiplayerController.Instance == null)
            throw new Exception("FATAL ERROR: I bet you forgot to switch back to the MenuScene before testing");

        base.Awake();
        defaultDebugTag = DebugTag.Gameplay;

        MultiplayerController = MultiplayerController.Instance;
        itemController = ItemController.Instance;
        mapController = MapController.Instance;
        UIController = UIController.Instance;
    }

    public async void Start()
    {
        SpawnPlayer();
        await System.Threading.Tasks.Task.Delay(5000);
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
        MultiplayerController.StartGame();
        //UIController.
    }
}
