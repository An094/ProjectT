using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<LevelData> LevelData;

    //the values defined in this constructor will be the default values
    //the game starts with when there's no data to load
    public GameData()
    { 
        this.LevelData = new List<LevelData>();
    }
}

//[System.Serializable]
//public class ObjectData
//{
    
//}

[System.Serializable]
public class LevelData
{
    public string Name;
    public float StartTime;
    public float EndTime;
    public float Duration;

    //the values defined in this constructor will be the default values
    //the game starts with when there's no data to load
    public LevelData()
    {
        this.Name = "none";
        this.StartTime = 0f;
        this.EndTime = 0f;
        this.Duration = 0f;
    }
}
