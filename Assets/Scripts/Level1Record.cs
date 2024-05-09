using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Record : MonoBehaviour
{
    public Text MonkeyTimeLabel;
    public Text RabbitTimeLabel;
    public Text PorcupineTimeLabel;
    public Text AvgTimeLabel;
    public Text UpdateTimeLabel;
    public Text IndexLabel;
    public void SetData(int index, float MonkeyTime, float RabbitTime, float PorcupineTime, long UpdateTime)
    {
        MonkeyTimeLabel.text = MonkeyTime.ToString("0.##") + "s";
        RabbitTimeLabel.text = RabbitTime.ToString("0.##") + "s";
        PorcupineTimeLabel.text = PorcupineTime.ToString("0.##") + "s";

        float AvgTime = (MonkeyTime + RabbitTime + PorcupineTime) / 3f;
        AvgTimeLabel.text = AvgTime.ToString("0.##") + "s";

        DateTime dt = new DateTime(UpdateTime);
        UpdateTimeLabel.text = dt.ToString("dddd, dd MMMM yyyy\n HH:mm:ss");

        IndexLabel.text = "No." + index;
    }
}
