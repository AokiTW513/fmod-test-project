using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        BGM,
        AMBIENCE,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider slider;
    [SerializeField] private Text volumeText;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                slider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.BGM:
                slider.value = AudioManager.instance.bgmVolume;
                break;
            case VolumeType.AMBIENCE:
                slider.value = AudioManager.instance.ambienceVolume;
                break;
            case VolumeType.SFX:
                slider.value = AudioManager.instance.sfxVolume;
                break;
            default:
                Debug.LogError("Volume Type not supported:" + volumeType);
                break;
        }
    }

    public void ChangeVolume()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = slider.value;
                break;
            case VolumeType.BGM:
                AudioManager.instance.bgmVolume = slider.value;
                break;
            case VolumeType.AMBIENCE:
                AudioManager.instance.ambienceVolume = slider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.sfxVolume = slider.value;
                break;
            default:
                Debug.LogError("Volume Type not supported:" + volumeType);
                break;
        }
    }
}