using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageController : DebugMonoBehaviour
{
    // This string is used to store player preferences
    private const string LanguagePreferenceKey = "Language";

    public static LanguageController Instance { get; private set; }
    public List<TextController> textControllers { get; set; }

    public Language CurrentLanguage { get; private set; }
    public Dictionary<string, string> LocalizationStrings { get; private set; }

    public override void Awake()
    {
        base.Awake();
        defaultDebugTag = DebugTag.Language;

        if (Instance != null)
        {
            LogError($"There is multiple {this}s, but it should be only one");
            LogWarning($"The previous {Instance} has been replaced with a new one");
        }
        Instance = this;
        // TODO what even is that thing ?
        SceneManager.sceneLoaded += OnSceneLoaded;
        textControllers = new List<TextController>();

        DontDestroyOnLoad(gameObject);

        // Load the prefered language
        // If there is a preferenced saved, load it
        // If not, load the system language
        // If the system language is not recognized, load english
        if (PlayerPrefs.HasKey(LanguagePreferenceKey))
            SetLanguage(PlayerPrefs.GetInt(LanguagePreferenceKey));
        else
            SetLanguage(GetSystemLanguage());
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //debug.LogMessage("LanguageController.OnSceneLoaded called", gameObject, DebugTag.Language);
        // Clear the list containing all textControllers, to avoid referencing textControllers on another scene (destroyed)
        textControllers = new List<TextController>();
    }

    private Language GetSystemLanguage(Language DefaultLanguage = Language.English)
    {
        return Enum.IsDefined(typeof(Language), (int)Application.systemLanguage) ? DefaultLanguage : (Language)Application.systemLanguage;
    }

    private XmlNodeList LoadLocalizationFile(string fileName)
    {
        XmlDocument localizationFile = Services.FileService.LoadXml($"LocalizationFiles/{fileName}.xml");
        return localizationFile.SelectNodes($"{fileName}/string");
    }

    private async Task UpdateAllTextControllers()
    {
        /*foreach (TextController textController in textControllers)
            textController.UpdateText();*/

        // TODO this code need some serious testing, I have no idea if this can work
        List<Task> tasks = new List<Task>();
        foreach (TextController textController in textControllers)
            tasks.Add(Task.Run(textController.UpdateText));
        await Task.WhenAll(tasks);
    }

    public async void SetLanguage(Language language)
    {
        // If the language is not defined, trying to load could create many errors
        if (!Enum.IsDefined(typeof(Language), language))
            throw new ArgumentException("I don't speak Klingon");

        CurrentLanguage = language;
        // Save this value for the next time the app is used
        PlayerPrefs.SetInt(LanguagePreferenceKey, (int)CurrentLanguage);
        LogMessage($"CurrentLanguage: {CurrentLanguage}");
        
        LocalizationStrings = new Dictionary<string, string>();
        foreach (string fileName in new string[] { "General", CurrentLanguage.ToString() })
        {
            foreach (XmlNode node in LoadLocalizationFile(fileName))
            {
                // "name" is the attribute containing the localization string in the xml file
                LocalizationStrings.Add(node.Attributes["name"].Value, node.InnerText);
            }
        }

        // TODO how do I handle this bit ?
        await UpdateAllTextControllers();
    }

    public void SetLanguage(int languageIndex)
    {
        SetLanguage((Language)languageIndex);
    }

    public string GetText(string localizationString)
    {
        if (!LocalizationStrings.ContainsKey(localizationString))
        {
            // If there is no translation available
            LogWarning($"Missing translation for {localizationString}");
            return localizationString;
        }
        else
            //
            return LocalizationStrings[localizationString];
    }

    public string GetLocalizationString(string text)
    {
        foreach (KeyValuePair<string, string> pair in LocalizationStrings)
        {
            if (pair.Value == text)
                return pair.Key;
        }
        return null;
    }
}
