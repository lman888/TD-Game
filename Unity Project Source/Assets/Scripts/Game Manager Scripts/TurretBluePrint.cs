using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    /* Standard Turret (level 1) */
    public GameObject m_prefab;
    public int m_cost;

    public int _upgradeOneCost;
    public int _upgradeTwoCost;
}
