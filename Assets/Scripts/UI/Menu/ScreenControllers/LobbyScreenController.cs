using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyScreenController : ModularScreenController
{
    private MenuMultiplayerController menuMultiplayerController;

    // TODO not the best solution
    public string GameSceneName;

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
        while (gameObject.activeInHierarchy)
        {
            playerInLobbyText.text = menuMultiplayerController.PlayerCountInLobby.ToString();
            await Task.Delay(RefreshDelay);
        }
    }

    /// <summary>
    /// TODO comment this you moron
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    private async Task LoadRoom(Task<bool> task)
    {
        // TODO need some tests (is the order of execution of the task correct ?)
        // TODO an error is occuring when the app is closed halfway through this operation, fix it
        SetMode(ScreenMode.Loading);
        if (await task)
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
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
