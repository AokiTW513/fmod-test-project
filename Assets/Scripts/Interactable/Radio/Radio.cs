using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class Radio : MonoBehaviour
{
    private StudioEventEmitter studioEventEmitter;
    [SerializeField] GameObject InteractionPromptUI;

    private void Start()
    {
        studioEventEmitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.testDistance, this.gameObject);
        studioEventEmitter.Play();
    }

    public void OnSwitch()
    {
        if (studioEventEmitter.IsPlaying())
        {
            studioEventEmitter.Stop();
        }
        else
        {
            studioEventEmitter.Play();
        }
    }

    public void ShowInteractionPromptUI(bool show)
    {
        InteractionPromptUI.SetActive(show);
    }
}