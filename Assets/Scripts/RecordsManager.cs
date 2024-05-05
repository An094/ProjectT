using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsManager : MonoBehaviour
{
    [SerializeField] private TabGroup TabGroup;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject Level1RecordTemplate;

    private void OnEnable()
    {
        TabGroup.OnTabChanged += OnTabChanged;
    }

    private void OnDisable()
    {
        TabGroup.OnTabChanged -= OnTabChanged;
    }

    private void OnTabChanged(int index)
    {
        //remove old objects
        for (var i = Content.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
        }

        GameData gameData = DataPersistenceManager.Instance.GetData();

        //for(int)
    }
}
