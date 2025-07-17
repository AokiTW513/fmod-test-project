using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    //In Level Pause
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject PauseBackGround;
    [SerializeField] private GameObject SettingUI;
    [SerializeField] private GameObject AudioSettingUI;
    [SerializeField] private GameObject GraphSettingUI;

    //MainMenu
    [SerializeField] private GameObject MainMenuUI;
    [SerializeField] private GameObject MainMenuSettingUI;
    [SerializeField] private GameObject MainMenuAudioSettingUI;
    [SerializeField] private GameObject MainMenuGraphSettingUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found another UIManager in this scene.");
            return;
        }

        instance = this;
    }

    //Initialized MainMenu
    public void InitializedMainMenu()
    {
        MainMenuUI = GameObject.Find("MainMenuUI");
        MainMenuSettingUI = GameObject.Find("SettingUI");
        MainMenuAudioSettingUI = GameObject.Find("AudioSettingUI");
        MainMenuGraphSettingUI = GameObject.Find("GraphSettingUI");
        ToggleMainMenuUI(false);
        ToggleMainMenuUI(true);
        ToggleMainMenuSettingUI(false);
        ToggleMainMenuAudioSettingUI(false);
        ToggleMainMenuGraphSettingUI(false);
    }

    //Initialized Level
    public void InitializedLevel()
    {
        PauseUI = GameObject.Find("PauseUI");
        PauseBackGround = GameObject.Find("PauseBackGround");
        SettingUI = GameObject.Find("SettingUI");
        AudioSettingUI = GameObject.Find("AudioSettingUI");
        GraphSettingUI = GameObject.Find("GraphSettingUI");
        TogglePauseUI(false);
        TogglePauseBackGroundUI(false);
        ToggleSettingUI(false);
        ToggleAudioSettingUI(false);
        ToggleGraphSettingUI(false);
    }

    //Pause
    public void TogglePauseUI(bool toggle)
    {
        PauseUI.SetActive(toggle);
    }

    public void TogglePauseBackGroundUI(bool toggle)
    {
        PauseBackGround.SetActive(toggle);
    }

    public void ToggleSettingUI(bool toggle)
    {
        SettingUI.SetActive(toggle);
    }

    public void ToggleAudioSettingUI(bool toggle)
    {
        AudioSettingUI.SetActive(toggle);
    }

    public void ToggleGraphSettingUI(bool toggle)
    {
        GraphSettingUI.SetActive(toggle);
    }

    //MainMenu
    public void ToggleMainMenuUI(bool toggle)
    {
        MainMenuUI.SetActive(toggle);
    }    

    public void ToggleMainMenuSettingUI(bool toggle)
    {
        MainMenuSettingUI.SetActive(toggle);
    }

    public void ToggleMainMenuAudioSettingUI(bool toggle)
    {
        MainMenuAudioSettingUI.SetActive(toggle);
    }

    public void ToggleMainMenuGraphSettingUI(bool toggle)
    {
        MainMenuGraphSettingUI.SetActive(toggle);
    }
}