using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    public string key;

    private void Start()
    {
        var tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = LocalizationManager.instance.GetText(key);
    }
}