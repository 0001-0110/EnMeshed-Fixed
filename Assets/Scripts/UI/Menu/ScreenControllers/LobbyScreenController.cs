using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyScreenController : ModularScreenController
{
    private const string GameSceneName = "GameScene";

    private MultiplayerController multiplayerController;

    public TextMeshProUGUI playerInLobbyText;
    public TMP_InputField roomNameInput;

    public int RefreshDelay;

    public override void Awake()
    {
        base.Awake();
        debugTags.Add(DebugTag.Multiplayer);

        multiplayerController = MultiplayerController.Instance;
    }

    public void OnEnable()
    {
        SetMode(ScreenMode.Menu);
        Refresh();
    }

    public async void Refresh()
    {
        while (gameObject.activeInHierarchy)
        {
            playerInLobbyText.text = multiplayerController.PlayerCountInLobby.ToString();
            await Task.Delay(RefreshDelay);
        }
    }

    public async void CreateRoom()
    {
        SetMode(ScreenMode.Loading);
        if (await multiplayerController.CreateRoom(multiplayerController.CreateRoomName()))
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }

    public async void JoinRandomOrCreate()
    {
        SetMode(ScreenMode.Loading);
        if (await multiplayerController.JoinRandomOrCreateRoom())
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }

    public async void JoinRoom()
    {
        SetMode(ScreenMode.Loading);
        if (await multiplayerController.JoinRoom(roomNameInput.text.ToUpper()))
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }

    // Doesn't work, because the screen must be set to the correct mode before trying to connect
    /*public void LoadGame(bool joined)
    {
        SetMode(ScreenMode.Loading);
        if (joined)
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }*/
}
