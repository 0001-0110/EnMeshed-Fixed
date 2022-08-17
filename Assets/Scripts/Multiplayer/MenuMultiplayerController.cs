using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;

public class MenuMultiplayerController : DebugMonoBehaviourPunCallbacks
{
    public static MenuMultiplayerController Instance { get; private set; }

    /// <summary>
    /// Delay before time out (ms)
    /// </summary>
    public const int TimeOutDelay = 2000;

    public bool IsConnected => PhotonNetwork.IsConnected;
    public bool IsConnectedAndReady => PhotonNetwork.IsConnectedAndReady;
    public bool InLobby => PhotonNetwork.InLobby;
    public bool InRoom => PhotonNetwork.InRoom;
    public bool OfflineMode => PhotonNetwork.OfflineMode;

    public int PlayerCountInRooms => PhotonNetwork.CountOfPlayersInRooms;
    public int PlayerCountInLobby => PhotonNetwork.CountOfPlayersOnMaster;
    public List<RoomInfo> roomInfos { get; private set; }

    public Player LocalPlayer => PhotonNetwork.LocalPlayer;
    public Dictionary<int, Player> Players => PhotonNetwork.CurrentRoom.Players;

    public override void Awake()
    {
        // This class does not need to init debugTags since it inherits from DebugMonoBehaviourPunCallbacks, which already does that
        base.Awake();

        if (Instance != null)
        {
            LogError($"ERROR - MULTIPLAYER | There is multiple MultiplayerControllers, but it should be only one");
            LogWarning($"WARNING - MULTIPLAYER | The previous MultiplayerController has been replaced with a new one");
        }
        Instance = this;
    }

    /// <summary>
    /// TODO doesn't work
    /// There is a bug when leaving the app while in room
    /// </summary>
    public void OnApplicationQuit()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }

    public string CreateRoomName()
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> ConnectToMaster(int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.ConnectUsingSettings();
        // Wait for the connection to be established or the time out to be reached
        while (timeOut >= 0 && !IsConnectedAndReady)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        if (IsConnectedAndReady)
            LogMessage($"DEBUG - MULTIPLAYER | IsConnectedAndReady is {IsConnectedAndReady}");
        return IsConnectedAndReady;
    }

    public async Task<bool> DisconnectFromMaster(int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.Disconnect();
        while (timeOut >= 0 && !IsConnectedAndReady)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"DEBUG - MULTIPLAYER | IsConnected is {IsConnected}");
        return !IsConnected;
    }

    public async Task<bool> ConnectToLobby(int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.JoinLobby();
        // Wait for the connection to be established or the time out to be reached
        while (timeOut >= 0 && !InLobby)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"DEBUG - MULTIPLAYER | InLobby is {InLobby}");
        return InLobby;
    }

    public async Task<bool> StartOfflineMode(int timeOut = TimeOutDelay, int tick = 1)
    {
        if (!PhotonNetwork.OfflineMode)
        {
            if (IsConnectedAndReady)
                await DisconnectFromMaster(timeOut, tick);
            PhotonNetwork.OfflineMode = true;
        }
        LogMessage($"DEBUG - MULTIPLAYER | Offline mode is {OfflineMode}");
        return OfflineMode;
    }

    public bool StopOfflineMode(int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.OfflineMode = false;
        LogMessage($"DEBUG - MULTIPLAYER | Offline mode is {OfflineMode}");
        return !OfflineMode;
    }

    public async Task<bool> CreateRoom(string roomName, RoomOptions roomOptions = null, TypedLobby typedLobby = null, string[] expectedUsers = null, int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
        // Wait for the connection to be established or the time out to be reached
        while (timeOut >= 0 && !InRoom)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"DEBUG - MULTIPLAYER | InRoom is {InRoom}");
        return InRoom;
    }

    public async Task<bool> JoinOrCreateRoom(string roomName, RoomOptions roomOptions = null, TypedLobby typedLobby = null, string[] expectedUsers = null, int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
        // Wait for the connection to be established or the time out to be reached
        while (timeOut >= 0 && !InRoom)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"DEBUG - MULTIPLAYER | InRoom is {InRoom}");
        return InRoom;
    }

    public async Task<bool> JoinRandomOrCreateRoom(int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
        // Wait for the connection to be established or the time out to be reached
        while (timeOut >= 0 && !InRoom)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"DEBUG - MULTIPLAYER | InRoom is {InRoom}");
        return InRoom;
    }

    public async Task<bool> JoinRoom(string roomName, string[] expectedUsers = null, int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.JoinRoom(roomName, expectedUsers);
        // Wait for the connection to be established or the time out to be reached
        while (timeOut >= 0 && !InRoom)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"DEBUG - MULTIPLAYER | InRoom is {InRoom}");
        return InRoom;
    }
}
