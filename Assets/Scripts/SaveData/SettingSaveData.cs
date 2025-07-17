using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingSaveData
{
    public Setting setting = new Setting();
}

[Serializable]
public class Setting
{
    public Audio audio = new Audio();
    public Graph graph = new Graph();
}

[Serializable]
public class Graph
{
    public bool isFullScreen = true;
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
}

[Serializable]
public class Audio
{
    public Volume volume = new Volume();
}

[Serializable]
public class Volume
{
    public float masterVolume = 1f;
    public float bgmVolume = 1f;
    public float sfxVolume = 1f;
    public float ambienceVolume = 1f;
}