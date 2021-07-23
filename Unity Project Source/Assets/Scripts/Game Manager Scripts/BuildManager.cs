using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager s_instance;

    public GameObject m_buildEffect;
    public GameObject m_sellEffect;

    private TurretBluePrint m_turretToBuild;
    private Node m_selectedNode;

    public NodeUI m_nodeUI;

    private void Awake()
    {
        if (s_instance != null)
        {
            Debug.Log("More then one build Manager in Scence");
            return;
        }
        s_instance = this;
    }

    /* This is called a Property as we can only get information, we never set anything in this function */
    public bool CanBuild { get { return m_turretToBuild != null; } }

    /* Checks if we have the avaliable money */
    public bool HasMoney { get { return  PlayerStats.m_money >= m_turretToBuild.m_cost; } }

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

        m_turretToBuild = null;

        /* Turns on the Node UI for this selected node */
        m_nodeUI.SetTarget(a_node);
    }

    /* Disables the Node UI */
    public void DeSelectNode()
    {
        m_selectedNode = null;
        m_nodeUI.Hide();
    }

    /* Sets the Turret we Selected */
    public void SelectTurretToBuild(TurretBluePrint a_turret)
    {
        m_turretToBuild = a_turret;

        DeSelectNode();
    }

    /* Obtains our Turret Prefab to build */
    public TurretBluePrint GetTurretToBuild()
    {
        return m_turretToBuild;
    }

}
