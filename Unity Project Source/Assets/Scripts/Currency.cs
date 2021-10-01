using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public void SellMoney(Node _turret)
    {
        //var _turretSellAmount = _turretAmount.GetComponent<BuildManager>();
        var _turretLevel = _turret.GetPlacedTurret().GetComponent<OrbTurret>();

        if (_turretLevel.GetCurrentLevel() == 0)
        {
            PlayerStats.m_money += _turret.GetTurretBluePrintType().m_cost / 2;
            Debug.Log("Sold Base turret");
        }
        if (_turretLevel.GetCurrentLevel() == 1)
        {
            PlayerStats.m_money += _turret.GetTurretBluePrintType()._upgradeOneCost / 2;
            Debug.Log("Sold Level One turret");
        }
        if (_turretLevel.GetCurrentLevel() == 2)
        {
            PlayerStats.m_money += _turret.GetTurretBluePrintType()._upgradeTwoCost / 2;
            Debug.Log("Sold Level Two turret");
        }
    }

    public void BuildCost(TurretBluePrint _turretAmount)
    {
        PlayerStats.m_money -= _turretAmount.m_cost;
    }
}
