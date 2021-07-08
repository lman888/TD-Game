using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject m_prefab;
    public int m_cost;

    public GameObject m_upgradedPrefab;
    public int m_upgradeCost;

    public int GetSellAmount()
    {
        return m_cost / 2;
    }
}
