using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Record : MonoBehaviour
{
    public Text DogTimeLabel;
    public Text CatTimeLabel;
    public Text PigTimeLabel;
    public Text AvgTimeLabel;
    public Text UpdateTimeLabel;
    public Text IndexLabel;
    public void SetData(int index, float DogTime, float CatTime, float PigTime, long UpdateTime)
    {
        DogTimeLabel.text = DogTime.ToString("0.##") + "s";
        CatTimeLabel.text = CatTime.ToString("0.##") + "s";
        PigTimeLabel.text = PigTime.ToString("0.##") + "s";

        float AvgTime = (DogTime + CatTime + PigTime) / 3f;
        AvgTimeLabel.text = AvgTime.ToString("0.##") + "s";

        DateTime dt = new DateTime(UpdateTime);
        UpdateTimeLabel.text = dt.ToString("dddd, dd MMMM yyyy\n HH:mm:ss");

        IndexLabel.text = "No." + index;

    }
}
