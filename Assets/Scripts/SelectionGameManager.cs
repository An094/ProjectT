using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

public class SelectionGameManager : MonoBehaviour
{
    [SerializeField] List<CustomButton> customButtons;
    [SerializeField] List<QnASet> SampleQnA;

    private List<int> playedSoundIndex;
    private List<int> PlayedSetIndex;
    private string currentAnimal;

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
    }

    public void OnCustomButtonClicked(CustomButton InButton)
    {
        if(InButton.GetAnimalData() != currentAnimal)
        {
            InButton.SetResult(false);
        }
        else
        {
            InButton.SetResult(true);
            StartCoroutine(CorrectAnswer());   
        }
    }

    IEnumerator CorrectAnswer()
    {
        AudioManager.Instance.PlaySFX("Votay");
        yield return new WaitForSeconds(3f);
        Setup();
    }
}
