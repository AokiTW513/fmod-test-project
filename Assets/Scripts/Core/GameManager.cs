using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public GameObject playerObject { get; private set; }

    public bool isPause { get; private set; }

    public int coin { get; private set; } = 0;

    public bool isFullScreen;
    public int resolutionWidth;
    public int resolutionHeight;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found another GameManager in this scene.");
            return;
        }

        instance = this;

        playerObject = GameObject.FindWithTag("Player");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        SaveManager.instance.LoadSetting();
        if (SaveManager.instance.CheckSettingSaveData())
        {
            AudioManager.instance.LoadVolumeData();
            ApplyResolution(SaveManager.instance.currentSettingData.setting.graph.resolutionWidth, SaveManager.instance.currentSettingData.setting.graph.resolutionHeight, SaveManager.instance.currentSettingData.setting.graph.isFullScreen);
        }
        else
        {
            SaveManager.instance.SaveSetting();
        }
    }

    public void TogglePause()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1f;
            UIManager.instance.TogglePauseUI(false);
            UIManager.instance.TogglePauseBackGroundUI(false);
        }
        else
        {
            isPause = true;
            Time.timeScale = 0f;
            UIManager.instance.TogglePauseUI(true);
            UIManager.instance.TogglePauseBackGroundUI(true);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioManager.instance.CleanUp();
        InitializedUI();
    }

    private void InitializedUI()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            UIManager.instance.InitializedMainMenu();
        }
        else if (SceneManager.GetActiveScene().name == "Level1")
        {
            isPause = false;
            playerObject = GameObject.FindWithTag("Player");
            AudioManager.instance.InitializedAmbience(FMODEvents.instance.ambience);
            AudioManager.instance.InitializedBGM(FMODEvents.instance.gameBGM);
            UIManager.instance.InitializedLevel();
        }
    }

    public void ApplyResolution(int w, int h, bool toggle)
    {
        resolutionWidth = w;
        resolutionHeight = h;
        isFullScreen = toggle;
        Screen.SetResolution(w, h, toggle);
    }

    public void SaveGameData()
    {
        SaveManager.instance.SaveGame();
    }

    public void LoadGameData()
    {
        SaveManager.instance.LoadGame();
    }

    public void ClearSaveGameData()
    {
        SaveManager.instance.ClearGameSave();
    }

    public void ClearSaveSettingData()
    {
        SaveManager.instance.ClearSettingSave();
    }

    public void AddCoin()
    {
        coin++;
    }
}