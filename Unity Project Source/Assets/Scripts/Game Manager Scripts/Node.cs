using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [HideInInspector] public bool _turretPlaced;
    [HideInInspector] public TurretBluePrint _turretBluePrint;
    //[HideInInspector] public bool m_isUpgraded = false;

    [SerializeField] private Transform _placementSpot;
    [SerializeField] private string _selectableTag = "BuildableSpot";

    private ISelectionResponse _selectionResponse;

    private Transform _selection;

    GameObject _placedTurret;

    BuildManager _buildManager;

    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    private void Start()
    {
        _buildManager = BuildManager.s_instance;
    }

    private void Update()
    {
        /* Checks if the mouse pointer is hovering over another UI element/GameObject that is not the node */
        /* If it isnt the node, we return and exit */
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        /* Checks if this spot has been built on */
        if (!_buildManager.CanBuild)
            return;

        /* If this has not been build on, we check if the player has enough money and highlight the color of the node accordingly */
        /* Selection/DeSelection Result */
        if (_selection != null)
        {
            _selectionResponse.OnDeSelect(_selection);
        }

        #region
        /* Creating a Ray */
        var _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        /* Selection Determination */
        _selection = null;
        if (Physics.Raycast(_ray, out var _hit))
        {
            var selection = _hit.transform;
            if (selection.CompareTag(_selectableTag))
            {
                _selection = selection;
            }
        }
        #endregion

        /* Selection/DeSelection Result */
        if (_selection != null)
        {
            if (_buildManager.HasMoney)
            {
                _selectionResponse.EnoughFunds(_selection);
            }
            else
            {
                _selectionResponse.NotEnoughFunds(_selection);
            }
        }
    }
    
    public Vector3 GetBuildPosition()
    {
        return _placementSpot.position;
    }

    private void OnMouseDown()
    {
        /* Checks if the mouse pointer is clicking over another UI element/GameObject that is not the node */
        /* If it isnt the node, we return and exit */
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        /* Checks if there is a turret currently built on this spot, if it is we select the turret and exit */
        if (_turretPlaced)
        {
            _buildManager.SelectNode(this);
            return;
        }

        /* Checks if we can build on thie Node (If there is a turret on it) */
        if (!_buildManager.CanBuild)
            return;


        /* If no turret is built, we build one! */
        _buildManager.BuildTurret(gameObject.GetComponent<Node>());

        _turretPlaced = true;
    }

    public void ClearSpot()
    {
        _turretPlaced = false;
        Destroy(_placedTurret);
    }

    public void SetPlacedTurret(GameObject _turret, TurretBluePrint _bluePrint)
    {
        _placedTurret = _turret;
        SetTurretBluePrintType(_bluePrint);
    }

    public GameObject GetPlacedTurret()
    {
        return _placedTurret;
    }

    void SetTurretBluePrintType(TurretBluePrint _bluePrint)
    {
        _turretBluePrint = _bluePrint;
    }

    public TurretBluePrint GetTurretBluePrintType()
    {
        return _turretBluePrint;
    }

}