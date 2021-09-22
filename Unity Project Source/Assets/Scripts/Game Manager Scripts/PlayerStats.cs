using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /* Only one instance to this variable */
    public static int m_money;
    public int m_startingMoney = 999999;

    public static int m_lives;
    public int m_startLives = 20;

    public static int m_rounds;

    private void Start()
    {
        m_money = m_startingMoney;
        m_lives = m_startLives;

        m_rounds = 0;
    }

}
