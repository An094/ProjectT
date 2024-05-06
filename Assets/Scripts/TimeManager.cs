using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeManager : MonoBehaviour, IDataPersistence
{
    public static TimeManager instance;

    public class Interval
    {
        public float StartTime;
        public float EndTime;
    }

    private Dictionary<string, Interval> timers = new Dictionary<string, Interval>();
    private void Awake()
    {
        if(instance)
        {
            Destroy( instance );
        }
        else
        {
            instance = this;
        }
    }

    public void StartTimer(string name)
    {
        if (timers.ContainsKey(name)) return;

        Interval interval = new Interval();
        interval.StartTime = Time.time;
        interval.EndTime = Time.time;

        timers[name] = interval;

        Debug.Log("Start timer for " + name);
    }

    public void StopTimer(string name)
    {
        if(timers.ContainsKey(name))
        {
            timers[name].EndTime = Time.time;
            Debug.Log("Stop timer for " + name);
        }
    }

    public float GetDuration(string name)
    {
        if(timers.ContainsKey(name))
        {
            return timers[name].EndTime - timers[name].StartTime;
        }
        return 0;
    }

    public void LoadData(GameData data)
    {
        //foreach (var timer in data.LevelData)
        //{
        //    timers[timer.Name].StartTime = timer.StartTime;
        //    timers[timer.Name].EndTime = timer.EndTime;
        //}
    }

    public void SaveData(ref GameData data)
    {
        int index = 0;
        float duration = 0.0f;
        foreach (var timer in timers)
        {
            duration = timer.Value.EndTime - timer.Value.StartTime;
            if (duration != 0f)
            {
                LevelData level2Data = new LevelData();
                level2Data.Name = timer.Key;
                //level2Data.StartTime = timer.Value.StartTime;
                //level2Data.EndTime = timer.Value.EndTime;
                level2Data.Duration = duration;
                data.GetCurrentRecords().LevelData.Add(level2Data);
                index++;
            }
           
        }
    }
}
