using UnityEngine;

public interface IGridElement
{
    bool IsSolid { get; }

    public void OnGridPosition(Vector3 position);
}