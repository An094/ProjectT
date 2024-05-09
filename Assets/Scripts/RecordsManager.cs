using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordsManager : MonoBehaviour
{
    [SerializeField] private TabGroup TabGroup;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject Level1RecordTemplate;
    [SerializeField] private GameObject Level2RecordTemplate;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Age;

    private void OnEnable()
    {
        TabGroup.OnTabChanged += OnTabChanged;
    }

    private void OnDisable()
    {
        TabGroup.OnTabChanged -= OnTabChanged;
    }

    private void Start()
    {
        OnTabChanged(1);

        GameData gameData = DataPersistenceManager.Instance.GetData();
        Name.text = DataPersistenceManager.SelectedProfileId;
        Age.text = gameData.Age.ToString();
    }

    private void OnTabChanged(int index)
    {
        //remove old objects
        for (var i = Content.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
        }

        GameData gameData = DataPersistenceManager.Instance.GetData();


        int IndexOfRecord1 = 1;
        int IndexOfRecord2 = 1;
        foreach (var record in gameData.Records)
        {
            if (record.Level == index)
            {
                GameObject RecordItem;
                
                //TODO: code smell!!!! Fix this plz
                if(index == 1)
                {
                    RecordItem = Instantiate(Level1RecordTemplate);
                    RecordItem.transform.SetParent(Content.transform);
                    Level1Record lv1Record = RecordItem.GetComponent<Level1Record>();

                    float MonkeyTime = 0.0f;
                    float RabbitTime = 0.0f;
                    float PorcupineTime = 0.0f;
                    long UpdateTime = record.LastUpdate;
                    if (lv1Record != null)
                    {
                        LevelData MonkeyData = record.LevelData.FirstOrDefault(p => p.Name == "Monkey");
                        LevelData RabbitData = record.LevelData.FirstOrDefault(p => p.Name == "Rabbit");
                        LevelData PorcupineData = record.LevelData.FirstOrDefault(p => p.Name == "Porcupine");

                        if(MonkeyData == null && RabbitData == null && PorcupineData == null)
                        {
                            Destroy(RecordItem);
                            continue;
                        }

                        if(MonkeyData != null)
                        {
                            MonkeyTime = MonkeyData.Duration;
                        }

                        if(RabbitData != null)
                        {
                            RabbitTime = RabbitData.Duration;
                        }

                        if (PorcupineData != null)
                        {
                            PorcupineTime = PorcupineData.Duration;
                        }

                        lv1Record.SetData(IndexOfRecord1, MonkeyTime, RabbitTime, PorcupineTime, UpdateTime);
                        IndexOfRecord1++;
                    }
                }
                else
                {
                    RecordItem = Instantiate(Level2RecordTemplate);

                    RecordItem.transform.SetParent(Content.transform);
                    Level2Record lv1Record = RecordItem.GetComponent<Level2Record>();

                    float DogTime = 0.0f;
                    float CatTime = 0.0f;
                    float PigTime = 0.0f;
                    long UpateTime = record.LastUpdate;

                    if (lv1Record != null)
                    {
                        LevelData DogData = record.LevelData.FirstOrDefault(p => p.Name == "Dog");
                        LevelData CatData = record.LevelData.FirstOrDefault(p => p.Name == "Cat");
                        LevelData PigData = record.LevelData.FirstOrDefault(p => p.Name == "Pig");

                        if(DogData == null && CatData == null && PigData == null)
                        {
                            Destroy(RecordItem);
                            continue;
                        }

                        if (DogData != null)
                        {
                            DogTime = DogData.Duration;
                        }

                        if (CatData != null)
                        {
                            CatTime = CatData.Duration;
                        }

                        if(PigData != null)
                        {
                            PigTime = PigData.Duration;
                        }

                        lv1Record.SetData(IndexOfRecord2, DogTime, CatTime, PigTime, UpateTime);
                        IndexOfRecord2++;
                    }
                }
                
                
            }
        }
    }

    public void ExitMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
