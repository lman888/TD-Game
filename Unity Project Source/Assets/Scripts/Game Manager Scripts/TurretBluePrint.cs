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
}
