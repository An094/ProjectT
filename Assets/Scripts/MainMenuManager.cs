using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    static bool IsPlayedMusic = false;
    [SerializeField] private GameObject ButtonOn;
    [SerializeField] private GameObject ButtonOff;

    private void Start()
    {
        if (!IsPlayedMusic)
        {
            AudioManager.Instance.PlayMusic("ANewDay");
            AudioManager.Instance.MusicVolume(0.05f);
            IsPlayedMusic = true;
        }
        TurnOnMusic();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ExitAppliaction()
    {
        Application.Quit();
    }

    public void TurnOnMusic()
    {
        ButtonOn.SetActive(true);
        ButtonOff.SetActive(false);
        AudioManager.Instance.TurnOnMusic();
    }

    public void TurnOffMusic()
    {
        ButtonOn.SetActive(false);
        ButtonOff.SetActive(true);
        AudioManager.Instance.TurnOffMusic();
    }
}
