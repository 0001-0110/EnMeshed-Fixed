using Photon.Pun;

public class PlayerController : EntityController
{
    private PlayerInputController inputController;

    public override void Awake()
    {
        base.Awake();

        inputController = GetComponent<PlayerInputController>();
        // The local player is the only one controlled by the inputs
        //inputController.enabled = photonView.IsMine;
    }

    [PunRPC]
    public void Die()
    {
        // TODO
        throw new System.NotImplementedException();
    }
}
