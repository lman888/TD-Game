using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public Text m_finishRoundsText;

    public Text m_currentRound;

    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        m_finishRoundsText.text = "0";
        int m_round = 0;

        yield return new WaitForSeconds(0.7f);

        while (m_round < PlayerStats.m_rounds)
        {
            m_round++;
            m_finishRoundsText.text = m_round.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }

}
