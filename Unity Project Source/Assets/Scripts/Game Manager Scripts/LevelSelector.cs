using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public SceneFader m_fader;

    public Button[] m_levelButtons;

    private void Start()
    {
        int m_levelReached = PlayerPrefs.GetInt("m_levelReached", 1);

        for (int i = 0; i < m_levelButtons.Length; i++)
        {
            if (i + 1 > m_levelReached)
                m_levelButtons[i].interactable = false;
        }
    }

    public void Select(string a_levelName)
    {
        m_fader.FadeTo(a_levelName);
    }
}
