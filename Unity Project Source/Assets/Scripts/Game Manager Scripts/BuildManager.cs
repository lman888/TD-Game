using UnityEngine;


/* This needs to obtain what kind of Turret it needs to build */
/* It instantiates the turret on a given node/object */
/* Passes the reference of OrbTurret */
/* new turret is orb turret, in new turret get component orb, pass that into the orb turret, orb does rest of work */

public class BuildManager : MonoBehaviour
{
    public static BuildManager s_instance;
    public GameObject _buildEffect;
    public GameObject m_sellEffect;

    private TurretBluePrint _turretBluePrint;
    private Node m_selectedNode;
    private ISelectionResponse _selectionResponse;

    public NodeUI m_nodeUI;

    private GameObject _turret;

    private Currency _currency;

    private void Awake()
    {
        if (s_instance != null)
        {
            Debug.Log("More then one build Manager in Scence");
            return;
        }
        s_instance = this;

        _selectionResponse = GetComponent<HighLightSelectionResponseBase>();

        _currency = GetComponent<Currency>();
    }

    /* This is called a Property as we can only get information, we never set anything in this function */
    public bool CanBuild { get { return _turretBluePrint != null; } }

    /* Checks if we have the avaliable money */
    public bool HasMoney { get { return  PlayerStats.m_money >= _turretBluePrint.m_cost; } }

    /* Functionality for Node that is selected */
    public void SelectNode(Node a_node)
    {
        /* De-selects the node if we click on it again */
        if (m_selectedNode == a_node)
        {
            DeSelectNode();
            return;
        }

        m_selectedNode = a_node;

        _selectionResponse.OnDeSelect(m_selectedNode.transform);

        _turretBluePrint = null;

        /* Turns on the Node UI for this selected node */
        m_nodeUI.SetTarget(a_node);
    }

    /* Disables the Node UI */
    public void DeSelectNode()
    {
        if (m_selectedNode != null)
        {
            _selectionResponse.OnDeSelect(m_selectedNode.transform);
        }
        m_selectedNode = null;
        m_nodeUI.Hide();
    }

    /* Sets the Turret we Selected */
    public void SelectTurretToBuild(TurretBluePrint a_turret)
    {
        _turretBluePrint = a_turret;
        DeSelectNode();
    }

    /* Obtains our Turret Prefab to build */
    public TurretBluePrint GetTurretToBuild()
    {
        return _turretBluePrint;
    }

    public void BuildTurret(Node _node)
    {
        /* Performs a Money check to see if we have enough */
        if (PlayerStats.m_money < GetTurretToBuild().m_cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        /* Subtracts the Players money to build said turret */
        _currency.BuildCost(_turretBluePrint);

        /* Spawns in the turret we chose to built */
        _turret = Instantiate(GetTurretToBuild().m_prefab, _node.GetBuildPosition(), Quaternion.identity);

        _node.SetPlacedTurret(_turret, _turretBluePrint);

        /* Spawns coolio build effect */
        GameObject m_effect = Instantiate(_buildEffect, _node.GetBuildPosition(), Quaternion.identity);
        Destroy(m_effect, 3.0f);
    }

    /* The purpose of this function is to give player some gold back when a turret is sold (Half the current turret Value) */
    public void SellTurret(Node _node)
    {
        /* Get the TurretBluePrint that is on this object */
        _currency.SellMoney(_node);

        _node.ClearSpot();

        //Spawn a Cool Effect when turret is sold
        GameObject m_effect = Instantiate(m_sellEffect, _node.GetBuildPosition(), Quaternion.identity);
        Destroy(m_effect, 3.0f);
        _turretBluePrint = null;
        m_nodeUI.Hide();
    }

    public void UpgradeTurret(Node _node)
    {
        /* Check Cost */
        /* Subtract Cost from Amount */

        /* Change over turret */
        var chosenTurret = _node.GetPlacedTurret().GetComponent<OrbTurret>();

        chosenTurret._models[chosenTurret.GetCurrentLevel()].SetActive(false);

        chosenTurret.IncreaseTurretLevel();

        chosenTurret._models[chosenTurret.GetCurrentLevel()].SetActive(true);
    }
}