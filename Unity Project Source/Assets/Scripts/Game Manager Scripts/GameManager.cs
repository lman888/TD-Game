using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public  static bool m_gameIsOver = false;
    public GameObject m_gameOverUI;
    public GameObject m_completeLevelUI;

    private void Start()
    {
        m_gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_gameIsOver)
            return;

        if (PlayerStats.m_lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        m_gameIsOver = true;
        m_gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        m_gameIsOver = true;
        m_completeLevelUI.SetActive(true);
    }

}
