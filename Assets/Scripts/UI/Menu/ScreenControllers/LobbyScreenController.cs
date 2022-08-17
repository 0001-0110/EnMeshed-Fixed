using System.Threading.Tasks;
using UnityEngine.SceneManagement;
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
        debugTags.Add(DebugTag.Multiplayer);

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

    /*public async void CreateRoom()
    {
        SetMode(ScreenMode.Loading);
        if (await menuMultiplayerController.CreateRoom(menuMultiplayerController.CreateRoomName()))
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }*/

    public async void CreateRoom()
    {
        await LoadRoom(menuMultiplayerController.CreateRoom(menuMultiplayerController.CreateRoomName()));
    }

    /*public async void JoinRandomOrCreate()
    {
        SetMode(ScreenMode.Loading);
        if (await menuMultiplayerController.JoinRandomOrCreateRoom())
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }*/

    public async void JoinRandomOrCreate()
    {
        await LoadRoom(menuMultiplayerController.JoinRandomOrCreateRoom());
    }

    /*public async void JoinRoom()
    {
        SetMode(ScreenMode.Loading);
        if (await menuMultiplayerController.JoinRoom(roomNameInput.text.ToUpper()))
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }*/

    public async void JoinRoom()
    {
        await LoadRoom(menuMultiplayerController.JoinRoom(roomNameInput.text.ToUpper()));
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
