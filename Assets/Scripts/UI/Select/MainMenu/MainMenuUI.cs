using Unity.VisualScripting;
using UnityEngine;

public class MainMenuUI : SelectUIBase
{
    public void OnEnable()
    {
        if (SaveManager.instance != null)
        {
            if (!SaveManager.instance.CheckGameSaveData())
            {
                options[1].cantChoose = true; //options[1]是載入遊戲，這裡是指如果沒有存檔，則無法載入遊戲，並把他改成不能選擇。
            }
        }
        UpdateDisplay();
    }

    protected override void OnConfirmPauseUI()
    {
        switch (selectedIndex)
        {
            case 0:
                GameManager.instance.ClearSaveGameData();
                GameManager.instance.LoadScene("Level1");
                break;
            case 1:
                GameManager.instance.LoadGameData();
                GameManager.instance.LoadScene("Level1");
                break;
            case 2:
                GameManager.instance.ClearSaveGameData();
                options[1].cantChoose = true; //options[1]是載入遊戲，這裡是指如果沒有存檔，則無法載入遊戲，並把他改成不能選擇。
                UpdateDisplay();
                break;
            case 3:
                UIManager.instance.ToggleMainMenuSettingUI(true);
                UIManager.instance.ToggleMainMenuUI(false);
                break;
            case 4:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                break;
        }
    }
}