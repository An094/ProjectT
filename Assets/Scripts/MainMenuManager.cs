using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ExitAppliaction()
    {
        Application.Quit();
    }
}
