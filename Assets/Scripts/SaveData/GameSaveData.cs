using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public PlayerData player = new PlayerData();
    public MapData map = new MapData();
}

[Serializable]
public class PlayerData
{
    public string playerName = "Player";
    public Vector3 playerPosition;
}

[Serializable]
public class MapData
{
    public int coin = 0;
    public List<string> collectedItemIDs = new List<string>();
}