using Photon.Pun;

public class PlayerController : EntityController
{
	private MultiplayerController MultiplayerController;

	private PlayerInputController inputController;

	public override void Awake()
	{
		base.Awake();

		MultiplayerController = MultiplayerController.Instance;

		inputController = GetComponent<PlayerInputController>();

		// TODO sync this
		if (photonView.IsMine)
		{
			name = $"Player_{MultiplayerController.LocalPlayer.NickName}";
			// TODO set user name
		}
	}

	[PunRPC]
	public void Die()
	{
		// TODO
		throw new System.NotImplementedException();
	}
}
