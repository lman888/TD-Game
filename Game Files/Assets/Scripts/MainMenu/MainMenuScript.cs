using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string m_levelToLoad = "MainLevel";

    public SceneFader m_sceneFader;

    public void Play()
    {
        m_sceneFader.FadeTo(m_levelToLoad);
    }

    public void Quit()
    {
        Debug.Log("Exiting....");
        Application.Quit();
    }
}
