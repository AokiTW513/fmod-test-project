using UnityEngine;

public class SettingUI : SelectUIBase
{
    protected override void OnConfirmPauseUI()
    {
        switch (selectedIndex)
        {
            case 0:
                UIManager.instance.ToggleAudioSettingUI(true);
                UIManager.instance.ToggleSettingUI(false);
                break;
            case 1:
                UIManager.instance.ToggleGraphSettingUI(true);
                UIManager.instance.ToggleSettingUI(false);
                break;
        }
    }

    protected override void OnPressBackButton()
    {
        UIManager.instance.TogglePauseUI(true);
        UIManager.instance.ToggleSettingUI(false);
    }
}