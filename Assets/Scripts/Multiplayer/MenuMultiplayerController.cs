using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MenuMultiplayerController : DebugMonoBehaviourPunCallbacks
{
    public static MenuMultiplayerController Instance { get; private set; }

    /// <summary>
    /// Delay before time out (ms)
    /// </summary>
    /// <remarks>
    /// <para>A short timeOutDelay can stop a succesful connection</para>
    /// <para>2000 already proved to be too little</para>
    /// </remarks>
    private const int TimeOutDelay = 5000;

    /// <summary>
    /// Default time between each check
    /// </summary>
    /// <remarks>
    /// <para>A tick too short might create some performance issu�s</para>
    /// </remarks>
    private const int defaultTick = 250;

    public bool IsConnectedToMaster { get; private set; }
    public bool IsConnectedToLobby { get; private set; }
    public bool IsConnectedToRoom { get; private set; }
    public bool OfflineMode => PhotonNetwork.OfflineMode;

    public int PlayerCountInRooms => PhotonNetwork.CountOfPlayersInRooms;
    public int PlayerCountInLobby => PhotonNetwork.CountOfPlayersOnMaster;
    public List<RoomInfo> RoomInfos { get; private set; }

    public Player LocalPlayer => PhotonNetwork.LocalPlayer;
    public Dictionary<int, Player> Players => PhotonNetwork.CurrentRoom.Players;

    ///<remarks>
    /// TODO may not be the best solution in case of the scene being renamed
    /// </remarks>
    [Tooltip("The name of the scene that is going to be loaded when joining a room")]
    public string GameSceneName;

    public override void Awake()
    {
        base.Awake();

        if (Instance != null)
        {
            LogWarning($"There is multiple {this}s, but it should be only one");
            LogWarning($"The previous {Instance} has been replaced with the new one");
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

    #region PUNCALLBACKS

    public override void OnConnectedToMaster() => IsConnectedToMaster = true;

    public override void OnDisconnected(DisconnectCause cause) => IsConnectedToMaster = false;

    public override void OnJoinedLobby() => IsConnectedToLobby = true;

    public override void OnLeftLobby() => IsConnectedToLobby = false;

    public override void OnJoinedRoom()
    {
        IsConnectedToRoom = true;
        PhotonNetwork.LoadLevel(GameSceneName);
    }

    public override void OnLeftRoom() => IsConnectedToRoom = false;

    #endregion

    public string CreateRoomName()
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> ConnectToMaster(int timeOut = TimeOutDelay, int tick = defaultTick)
    {
        PhotonNetwork.ConnectUsingSettings();
        // Wait for the connection to be established or the time out to be reached
        while (timeOut > 0 && !IsConnectedToMaster)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"IsConnectedToMaster is {IsConnectedToMaster}");
        return IsConnectedToMaster;
    }

    public async Task<bool> DisconnectFromMaster(int timeOut = TimeOutDelay, int tick = defaultTick)
    {
        PhotonNetwork.Disconnect();
        while (timeOut > 0 && IsConnectedToMaster)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"IsConnectedToMaster is {IsConnectedToMaster}");
        return !IsConnectedToMaster;
    }

    public async Task<bool> ConnectToLobby(int timeOut = TimeOutDelay, int tick = defaultTick)
    {
        PhotonNetwork.JoinLobby();
        // Wait for the connection to be established or the time out to be reached
        while (timeOut > 0 && !IsConnectedToLobby)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"IsConnectedToLobby is {IsConnectedToLobby}");
        return IsConnectedToLobby;
    }

    public async Task<bool> StartOfflineMode(int timeOut = TimeOutDelay, int tick = defaultTick)
    {
        if (!PhotonNetwork.OfflineMode)
        {
            if (IsConnectedToMaster)
                await DisconnectFromMaster(timeOut, tick);
            PhotonNetwork.OfflineMode = true;
        }
        LogMessage($"Offline mode is {OfflineMode}");
        return OfflineMode;
    }

    public bool StopOfflineMode()
    {
        PhotonNetwork.OfflineMode = false;
        LogMessage($"Offline mode is {OfflineMode}");
        return !OfflineMode;
    }

    public async Task<bool> CreateRoom(string roomName, RoomOptions roomOptions = null, TypedLobby typedLobby = null, string[] expectedUsers = null, int timeOut = TimeOutDelay, int tick = 1)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
        // Wait for the connection to be established or the time out to be reached
        while (timeOut > 0 && !IsConnectedToRoom)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"IsConnectedToRoom is {IsConnectedToRoom}");
        return IsConnectedToRoom;
    }

    public async Task<bool> JoinOrCreateRoom(string roomName, RoomOptions roomOptions = null, TypedLobby typedLobby = null, string[] expectedUsers = null, int timeOut = TimeOutDelay, int tick = defaultTick)
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
        // Wait for the connection to be established or the time out to be reached
        while (timeOut > 0 && !IsConnectedToRoom)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"IsConnectedToRoom is {IsConnectedToRoom}");
        return IsConnectedToRoom;
    }

    public async Task<bool> JoinRandomOrCreateRoom(int timeOut = TimeOutDelay, int tick = defaultTick)
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
        // Wait for the connection to be established or the time out to be reached
        while (timeOut > 0 && !IsConnectedToRoom)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"IsConnectedToRoom is {IsConnectedToRoom}");
        return IsConnectedToRoom;
    }

    public async Task<bool> JoinRoom(string roomName, string[] expectedUsers = null, int timeOut = TimeOutDelay, int tick = defaultTick)
    {
        PhotonNetwork.JoinRoom(roomName, expectedUsers);
        // Wait for the connection to be established or the time out to be reached
        while (timeOut > 0 && !IsConnectedToRoom)
        {
            await Task.Delay(tick);
            timeOut -= tick;
        }
        LogMessage($"IsConnectedToRoom is {IsConnectedToRoom}");
        return IsConnectedToRoom;
    }
}
