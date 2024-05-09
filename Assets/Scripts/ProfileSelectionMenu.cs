using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileSelectionMenu : MonoBehaviour, IDataPersistence
{
    [SerializeField] TMPro.TMP_InputField InputField;
    [SerializeField] TMPro.TMP_InputField AgeField;
    [SerializeField] ToggleGroup ToggleGroup;

    private string Gender;
    private int Age;
    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        data.Gender = this.Gender;
        data.Age = this.Age;
    }

    public void SelectProfile()
    {
        string ProfileId = InputField.text;
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.Instance.GetAllProfilesGameData();

        //if (profilesGameData.ContainsKey(ProfileId))
        {
            DataPersistenceManager.Instance.ChangeSelectedProfileId(ProfileId);

        }
        //else
        //{
        //    DataPersistenceManager.Instance.NewGame();
        //}
        Toggle toggle = ToggleGroup.GetFirstActiveToggle();
        Gender = toggle.GetComponentInChildren<Text>().text;

        Age = Convert.ToInt32(AgeField.text);

        DataPersistenceManager.Instance.SaveGame();

        SceneManager.LoadScene("MainMenu");
    }
}
