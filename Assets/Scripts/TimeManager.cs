using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
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
}
