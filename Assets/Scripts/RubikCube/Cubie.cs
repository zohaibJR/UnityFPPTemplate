using UnityEngine;

public class Cubie : MonoBehaviour
{
    public Vector3Int gridPos;

    public void SetGridPosition(Vector3 pos)
    {
        gridPos = Vector3Int.RoundToInt(pos);
    }
}