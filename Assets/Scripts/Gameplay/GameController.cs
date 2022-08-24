using System;
using UnityEngine;
using Photon.Pun;

public class GameController : DebugMonoBehaviour
{
    public GameObject PlayerPrefab;

    private GameMultiplayerController gameMultiplayerController;
    private ItemController itemController;
    private MapController mapController;
    private UIController UIController;

    public override void Awake()
    {
        if (!DebugController.Initialised)
            throw new Exception("Cannot play before initialisation\nI bet you forgot to switch back to the MenuScene before testing");

        base.Awake();
        // This controller is handling a lot of different things, debugTags must be provided independently

        gameMultiplayerController = GameMultiplayerController.Instance;
        itemController = ItemController.Instance;
        mapController = MapController.Instance;
        UIController = UIController.Instance;
    }

    public void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, mapController.GetPlayerSpawnPosition(), Quaternion.identity);
    }

    private void SpawnEVA()
    {
        PhotonNetwork.InstantiateRoomObject(PlayerPrefab.name, mapController.GetPlayerSpawnPosition(), Quaternion.identity);
    }
}
