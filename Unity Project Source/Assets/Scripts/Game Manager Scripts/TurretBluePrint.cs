using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    /* Standard Turret (level 1) */
    public GameObject m_prefab;
    public int m_cost;

    /* Upgraded version of said turret */
    public GameObject m_upgradedPrefab;
    public int m_upgradeCost;


    /* Turret Sell Amounts */
    public int GetSellAmount()
    {
        return m_cost / 2;
    }

    public int GetUpgradedSellAmount()
    {
        return m_cost + m_upgradeCost / 2;
    }
}
