using UnityEngine;
using Photon.Pun;

public class PlayerInputController : DebugMonoBehaviour
{
    private PlayerController playerController;

    public override void Awake()
    {
        base.Awake();

        // Only the local player is receiving local inputs
        enabled = GetComponent<PhotonView>().IsMine;

        playerController = GetComponent<PlayerController>();
    }

    public void Update()
    {
        playerController.SetVelocity(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
