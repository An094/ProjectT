using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecordLabelInfo
{
    public string Name;
    public TMPro.TMP_Text Recordlabel;
}
public class ResultMatchGamePopup : MonoBehaviour
{
    [SerializeField] private List<RecordLabelInfo> recordLabelInfos;
    [SerializeField] private TMPro.TMP_Text AvgTimeLabel;

    private void Start()
    {
        float TotalTime = 0f;
        foreach (RecordLabelInfo info in recordLabelInfos)
        {
            float duration = TimeManager.instance.GetDuration(info.Name);
            TotalTime += duration;
            info.Recordlabel.text = duration.ToString("0.##") + " s";
        }

        float avgTime = TotalTime / recordLabelInfos.Count;

        AvgTimeLabel.text = "AVG: " + avgTime.ToString("0.##") + " s";
    }

    private void Restart()
    {
        MatchGameManager.Instance.Restart();
    }

    private void PlayNextLevel()
    {
        MatchGameManager.Instance.PlayNextLevel();
    }
}
