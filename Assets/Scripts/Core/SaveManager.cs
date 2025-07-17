using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public string gameSaveFileName = "save.mygames";
    public string settingSaveFileName = "setting.mygames";
    private string gameSavePath;
    private string settingSavePath;

    public GameSaveData currentGameData;
    public SettingSaveData currentSettingData;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found another SaveManager in this scene.");
            return;
        }

        instance = this;

        string folderPath = Path.Combine(Application.dataPath, "Saves");

        //如果沒資料夾，就幫他建一個
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        gameSavePath = Path.Combine(folderPath, gameSaveFileName);
        settingSavePath = Path.Combine(folderPath, settingSaveFileName);
    }

    public void SaveGame()
    {
        //Player
        currentGameData.player.playerPosition = GameManager.instance.playerObject.transform.position;
        //Map
        currentGameData.map.coin = GameManager.instance.coin;

        string json = JsonUtility.ToJson(currentGameData, true);
        File.WriteAllText(gameSavePath, json);
    }

    public void SaveSetting()
    {
        //AudioSetting
        currentSettingData.setting.audio.volume.masterVolume = AudioManager.instance.masterVolume;
        currentSettingData.setting.audio.volume.bgmVolume = AudioManager.instance.bgmVolume;
        currentSettingData.setting.audio.volume.sfxVolume = AudioManager.instance.sfxVolume;
        currentSettingData.setting.audio.volume.ambienceVolume = AudioManager.instance.ambienceVolume;

        currentSettingData.setting.graph.isFullScreen = GameManager.instance.isFullScreen;
        currentSettingData.setting.graph.resolutionWidth = GameManager.instance.resolutionWidth;
        currentSettingData.setting.graph.resolutionHeight = GameManager.instance.resolutionHeight;

        string json = JsonUtility.ToJson(currentSettingData, true);
        File.WriteAllText(settingSavePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(gameSavePath))
        {
            string json = File.ReadAllText(gameSavePath);
            currentGameData = JsonUtility.FromJson<GameSaveData>(json);
        }
        else
        {
            currentGameData = new GameSaveData();
        }
    }

    public void LoadSetting()
    {
        if (File.Exists(settingSavePath))
        {
            string json = File.ReadAllText(settingSavePath);
            currentSettingData = JsonUtility.FromJson<SettingSaveData>(json);
        }
        else
        {
            currentSettingData = new SettingSaveData();
        }
    }

    public bool CheckGameSaveData()
    {
        if (File.Exists(gameSavePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool CheckSettingSaveData()
    {
        if (File.Exists(settingSavePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsCollected(string id)
    {
        return currentGameData.map.collectedItemIDs.Contains(id);
    }

    public void MarkCollected(string id)
    {
        if (!currentGameData.map.collectedItemIDs.Contains(id))
        {
            currentGameData.map.collectedItemIDs.Add(id);
        }
    }

    public void LoadDefaultSetting()
    {
        currentSettingData = new SettingSaveData();
        GameManager.instance.ApplyResolution(currentSettingData.setting.graph.resolutionWidth, currentSettingData.setting.graph.resolutionHeight, currentSettingData.setting.graph.isFullScreen);
        AudioManager.instance.LoadVolumeData();
        SaveSetting();
    }

    public void ClearGameSave()
    {
        if (File.Exists(gameSavePath))
        {
            File.Delete(gameSavePath);
        }

        currentGameData = new GameSaveData();
    }

    public void ClearSettingSave()
    {
        if (File.Exists(settingSavePath))
        {
            File.Delete(settingSavePath);
        }

        currentSettingData = new SettingSaveData();
    }
}