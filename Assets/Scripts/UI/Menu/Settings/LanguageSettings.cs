using System.Linq;
using System.Collections.Generic;
using TMPro;

using Services;

public class LanguageSettings : DebugMonoBehaviour
{
    private LanguageController languageController;
    public TMP_Dropdown LanguageSelectionDropDown;

    private Dictionary<Language, string> languages = new Dictionary<Language, string>()
    {
        [Language.English] = "Language_English",
        [Language.French] = "Language_French",
        [Language.German] = "Language_German",
        /*[Language.Romanian] = "Language_Romanian",
        [Language.Arabic] = "Language_Arabic",
        [Language.Turkish] = "Language_Turkish",
        [Language.Italian] = "Language_Italian",
        [Language.Serbian] = "Language_Serbian",
        [Language.Portuguese] = "Language_Portuguese",
        [Language.Spanish] = "Language_Spanish",*/
    };
    private List<string> languageTexts;

    public override void Awake()
    {
        base.Awake();

        languageController = LanguageController.Instance;
        InitLanguageSelection();
    }

    public void OnEnable()
    {
        DisplayLanguageSelection();
    }

    /// <summary>
    /// Display all the avalaible languages
    /// </summary>
    private void InitLanguageSelection()
    {
        languageTexts = languages.Values.ToList();
        LanguageSelectionDropDown.ClearOptions();
        // This line might cause problems if the language is still loading, but this is very unlikely to ever happen
        List<string> options = ListService.ForEach(languageTexts, languageText => languageController.GetText(languageText));
        LanguageSelectionDropDown.AddOptions(options);
    }

    private void DisplayLanguageSelection()
    {
        LanguageSelectionDropDown.value = languageTexts.IndexOf(languages[languageController.CurrentLanguage]);
    }

    public void UpdateLanguage()
    {
        // TODO need some comments, do it or you'll regret it later
        languageController.SetLanguage(languages.First(pair => pair.Value == languageTexts[LanguageSelectionDropDown.value]).Key);
    }
}
