using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{

    public Text m_moneyText;

    // Update is called once per frame
    void Update()
    {
        m_moneyText.text = "$" + PlayerStats.m_money.ToString();
    }
}
