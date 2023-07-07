using UnityEngine;

public class RoomController : DebugMonoBehaviour
{
	protected Collider2D triggerCollider;
	//protected List<PlayerController> 

	public override void Awake()
	{
		base.Awake();

		triggerCollider = GetComponent<Collider2D>();
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		// TODO
	}

	public void OnCollisionExit2D(Collision2D collision)
	{
		// TODO
	}
}
