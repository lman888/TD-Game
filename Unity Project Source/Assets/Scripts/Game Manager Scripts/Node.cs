using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color m_hoverColor;
    public Color m_notEnoughMoneyColor;

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
        /* Sets where the turret is built */
        return transform.position + m_buildManager.GetOffset();
    }

    /* Functionality of building a Turret */
    void BuildTurret(TurretBluePrint a_bluePrint)
    {
        /* Performs a Money check to see if we have enough */
        if (PlayerStats.m_money < a_bluePrint.m_cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        /* Subtracts the Players money to build said turret */
        PlayerStats.m_money -= a_bluePrint.m_cost;

        /* Spawns in the turret we chose to built */
        GameObject m_objectTurret = Instantiate(a_bluePrint.m_prefab, GetBuildPos(), Quaternion.identity);
        /* Informs the Node that a Turret is on it */
        m_turret = m_objectTurret;

        /* Assigns the chosen turret to the blueprint */
        m_turretBluePrint = a_bluePrint;
        /* Spawns coolio build effect */
        GameObject m_effect = Instantiate(m_buildManager.m_buildEffect, GetBuildPos(), Quaternion.identity);
        Destroy(m_effect, 3.0f);

        Debug.Log("Turret Built");
    }

    private void OnMouseDown()
    {
        /* Checks if the mouse pointer is clicking over another UI element/GameObject that is not the node */
        /* If it isnt the node, we return and exit */
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        /* Checks if there is a turret currently built on this spot, if it is we select the turret and exit */
        if (m_turret != null)
        {
            m_buildManager.SelectNode(this);
            return;
        }

        /* Checks if we can build on thie Node (If there is a turret on it) */
        if (!m_buildManager.CanBuild)
            return;

        /* If no turret is built, we build one! */
        BuildTurret(m_buildManager.GetTurretToBuild());
    }

    /* The purpose of this function is to give player some gold back when a turret is sold (Half the current turret Value) */
    public void SellTurret()
    {
        /* Checks if the Turret is upgraded and gives the correct amount of money back based on what upgrade it is */
        if (!m_isUpgraded)
            PlayerStats.m_money += m_turretBluePrint.GetSellAmount();
        else if (m_isUpgraded)
            PlayerStats.m_money += m_turretBluePrint.GetUpgradedSellAmount();

        //Spawn a Cool Effect when turret is sold
        GameObject m_effect = Instantiate(m_buildManager.m_sellEffect, GetBuildPos(), Quaternion.identity);
        Destroy(m_effect, 3.0f);
        Destroy(m_turret);
        m_turretBluePrint = null;
    }

    /* If the player has enough money, they can upgrade the selected turret */
    public void UpgradeTurret()
    {
        /* Checks if we got enough money to upgrade the turret */
        if (PlayerStats.m_money < m_turretBluePrint.m_upgradeCost)
        {
            Debug.Log("Not Enough Money to Upgrade");
            return;
        }

        /* Subtracts the turret cost from the players money */
        PlayerStats.m_money -= m_turretBluePrint.m_upgradeCost;

        /* Destroys non-upgraded Turret */
        Destroy(m_turret);

        /* Builds Upgrade Turret */
        GameObject m_objectTurret = Instantiate(m_turretBluePrint.m_upgradedPrefab, GetBuildPos(), Quaternion.identity);
        /* Informs the Node that a Turret is on it */
        m_turret = m_objectTurret;

        /* Cool build effect plays */
        GameObject m_effect = Instantiate(m_buildManager.m_buildEffect, GetBuildPos(), Quaternion.identity);
        Destroy(m_effect, 3.0f);

        /* Set the upgraded bool */
        m_isUpgraded = true;

        Debug.Log("Turret Upgraded");
    }

    /* This runs when we hover our mouse over a Node (Buildable Turret Spot) */
    private void OnMouseEnter()
    {
        /* Checks if the mouse pointer is hovering over another UI element/GameObject that is not the node */
        /* If it isnt the node, we return and exit */
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        /* Checks if this spot has been built on */
        if (!m_buildManager.CanBuild)
            return;

        /* If this has not been build on, we check if the player has enough money and highlight the color of the node accordingly */
        if (m_buildManager.HasMoney)
        {
            m_rend.materials[1].color = m_hoverColor;
        }
        else
        {
            m_rend.materials[1].color = m_notEnoughMoneyColor;
        }
    }

    /* Resets the nodes material back to the basic color */
    private void OnMouseExit()
    {
        m_rend.materials[1].color = m_startColor;
    }
}
