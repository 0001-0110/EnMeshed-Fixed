using System.Threading.Tasks;
using TMPro;

public class LobbyScreenController : GameLoadingScreenController
{
    private MultiplayerController MultiplayerController;

    public TextMeshProUGUI playerInLobbyText;
    public TMP_InputField roomNameInput;

    public int RefreshDelay;

    public override void Awake()
    {
        base.Awake();

        MultiplayerController = MultiplayerController.Instance;
    }

    public void OnEnable()
    {
        SetMode(ScreenMode.Menu);
        Refresh();
    }

    public async void Refresh()
    {
        while (this != null && gameObject.activeInHierarchy)
        {
            playerInLobbyText.text = MultiplayerController.PlayerCountInLobby.ToString();
            await Task.Delay(RefreshDelay);
        }
    }

    public async void CreateRoom()
    {
        await LoadRoom(MultiplayerController.CreateRoom(MultiplayerController.CreateRoomName()));
    }

    public async void JoinRandomOrCreate()
    {
        await LoadRoom(MultiplayerController.JoinRandomOrCreateRoom());
    }

    public async void JoinRoom()
    {
        await LoadRoom(MultiplayerController.JoinRoom(roomNameInput.text.ToUpper()));
    }
}
