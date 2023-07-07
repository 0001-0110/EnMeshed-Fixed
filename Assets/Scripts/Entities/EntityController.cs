using UnityEngine;
using Photon.Pun;

public class EntityController : DebugMonoBehaviour
{
	protected PhotonView photonView;
	// TODO might move camera and audioListener to playerController if there is no use for them in EVAController
	protected new Camera camera;
	protected AudioListener audioListener;
	protected new Rigidbody2D rigidbody;

	protected SpriteRenderer spriteRenderer;

	[SerializeField]
	private int maxVelocity;

	[SerializeField]
	protected int inventorySize;
	protected Inventory inventory;

	public override void Awake()
	{
		base.Awake();

		photonView = GetComponent<PhotonView>();

		// Only the local player needs to activate his camera and audio listener
		camera = GetComponentInChildren<Camera>();
		camera.enabled = photonView.IsMine;
		audioListener = GetComponentInChildren<AudioListener>();
		audioListener.enabled = photonView.IsMine;

		rigidbody = GetComponent<Rigidbody2D>();

		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		inventory = new Inventory(inventorySize);
	}

	#region MOUVEMENTS

	public void SetPosition(Vector2 position)
	{
		rigidbody.position = position;
	}

	public void SetVelocity(Vector2 velocity)
	{
		// The speed must be normalized to avoid diagonal mouvements to be faster than straight ones
		rigidbody.velocity = velocity.normalized * maxVelocity;
		// TODO Flip the sprite around the x axis to look forward
		/*float xScale = spriteRenderer.transform.localScale.x * velocity.x > 0 ? 1 : -1;
        spriteRenderer.transform.localScale = new Vector3(xScale, spriteRenderer.transform.localScale.y);*/
	}

	#endregion
}
