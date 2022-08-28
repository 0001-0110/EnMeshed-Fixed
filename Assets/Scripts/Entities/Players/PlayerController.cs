using Photon.Pun;

public class PlayerController : EntityController
{
    private GameMultiplayerController gameMultiplayerController;

    private PlayerInputController inputController;

    public override void Awake()
    {
        base.Awake();

        gameMultiplayerController = GameMultiplayerController.Instance;

        inputController = GetComponent<PlayerInputController>();

        // TODO sync this
        if (photonView.IsMine)
            name = $"Player_{gameMultiplayerController.LocalPlayer.NickName}";
    }

    [PunRPC]
    public void Die()
    {
        // TODO
        throw new System.NotImplementedException();
    }
}
