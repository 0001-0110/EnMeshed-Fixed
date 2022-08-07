using UnityEngine;
using UnityEngine.UI;

public class SettingsScreenController : ScreenController
{
    public Button[] Buttons;
    public GameObject[] settingsPanels;
    private int activeSettingsPanelIndex;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        activeSettingsPanelIndex = 0;
        ShowSettingsPanel(0);
    }

    public void ShowSettingsPanel(int index)
    {
        //
        Buttons[activeSettingsPanelIndex].interactable = true;
        settingsPanels[activeSettingsPanelIndex].gameObject.SetActive(false);
        //
        activeSettingsPanelIndex = index;
        //
        Buttons[index].interactable = false;
        settingsPanels[index].gameObject.SetActive(true);

    }
}
