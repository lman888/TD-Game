using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject m_ui;

    public Text m_upgradeCost;
    public Text m_sellAmount;
    public Button m_upgradeButton;

    private Node m_target;

    public void SetTarget(Node a_node)
    {
        m_target = a_node;

        transform.position = m_target.GetBuildPosition();

        if (!m_target.m_isUpgraded)
        {
            
            m_upgradeButton.interactable = true;
        }
        else
        {
            m_upgradeCost.text = "Max Upgrade (For Now)";
            m_upgradeButton.interactable = false;
        }

        if (!m_target.m_isUpgraded)
        {
            //m_sellAmount.text = "$" + m_target.m_turretBluePrint.GetSellAmount();
        }
        else if(m_target.m_isUpgraded)
        {
            //m_sellAmount.text = "$" + m_target.m_turretBluePrint.GetUpgradedSellAmount();
        }

        m_ui.SetActive(true);
    }

    public void Hide()
    {
        m_ui.SetActive(false);
    }

    public void Upgrade()
    {
        /* Initiates the Turret Upgrade */
        BuildManager.s_instance.UpgradeTurret(m_target);

        /* Closes the UI right after the Upgrade */
        BuildManager.s_instance.DeSelectNode();
    }

    public void Sell()
    {
        BuildManager.s_instance.SellTurret(m_target);
        m_target.m_isUpgraded = false;
        BuildManager.s_instance.DeSelectNode();
    }

}
