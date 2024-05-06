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
    public void SetData(float DogTime, float CatTime, float PigTime)
    {
        DogTimeLabel.text = DogTime.ToString("0.##") + "s";
        CatTimeLabel.text = CatTime.ToString("0.##") + "s";
        PigTimeLabel.text = PigTime.ToString("0.##") + "s";

        float AvgTime = (DogTime + CatTime + PigTime) / 3f;
        AvgTimeLabel.text = AvgTime.ToString("0.##") + "s";

    }
}
