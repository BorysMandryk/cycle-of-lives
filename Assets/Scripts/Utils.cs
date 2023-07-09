using UnityEngine;

public static class Utils
{
    public static Vector2 SnapToGrid(Grid grid, Vector2 position)
    {
        Vector3Int cellPos = grid.WorldToCell(position);
        return grid.GetCellCenterWorld(cellPos);
    }
}
