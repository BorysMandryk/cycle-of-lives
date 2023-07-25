using UnityEngine;

public static class Utils
{
    public static Vector2 SnapToGrid(Grid grid, Vector2 position)
    {
        Vector3Int cellPos = grid.WorldToCell(position);
        return grid.GetCellCenterWorld(cellPos);
    }

    public static float HeightToForce(float height, Rigidbody2D rigidbody)
    {
        return Mathf.Sqrt(height * (Physics2D.gravity.y * rigidbody.gravityScale) * -2.0f) * rigidbody.mass;
    }
}
