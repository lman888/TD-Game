using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Turrets the Player can Build")]
    public TurretBluePrint m_standardTurret;
    public TurretBluePrint m_missileTurret;
    public TurretBluePrint m_laserTurret;

    BuildManager m_buildManager;
    private void Start()
    {
        m_buildManager = BuildManager.s_instance;
    }

    /* These Methods are called when we click on them */
    public void SelectStandardTurret()
    {
        m_buildManager.SelectTurretToBuild(m_standardTurret);
    }

    public void SelectMissileLauncher()
    {
        m_buildManager.SelectTurretToBuild(m_missileTurret);
    }

    public void SelectLaserTurret()
    {
        m_buildManager.SelectTurretToBuild(m_laserTurret);
    }
}
