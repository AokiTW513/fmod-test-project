using UnityEngine;

public class MainMenuSettingUI : SelectUIBase
{
    public void OnEnable()
    {
        if (SaveManager.instance != null)
        {
            if (!SaveManager.instance.CheckSettingSaveData())
            {
                options[2].cantChoose = true;
            }
            else
            {
                options[2].cantChoose = false;
            }
        }
        UpdateDisplay();
    }

    protected override void OnConfirmPauseUI()
    {
        switch (selectedIndex)
        {
            case 0:
                UIManager.instance.ToggleMainMenuAudioSettingUI(true);
                UIManager.instance.ToggleMainMenuSettingUI(false);
                break;
            case 1:
                UIManager.instance.ToggleMainMenuGraphSettingUI(true);
                UIManager.instance.ToggleMainMenuSettingUI(false);
                break;
            case 2:
                GameManager.instance.ClearSaveSettingData();
                options[2].cantChoose = true;
                SaveManager.instance.LoadDefaultSetting();
                break;
        }
    }

    protected override void OnPressBackButton()
    {
        UIManager.instance.ToggleMainMenuUI(true);
        UIManager.instance.ToggleMainMenuSettingUI(false);
    }
}