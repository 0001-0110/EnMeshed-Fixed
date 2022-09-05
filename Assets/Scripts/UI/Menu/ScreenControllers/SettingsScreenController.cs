using UnityEngine;
using UnityEngine.UI;

public class SettingsScreenController : ScreenController
{
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private GameObject[] settingsPanels;
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
        buttons[activeSettingsPanelIndex].interactable = true;
        settingsPanels[activeSettingsPanelIndex].gameObject.SetActive(false);
        //
        activeSettingsPanelIndex = index;
        //
        buttons[index].interactable = false;
        settingsPanels[index].gameObject.SetActive(true);

    }
}
