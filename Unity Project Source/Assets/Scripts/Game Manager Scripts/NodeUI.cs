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

        var level = m_target.GetPlacedTurret().GetComponent<OrbTurret>();

        if (m_target.GetPlacedTurret())
        {
            m_upgradeButton.interactable = true;
        }
        
        if (level.GetCurrentLevel() == 2)
        {
            m_upgradeCost.text = "Max Upgrade (For Now)";
            m_upgradeButton.interactable = false;
        }

        if (level.GetCurrentLevel() == 0)
        {
            m_sellAmount.text = "$" + m_target.GetTurretBluePrintType().m_cost / 2;
        }
        else if(level.GetCurrentLevel() == 1)
        {
            m_sellAmount.text = "$" + m_target.GetTurretBluePrintType()._upgradeOneCost / 2;
        }
        else if (level.GetCurrentLevel() == 2)
        {
            m_sellAmount.text = "$" + m_target.GetTurretBluePrintType()._upgradeTwoCost / 2;
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
        BuildManager.s_instance.DeSelectNode();
    }

}
