using UnityEngine;
using TMPro;

public abstract class SelectUIBase : MonoBehaviour
{
    public enum OptionType
    {
        Value,
        Button,
        Switch
    }

    [System.Serializable]
    public class SettingOption
    {
        public OptionType type;
        public TMP_Text label;
        public bool cantChoose = false;
        public TMP_Text valueText;
        [Range(0, 10)] public float value;
    }

    public SettingOption[] options;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;
    public Color cantChooseColor = Color.gray;
    protected int selectedIndex = 0;

    protected virtual void Start()
    {
        UpdateDisplay();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
            
            if (options[selectedIndex].cantChoose)
            { 
                selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
            }

            UpdateDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % options.Length;

            if (options[selectedIndex].cantChoose)
            {
                selectedIndex = (selectedIndex + 1 + options.Length) % options.Length;
            }

            UpdateDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (options[selectedIndex].type == OptionType.Button)
            {
                OnConfirmPauseUI();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
        {
            OnPressBackButton();
        }
    }

    protected virtual void OnConfirmPauseUI()
    { 

    }

    protected virtual void OnPressBackButton()
    {
        
    }

    protected virtual void UpdateDisplay()
    {
        for (int i = 0; i < options.Length; i++)
        {
            var opt = options[i];
            if (opt.cantChoose)
            {
                opt.label.color = cantChooseColor;
                if (opt.valueText != null && opt.type == OptionType.Value)
                {
                    opt.valueText.color = cantChooseColor;
                    opt.valueText.text = $"{opt.value}";
                }
            }
            else
            {
                opt.label.color = (i == selectedIndex) ? selectedColor : normalColor;
                //如果需要顯示數值文字的話才執行
                if (opt.valueText != null)
                {
                    opt.valueText.color = (i == selectedIndex) ? selectedColor : normalColor;
                    if (opt.type == OptionType.Value)
                    {
                        opt.valueText.text = $"{opt.value}";
                    }
                }
            }
        }
    }
}