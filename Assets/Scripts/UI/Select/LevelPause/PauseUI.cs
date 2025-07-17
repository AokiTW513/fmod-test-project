using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : SelectUIBase
{
    protected override void OnConfirmPauseUI()
    {
        switch (selectedIndex)
        {
            case 0:
                UIManager.instance.ToggleSettingUI(true);
                UIManager.instance.TogglePauseUI(false);
                break;
            case 1:
                GameManager.instance.SaveGameData();
                GameManager.instance.TogglePause();
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    protected override void OnPressBackButton()
    {
        GameManager.instance.TogglePause();
    }
}