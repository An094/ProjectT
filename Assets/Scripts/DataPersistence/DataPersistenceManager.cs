using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData GameData;
    private List<IDataPersistence> DataPersistenceObjects;
    private FileDataHandler FileDataHandler;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        Instance = this;
    }

    private void Start()
    {
        this.FileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.DataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.GameData = new GameData();
    }

    public void LoadGame()
    {
        //Load any saved data from a file using the data handler
        this.GameData = FileDataHandler.Load();

        if (this.GameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }

        foreach(IDataPersistence dataPersistenceObj in DataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(GameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref GameData);
        }

        FileDataHandler.Save(GameData);
    }

}
