using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameLoadingScreenController : ModularScreenController
{
    // TODO not the best solution
    public string GameSceneName;

    /// <summary>
    /// TODO comment this you moron
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    protected async Task LoadRoom(Task<bool> joinRoom)
    {
        // TODO need some tests (is the order of execution of the task correct ?)
        // TODO an error is occuring when the app is closed halfway through this operation, fix it
        SetMode(ScreenMode.Loading);
        if (await joinRoom)
            SceneManager.LoadScene(GameSceneName);
        else
            SetMode(ScreenMode.FailedConnection);
    }
}
