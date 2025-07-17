using UnityEngine;
using TMPro;

public class MainMenuAudioSettingUI : SelectUIBase
{
    public void OnEnable()
    {
        if (AudioManager.instance != null)
        {
            options[0].value = AudioManager.instance.masterVolume * 10;
            options[1].value = AudioManager.instance.bgmVolume * 10;
            options[2].value = AudioManager.instance.sfxVolume * 10;
            options[3].value = AudioManager.instance.ambienceVolume * 10;
        }
        UpdateDisplay();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (options[selectedIndex].type == OptionType.Value)
            {
                options[selectedIndex].value = Mathf.Max(0, options[selectedIndex].value - 1);
                UpdateDisplay();
                UpdateVolume();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (options[selectedIndex].type == OptionType.Value)
            {
                options[selectedIndex].value = Mathf.Min(10, options[selectedIndex].value + 1);
                UpdateDisplay();
                UpdateVolume();
            }
        }
    }

    private void UpdateVolume()
    {
        float value = options[selectedIndex].value / 10f;
 
        switch (selectedIndex)
        {
            case 0:
                AudioManager.instance.masterVolume = value;
                break;
            case 1:
                AudioManager.instance.bgmVolume = value;
                break;
            case 2:
                AudioManager.instance.sfxVolume = value;
                break;
            case 3:
                AudioManager.instance.ambienceVolume = value;
                break;
            default:
                Debug.LogError("Bro how did u selected other things wtf");
                break;
        }
    }

    protected override void OnPressBackButton()
    {
        SaveManager.instance.SaveSetting();
        UIManager.instance.ToggleMainMenuSettingUI(true);
        UIManager.instance.ToggleMainMenuAudioSettingUI(false);
    }
}