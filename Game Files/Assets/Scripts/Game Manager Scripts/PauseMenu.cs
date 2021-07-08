using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject m_UI;

    public string m_menuSceneName = "MainScene";

    public SceneFader m_fader;

    // Update is called once per frame
    void Update()
    {
        /* If we press Escape or P, pause the game */
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            /* Pause the Game */
            Toggle();
        }
    }

    public void Toggle()
    {
        m_UI.SetActive(!m_UI.activeSelf);

        if (m_UI.activeSelf)
        {
            /* Pauses the Game */
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Retry()
    {
        /* Reloads the current Scene (also make sure to unfreeze time) */
        Toggle();
        m_fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        m_fader.FadeTo(m_menuSceneName);
    }

}
