using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : DebugMonoBehaviour
{
	public static UIController Instance { get; private set; }

	private MultiplayerController multiplayerController;

	[SerializeField]
	private TextController roomNameTextController;
	[SerializeField]
	private GameObject visibilityButton;
	private Image visibilityButtonImage;
	private TextController visibilityButtonTextController;
	[SerializeField]
	private GameObject escapeMenuObject;

	public async override void Awake()
	{
		base.Awake();
		defaultDebugTag = DebugTag.UI;

		if (Instance != null)
		{
			LogWarning($"There is multiple {this}s, but it should be only one");
			LogWarning($"The previous {Instance} has been replaced with the new one");
		}
		Instance = this;

		multiplayerController = MultiplayerController.Instance;

		visibilityButtonImage = visibilityButton.GetComponent<Image>();
		visibilityButtonTextController = visibilityButton.GetComponentInChildren<TextController>();
		// TODO missing localization string
		await roomNameTextController.SetText($"{{}}: {multiplayerController.CurrentRoom.Name}");
	}

	public void ToggleVisibilty()
	{
		multiplayerController.ToggleVisibility();
	}

	public async void LeaveRoom()
	{
		await multiplayerController.LeaveRoom();
	}

	public void QuitGame()
	{
		// Really useful ?
		LeaveRoom();

		// Application.Quit works only when in a build
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}
