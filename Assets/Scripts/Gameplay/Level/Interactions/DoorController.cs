using UnityEngine;
using Photon.Pun;

public class DoorController : DebugMonoBehaviour
{
    private PhotonView photonView;

    [SerializeField]
    [Tooltip("The collider responsible for blocking players")]
    private Collider2D hitbox;
    // This field might be more useful in the interaction
    [SerializeField]
    [Tooltip("The collider responsible for detecting players next to the door")]
    private Collider2D trigger;
    private AudioSource audioSource;

    public override void Awake()
    {
        base.Awake();
        photonView = GetComponent<PhotonView>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void SetDoorState(bool open)
    {
        photonView.RPC("SetDoorStateRPC", RpcTarget.All, open);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="open">true means open, false means close</param>
    [PunRPC]
    public void SetDoorStateRPC(bool open)
    {
        hitbox.enabled = !open;
    }
}
