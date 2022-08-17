using UnityEngine;
using Photon.Pun;

public class EntityController : DebugMonoBehaviour
{
    private PhotonView photonView;
    private Camera entityCamera;
    private AudioListener audioListener;

    public int InventorySize;
    protected Inventory inventory;

    public override void Awake()
    {
        base.Awake();

        photonView = GetComponent<PhotonView>();
        entityCamera = GetComponentInChildren<Camera>();
        audioListener = GetComponentInChildren<AudioListener>();
        // only the local player needs to activate his camera and audio listener
        entityCamera.enabled = photonView.IsMine;
        audioListener.enabled = photonView.IsMine;

        inventory = new Inventory(InventorySize);
    }
}
