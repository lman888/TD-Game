using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text m_livesText;

    // Update is called once per frame
    void Update()
    {
        m_livesText.text = PlayerStats.m_lives + " LIVES";
    }
}
