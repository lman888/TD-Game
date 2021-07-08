using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color m_hoverColor;
    public Color m_notEnoughMoneyColor;
    public Vector3 m_offset;

    [HideInInspector]
    public GameObject m_turret;
    [HideInInspector]
    public TurretBluePrint m_turretBluePrint;
    [HideInInspector]
    public bool m_isUpgraded = false;

    private Renderer m_rend;
    private Color m_startColor;

    BuildManager m_buildManager;

    private void Start()
    {
        m_buildManager = BuildManager.s_instance;
        m_rend = GetComponent<Renderer>();
        m_startColor = m_rend.materials[1].color;
    }

    public Vector3 GetBuildPos()
    {
        return transform.position + m_offset;
    }

    void BuildTurret(TurretBluePrint a_bluePrint)
    {
        if (PlayerStats.m_money < a_bluePrint.m_cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        PlayerStats.m_money -= a_bluePrint.m_cost;

        GameObject m_objectTurret = Instantiate(a_bluePrint.m_prefab, GetBuildPos(), Quaternion.identity);
        /* Informs the Node that a Turret is on it */
        m_turret = m_objectTurret;

        m_turretBluePrint = a_bluePrint;

        GameObject m_effect = Instantiate(m_buildManager.m_buildEffect, GetBuildPos(), Quaternion.identity);

        Destroy(m_effect, 3.0f);

        Debug.Log("Turret Built");
    }

    private void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (m_turret != null)
        {
            m_buildManager.SelectNode(this);
            return;
        }

        if (!m_buildManager.CanBuild)
            return;

        BuildTurret(m_buildManager.GetTurretToBuild());
    }

    public void SellTurret()
    {
        PlayerStats.m_money += m_turretBluePrint.GetSellAmount();

        //Spawn a Cool Effect
        GameObject m_effect = Instantiate(m_buildManager.m_sellEffect, GetBuildPos(), Quaternion.identity);
        Destroy(m_effect, 3.0f);

        Destroy(m_turret);
        m_turretBluePrint = null;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.m_money < m_turretBluePrint.m_upgradeCost)
        {
            Debug.Log("Not Enough Money to Upgrade");
            return;
        }

        PlayerStats.m_money -= m_turretBluePrint.m_upgradeCost;

        /* Destroys non-upgraded Turret */
        Destroy(m_turret);

        /* Builds Upgrade Turret */
        GameObject m_objectTurret = Instantiate(m_turretBluePrint.m_upgradedPrefab, GetBuildPos(), Quaternion.identity);
        /* Informs the Node that a Turret is on it */
        m_turret = m_objectTurret;

        GameObject m_effect = Instantiate(m_buildManager.m_buildEffect, GetBuildPos(), Quaternion.identity);
        Destroy(m_effect, 3.0f);

        m_isUpgraded = true;

        Debug.Log("Turret Upgraded");
    }

    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!m_buildManager.CanBuild)
            return;

        if (m_buildManager.HasMoney)
        {
            m_rend.materials[1].color = m_hoverColor;
        }
        else
        {
            m_rend.materials[1].color = m_notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        m_rend.materials[1].color = m_startColor;
    }
}
