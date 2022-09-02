using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserNameScreenController : ScreenController
{
    private const string UserNamePreferenceKey = "UserName";

    private MultiplayerController MultiplayerController;

    public TMP_InputField UserNameInput;
    public Button ValidateUserNameButton;
    private TextController ValidateUserNameButtonTextController;

    public string InvalidLocalizationString;

    public override void Awake()
    {
        base.Awake();
        MultiplayerController = MultiplayerController.Instance;
        if (PlayerPrefs.HasKey(UserNamePreferenceKey))
            UserNameInput.text = PlayerPrefs.GetString(UserNamePreferenceKey);
    }

    private bool IsInputValid()
    {
        return UserNameInput.text != string.Empty;
    }

    private async void InputInvalid(int delay = 2000)
    {
        ValidateUserNameButton.interactable = false;
        string baseLocalizationString = ValidateUserNameButtonTextController.LocalizationString;
        await ValidateUserNameButtonTextController.SetText(InvalidLocalizationString);
        await Task.Delay(delay);
        await ValidateUserNameButtonTextController.SetText(InvalidLocalizationString);
        ValidateUserNameButton.interactable = true;
    }

    public void ValidateUserName(GameObject screen)
    {
        if (!IsInputValid())
            InputInvalid();
        else
        {
            PlayerPrefs.SetString(UserNamePreferenceKey, UserNameInput.text);
            MultiplayerController.LocalPlayer.NickName = UserNameInput.text;
            OpenScreen(screen);
        }
    }
}
