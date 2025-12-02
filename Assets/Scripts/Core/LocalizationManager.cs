using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance { get; private set; }

    public string currentLanguage = "en"; // 預設語言
    private Dictionary<string, string> localizedText = new Dictionary<string, string>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found another GameManager in this scene.");
            return;
        }

        instance = this;
        LoadLocalizedText(currentLanguage);
    }

    public void LoadLocalizedText(string languageCode)
    {
        localizedText.Clear();

        TextAsset jsonFile = Resources.Load<TextAsset>($"Localization/{languageCode}");
        if (jsonFile == null)
        {
            Debug.LogError("找不到語言檔：" + languageCode);
            return;
        }

        localizedText = JsonUtility.FromJson<LocalizationData>(jsonFile.text).ToDictionary();
    }

    public string GetText(string key)
    {
        if (localizedText.TryGetValue(key, out string value))
        {
            return value;
        }
        return $"[{key}]";
    }

    [System.Serializable]
    private class LocalizationData
    {
        public LocalizedItem[] items;

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var item in items)
            {
                dict[item.key] = item.value;
            }
            return dict;
        }
    }

    [System.Serializable]
    private class LocalizedItem
    {
        public string key;
        public string value;
    }
}