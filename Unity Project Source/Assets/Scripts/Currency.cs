using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public void SellMoney(TurretBluePrint _turretAmount)
    {
        PlayerStats.m_money += _turretAmount.m_cost / 2;
    }

    public void BuildCost(TurretBluePrint _turretAmount)
    {
        PlayerStats.m_money -= _turretAmount.m_cost;
    }
}
