using TMPro;

/// <summary>
/// Must be placed as a componnent of the text object
/// </summary>
public class TextController : DebugMonoBehaviour
{
    private LanguageController languageController;
    private TextMeshProUGUI text;

    public string LocalizationString;

    public override void Awake()
    {
        base.Awake();
        defaultDebugTag = DebugTag.Language;

        languageController = LanguageController.Instance;
        // Add this TextController to the list of controllers to be updated in case of a language change
        languageController.textControllers.Add(this);

        text = GetComponent<TextMeshProUGUI>();
        if (text == null)
            LogWarning($"This TextController is missing a text to control", "This may be due to the text not being a TextMeshProUGUI");

        UpdateText();
    }

    public void OnDestroy()
    {
        // To avoid referencing an object that doesn't exist anymore
        languageController.textControllers.Remove(this);
    }

    public void UpdateText()
    {
        if (LocalizationString == string.Empty)
        {
            LogWarning("WARNING: Language - Missing localization string", gameObject);
            return;
        }
        SetText(LocalizationString);
    }

    public void SetText(string localizationString)
    {
        this.LocalizationString = localizationString;
        // text might be null if SetText is called before this object is enabled
        // This is not a problem tho, as it will only update the localization string and wiat for OnEnable to update the text
        if (text != null)
            text.text = languageController.GetText(localizationString);
    }
}
