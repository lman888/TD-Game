using UnityEngine;

internal class HighLightSelectionResponseBase : MonoBehaviour, ISelectionResponse
{
    [SerializeField] public Material _canPurchaseMaterial;
    [SerializeField] public Material _notEnoughFundsMaterial;
    [SerializeField] public Material _defaultMaterial;

    public void EnoughFunds(Transform selection)
    {
        var _selectionRenderer = selection.GetComponent<MeshRenderer>();
        if (_selectionRenderer != null)
        {
            _selectionRenderer.material = this._canPurchaseMaterial;
        }
    }

    public void NotEnoughFunds(Transform selection)
    {
        var _selectionRenderer = selection.GetComponent<MeshRenderer>();
        if (_selectionRenderer != null)
        {
            _selectionRenderer.material = this._notEnoughFundsMaterial;
        }
    }

    public void OnDeSelect(Transform selection)
    {
        var _selectionRenderer = selection.GetComponent<MeshRenderer>();
        if (_selectionRenderer != null)
        {
            _selectionRenderer.material = this._defaultMaterial;
        }
    }
}