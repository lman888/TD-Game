using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TGGame/Turret Settings", fileName = "NewTurretSettings")]
public class TurretUpgrades : ScriptableObject
{
    [Serializable]
    public class UpgradeDetails
    {
        public int Level;
        public int Damage;
        public int ShootSpeed;
        public int SlowPower;
    }

    public List<UpgradeDetails> upgradeDetails;

    public UpgradeDetails GetUpgradeDetails(int level)
    {
        if (upgradeDetails.Capacity == 0)
        {
            Debug.LogError("No upgrade details exist!");
            return null;
        }

        var index = upgradeDetails.FindIndex(u => u.Level == level);
        if (index < 0)
        {
            Debug.LogWarning($"Failed to find upgrade details for level {level}");
            return upgradeDetails[0];
        }

        return upgradeDetails[index];
    }
}
