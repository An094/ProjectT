using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class MatchGameManager : MonoBehaviour, IDataPersistence
{
    public static MatchGameManager Instance { get; private set; }

    [SerializeField] private List<Slot> slots;

    [SerializeField] private GameObject RecordPopup;
    private RectTransform RecordPopupPanel;

    [SerializeField] private CanvasGroup CanvasGround;//dark background

    [Header("Scene")]
    [SerializeField] private string NextLevel;
    private Vector2 DefaultStartPosition = new Vector2(0.0f, 2000f);
    private int numberMatchedItems = 0;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        foreach (var slot in slots)
        {
            slot.OnMatched += OnItemMatch;
        }
    }

    private void OnDisable()
    {
        foreach (var slot in slots)
        {
            slot.OnMatched -= OnItemMatch;
        }
    }

    private void Start()
    {
        RecordPopupPanel = RecordPopup.GetComponent<RectTransform>();
        RecordPopupPanel.anchoredPosition = DefaultStartPosition;
    }

    private void OnItemMatch()
    {
        numberMatchedItems++;
        Debug.Log("numberMatchedItems" + numberMatchedItems);
        if (numberMatchedItems == slots.Count)
        {
            //Show resultPopup
            //RecordPopup.SetActive(true);
            OpenRecordPopup();
        }
    }

    public void OpenRecordPopup()
    {
        RecordPopup.SetActive(true);
        RecordPopupIntro();
    }

    public async void CloseRecordPopup()
    {
        await RecordPopupOuttro();
        // RecordPopupPanel.anchoredPosition = DefaultStartPosition;
        //Popups.SetActive(false);
        RecordPopup.SetActive(false);
    }

    private void RecordPopupIntro()
    {
        CanvasGround.DOFade(1, 1.0f).SetUpdate(true);
        //MusicSettingPopupPanel.DOAnchorPosY(0, duration).SetUpdate(true);
        RecordPopupPanel.DOAnchorPosY(0, 1f).SetEase(Ease.OutQuint);
    }

    private async Task RecordPopupOuttro()
    {
        CanvasGround.DOFade(0, 1f).SetUpdate(true);
        //await MusicSettingPopupPanel.DOAnchorPosY(DefaultEndPosition.y, duration).SetUpdate(true).AsyncWaitForCompletion();
        await RecordPopupPanel.DOAnchorPosY(1000f, 1f).SetEase(Ease.InOutQuint).AsyncWaitForCompletion();
    }

    public void Restart()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayNextLevel()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene(NextLevel);
    }

    public void GotoHome()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        data.GetCurrentRecords().Level = 1;
        data.GetCurrentRecords().LastUpdate = DateTime.Now.Ticks;
    }
}
