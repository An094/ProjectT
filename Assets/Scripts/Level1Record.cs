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
    public void SetData(float MonkeyTime, float RabbitTime, float PorcupineTime)
    {
        MonkeyTimeLabel.text = MonkeyTime.ToString("0.##") + "s";
        RabbitTimeLabel.text = RabbitTime.ToString("0.##") + "s";
        PorcupineTimeLabel.text = PorcupineTime.ToString("0.##") + "s";

        float AvgTime = (MonkeyTime + RabbitTime + PorcupineTime) / 3f;
        AvgTimeLabel.text = AvgTime.ToString("0.##") + "s";

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
