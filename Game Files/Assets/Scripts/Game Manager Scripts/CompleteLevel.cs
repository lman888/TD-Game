using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public string m_menuSceneName = "MainMenu";
    public SceneFader m_sceneFader;

    public string m_nextlevel = "Level02";
    public int m_levelToUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("m_levelReached", m_levelToUnlock);
        m_sceneFader.FadeTo(m_nextlevel);
    }

    public void Menu()
    {
        m_sceneFader.FadeTo(m_menuSceneName);
    }
}
