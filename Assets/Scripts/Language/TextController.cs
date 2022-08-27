using System.Text.RegularExpressions;
using TMPro;

/// <summary>
/// Must be placed as a componnent of the text object
/// </summary>
public class TextController : DebugMonoBehaviour
{
    // Regex that matches every localization in between curly braces
    private static Regex regex = new Regex("{([^}]*)}");

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
        SetText(LocalizationString);
    }

    public void SetText(string localizationString)
    {
        if (localizationString == string.Empty)
        {
            LogWarning("WARNING: Language - Missing localization string", gameObject);
            return;
        }

        LocalizationString = localizationString;
        // text might be null if SetText is called before this object is enabled
        // This is not a problem tho, as it will only update the localization string and wiat for OnEnable to update the text
        if (text != null)
        {
            text.text = localizationString;
            Match match = regex.Match(localizationString);
            for (int i = 1; i < match.Groups.Count; i++)
                text.text = regex.Replace(text.text, languageController.GetText(match.Groups[i].Value));
            LogMessage(text.text);
        }
    }
}
