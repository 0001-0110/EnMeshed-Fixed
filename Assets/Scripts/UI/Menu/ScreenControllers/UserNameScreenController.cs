using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserNameScreenController : ScreenController
{
	private const string UserNamePreferenceKey = "UserName";

	private MultiplayerController MultiplayerController;

	[SerializeField]
	private TMP_InputField userNameInput;
	[SerializeField]
	private Button validateUserNameButton;
	private readonly TextController validateUserNameButtonTextController;

	[SerializeField]
	private string invalidLocalizationString;

	public override void Awake()
	{
		base.Awake();
		MultiplayerController = MultiplayerController.Instance;
		if (PlayerPrefs.HasKey(UserNamePreferenceKey))
			userNameInput.text = PlayerPrefs.GetString(UserNamePreferenceKey);
	}

	private bool IsInputValid()
	{
		return userNameInput.text != string.Empty;
	}

	private async void InputInvalid(int delay = 2000)
	{
		validateUserNameButton.interactable = false;
		string baseLocalizationString = validateUserNameButtonTextController.LocalizationString;
		await validateUserNameButtonTextController.SetText(invalidLocalizationString);
		await Task.Delay(delay);
		await validateUserNameButtonTextController.SetText(invalidLocalizationString);
		validateUserNameButton.interactable = true;
	}

	public void ValidateUserName(GameObject screen)
	{
		if (!IsInputValid())
			InputInvalid();
		else
		{
			PlayerPrefs.SetString(UserNamePreferenceKey, userNameInput.text);
			MultiplayerController.LocalPlayer.NickName = userNameInput.text;
			OpenScreen(screen);
		}
	}
}
