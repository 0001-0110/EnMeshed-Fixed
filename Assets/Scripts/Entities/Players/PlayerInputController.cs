using Photon.Pun;

public class PlayerInputController : DebugMonoBehaviour
{
    private PlayerController playerController;

    public override void Awake()
    {
        base.Awake();
        debugTags.Add(DebugTag.Entities);

        // Only the local player is receiving local inputs
        enabled = GetComponent<PhotonView>().IsMine;

        playerController = GetComponent<PlayerController>();
    }
}
