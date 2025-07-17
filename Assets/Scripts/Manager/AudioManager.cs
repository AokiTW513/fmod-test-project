using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float bgmVolume = 1;
    [Range(0, 1)]
    public float ambienceVolume = 1;
    [Range(0, 1)]
    public float sfxVolume = 1;

    private Bus masterBus;
    private Bus bgmBus;
    private Bus ambienceBus;
    private Bus sfxBus;

    public static AudioManager instance { get; private set; }
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> studioEventEmitters;
    private EventInstance ambienceEventInstance;
    private EventInstance bgmEventInstance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found another AudioManager in this scene.");
            return;
        }

        instance = this;

        eventInstances = new List<EventInstance>();
        studioEventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        bgmBus = RuntimeManager.GetBus("bus:/BGM");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    public void LoadVolumeData()
    {
        //調這裡的音量
        masterVolume = SaveManager.instance.currentSettingData.setting.audio.volume.masterVolume;
        bgmVolume = SaveManager.instance.currentSettingData.setting.audio.volume.bgmVolume;
        sfxVolume = SaveManager.instance.currentSettingData.setting.audio.volume.sfxVolume;
        ambienceVolume = SaveManager.instance.currentSettingData.setting.audio.volume.ambienceVolume;

        //設定FMOD的音量
        masterBus.setVolume(masterVolume);
        bgmBus.setVolume(bgmVolume);
        ambienceBus.setVolume(ambienceVolume);
        sfxBus.setVolume(sfxVolume);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        bgmBus.setVolume(bgmVolume);
        ambienceBus.setVolume(ambienceVolume);
        sfxBus.setVolume(sfxVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void InitializedAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }
    
    public void InitializedBGM(EventReference BGMEventReference)
    {
        bgmEventInstance = CreateInstance(BGMEventReference);
        bgmEventInstance.start();
    }
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        studioEventEmitters.Add(emitter);
        return emitter;
    }

    public void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        foreach (StudioEventEmitter studioEventEmitter in studioEventEmitters)
        {
            studioEventEmitter.Stop();
        }
    }
}