using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

/// <summary>
/// Must be placed as a componnent of the text object
/// </summary>
public class TextController : DebugMonoBehaviour
{
	// TODO better name needed
	// Regex that matches every localization in between curly braces
	private static readonly Regex regex = new Regex("{([^}]*)}");

	private LanguageController languageController;
	private TextMeshProUGUI text;

	[SerializeField]
	public string LocalizationString { get; private set; }

	public async override void Awake()
	{
		base.Awake();
		defaultDebugTag = DebugTag.Language;

		languageController = LanguageController.Instance;
		// Add this TextController to the list of controllers to be updated in case of a language change
		languageController.TextControllers.Add(this);

		text = GetComponent<TextMeshProUGUI>();
		if (text == null)
			LogWarning($"This TextController is missing a text to control", "This may be due to the text not being a TextMeshProUGUI");

		await UpdateText();
	}

	public void OnDestroy()
	{
		// To avoid referencing an object that doesn't exist anymore
		languageController.TextControllers.Remove(this);
	}

	public async Task UpdateText()
	{
		await SetText(LocalizationString);
	}

	public async Task SetText(string localizationString)
	{
		if (localizationString == string.Empty)
		{
			LogWarning("WARNING: Language - Invalid localization string", gameObject);
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
				text.text = regex.Replace(text.text, await languageController.GetTextAsync(match.Groups[i].Value));
		}
	}
}
