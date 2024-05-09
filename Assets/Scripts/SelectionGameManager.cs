using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class QnASet
{
    public List<string> Answer;
    public int CorrectAnswerIndex;

    public string GetCorrectAnswerIndex()
    {
        return Answer[CorrectAnswerIndex];
    }
}

public class SelectionGameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] List<CustomButton> customButtons;
    [SerializeField] List<QnASet> SampleQnA;

    private List<int> playedSoundIndex;
    private List<int> PlayedSetIndex;
    private string currentAnimal;

    private int currentRound = 0;
    private Vector2 DefaultStartPosition = new Vector2(0.0f, 2000f);

    //
    [Header("Popup")]
    [SerializeField] private GameObject RecordPopup;
    private RectTransform RecordPopupPanel;

    [SerializeField] private CanvasGroup CanvasGround;//dark background

    private bool PickRandomSoundSfx(ref string OutName)
    {
        while (playedSoundIndex.Count < AudioManager.Instance.sfxSounds.Length)
        {
            int newIndex = UnityEngine.Random.Range(0, AudioManager.Instance.sfxSounds.Length);

            if (playedSoundIndex.Contains(newIndex))
            {
                continue;
            }
            else
            {
                playedSoundIndex.Add(newIndex);
                OutName = AudioManager.Instance.sfxSounds[newIndex].m_name;
                return true;
            }
        }
        return false;
    }

    private void OnEnable()
    {
        foreach (CustomButton button in customButtons)
        {
            button.OnCustomButtonClickEvent += OnCustomButtonClicked;
        }
    }

    private void OnDisable()
    {
        foreach (CustomButton button in customButtons)
        {
            button.OnCustomButtonClickEvent -= OnCustomButtonClicked;
        }
    }

    private void Start()
    {
        playedSoundIndex = new List<int>();

        PlayedSetIndex = new List<int>();
        RecordPopupPanel = RecordPopup.GetComponent<RectTransform>();

        Setup();

    }

    private void Setup()
    {
        int QnASetIndex = PickRadomSet();

        if (QnASetIndex == -1) return;

        QnASet currentQnASet = SampleQnA[QnASetIndex];
        currentAnimal = currentQnASet.GetCorrectAnswerIndex();

        for (int i = 0; i < customButtons.Count; ++i)
        {
            Sprite sprite = Resources.LoadAll<Sprite>(currentQnASet.Answer[i]).FirstOrDefault();
            customButtons[i].SetIcon(sprite);
            customButtons[i].SetAnimalData(currentQnASet.Answer[i]);
            customButtons[i].SetDefautState();
        }

        RecordPopupPanel.anchoredPosition = DefaultStartPosition;
        currentRound++;
    }

    private int PickRadomSet()
    {
        while (PlayedSetIndex.Count < SampleQnA.Count)
        {
            int newIndex = UnityEngine.Random.Range(0, SampleQnA.Count);

            if (PlayedSetIndex.Contains(newIndex))
            {
                continue;
            }
            else
            {
                PlayedSetIndex.Add(newIndex);
            
                return newIndex;
            }
        }
        return -1;
    }

    public void PlayerSound()
    {
        AudioManager.Instance.PlaySFX(currentAnimal);
        //TimeManager.instance.StartTimer(currentRound.ToString());
        TimeManager.instance.StartTimer(currentAnimal);
    }

    public void OnCustomButtonClicked(CustomButton InButton)
    {
        if(InButton.GetAnimalData() != currentAnimal)
        {
            InButton.SetResult(false);
        }
        else
        {
            //TimeManager.instance.StopTimer(currentRound.ToString());
            TimeManager.instance.StopTimer(currentAnimal);
            InButton.SetResult(true);
            StartCoroutine(CorrectAnswer());   
        }
    }

    IEnumerator CorrectAnswer()
    {
        AudioManager.Instance.PlaySFX("Votay");
        yield return new WaitForSeconds(4f);
        
        if(currentRound == 3)
        {
            OpenRecordPopup();
        }
        else
        {
            Setup();
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

    public void LoadData(GameData data)
    {
       
    }

    public void SaveData(ref GameData data)
    {
        data.GetCurrentRecords().Level = 2;
        data.GetCurrentRecords().LastUpdate = DateTime.Now.Ticks;
    }

    public void GotoHome()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
