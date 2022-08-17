using System;
using UnityEngine;
using Photon.Pun;

using Services;

public class GameController : DebugMonoBehaviour
{
    public GameObject PlayerPrefab;

    public ItemController ItemController;
    public MapController MapController;
    public UIController UIController;

    public override void Awake()
    {
        if (!DebugController.Initialised)
            throw new Exception("Cannot play before initialisation\nI bet you forgot to switch back to the MenuScene before testing");

        base.Awake();
        // This controller is handling a lot of different things, debugTags must be provided independently

        PhotonService.AddToPrefabPool(PlayerPrefab);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, MapController.GetPlayerSpawnPosition(), Quaternion.identity);
    }

    private void SpawnEVA()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, MapController.GetPlayerSpawnPosition(), Quaternion.identity);
    }
}
