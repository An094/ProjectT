using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class MatchGameManager : MonoBehaviour
{
    [SerializeField] private List<Slot> slots;

    [SerializeField] private GameObject RecordPopup;
    private RectTransform RecordPopupPanel;

    [SerializeField] private CanvasGroup CanvasGround;//dark background
    private Vector2 DefaultStartPosition = new Vector2(0.0f, 2000f);
    private int numberMatchedItems = 0;
    private void OnEnable()
    {
        foreach(var slot in slots)
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
        if(numberMatchedItems == slots.Count)
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
}