using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    

    public string m_menuScene = "MainMenu";

    public SceneFader m_fader;

    public void Retry()
    {
        m_fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        m_fader.FadeTo(m_menuScene);
    }

}
