using System.Threading.Tasks;
using TMPro;

public class LobbyScreenController : GameLoadingScreenController
{
    private MenuMultiplayerController menuMultiplayerController;

    public TextMeshProUGUI playerInLobbyText;
    public TMP_InputField roomNameInput;

    public int RefreshDelay;

    public override void Awake()
    {
        base.Awake();

        menuMultiplayerController = MenuMultiplayerController.Instance;
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
            playerInLobbyText.text = menuMultiplayerController.PlayerCountInLobby.ToString();
            await Task.Delay(RefreshDelay);
        }
    }

    public async void CreateRoom()
    {
        await LoadRoom(menuMultiplayerController.CreateRoom(menuMultiplayerController.CreateRoomName()));
    }

    public async void JoinRandomOrCreate()
    {
        await LoadRoom(menuMultiplayerController.JoinRandomOrCreateRoom());
    }

    public async void JoinRoom()
    {
        await LoadRoom(menuMultiplayerController.JoinRoom(roomNameInput.text.ToUpper()));
    }
}
