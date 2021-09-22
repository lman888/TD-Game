using UnityEngine;

internal interface ISelectionResponse
{
    void OnDeSelect(Transform selection);
    void EnoughFunds(Transform selection);
    void NotEnoughFunds(Transform selection);
}