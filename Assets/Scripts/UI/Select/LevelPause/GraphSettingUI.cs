using UnityEngine;

public class GraphSettingUI : SelectUIBase
{
    private ResolutionOption[] resolutions = new ResolutionOption[]
    {
        new ResolutionOption { width = 1280, height = 720 },
        new ResolutionOption { width = 1600, height = 900 },
        new ResolutionOption { width = 1920, height = 1080 },
        new ResolutionOption { width = 2560, height = 1440 },
        new ResolutionOption { width = 3840, height = 2160 }
    };

    private int currentIndex = 0;

    private bool toggleFullScreen;
    
    private void OnEnable()
    {
        options[2].cantChoose = true;
        
        toggleFullScreen = GameManager.instance.isFullScreen;

        UpdateFullScreen();

        UpdateUI();

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                currentIndex = i;
                break;
            }
        }
        UpdateDisplay();
    }

    void UpdateUI()
    {
        options[1].valueText.text = $"{resolutions[currentIndex].width}X{resolutions[currentIndex].height}";
        options[2].cantChoose = false;
        UpdateDisplay();
    }

    void UpdateFullScreen()
    {
        if (toggleFullScreen)
        {
            options[0].valueText.text = "Yes";
        }
        else
        {
            options[0].valueText.text = "No";
        }
        options[2].cantChoose = false;
        UpdateDisplay();
    }

    void PreviousResolution()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = resolutions.Length - 1;
        UpdateUI();
    }

    void NextResolution()
    {
        currentIndex++;
        if (currentIndex >= resolutions.Length) currentIndex = 0;
        UpdateUI();
    }

    void ApplyResolution()
    {
        GameManager.instance.ApplyResolution(resolutions[currentIndex].width, resolutions[currentIndex].height, toggleFullScreen);
        options[2].cantChoose = true;
    }

    protected override void Update()
    {
        base.Update();
        switch (selectedIndex)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (!toggleFullScreen)
                    {
                        toggleFullScreen = true;
                        UpdateFullScreen();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (toggleFullScreen)
                    {
                        toggleFullScreen = false;
                        UpdateFullScreen();
                    }
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    NextResolution();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    PreviousResolution();
                }
                break;
        }
    }

    protected override void OnConfirmPauseUI()
    {
        switch (selectedIndex)
        {
            case 2:
                ApplyResolution();
                break;
        }
    }

    protected override void OnPressBackButton()
    {
        SaveManager.instance.SaveSetting();
        UIManager.instance.ToggleSettingUI(true);
        UIManager.instance.ToggleGraphSettingUI(false);
    }
}