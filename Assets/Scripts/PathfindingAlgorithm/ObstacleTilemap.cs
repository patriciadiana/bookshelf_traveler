using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleTilemap : MonoBehaviour
{
    private Pathfinding pathfinding;
    private void Start()
    {
        pathfinding = Pathfinding.Instance;
        Grid<PathNode> grid = pathfinding.GetGrid();

        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int cellPos in bounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(cellPos))
                continue;

            Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);
            grid.GetXY(worldPos, out int x, out int y);

            pathfinding.SetWalkable(x, y, false);
        }
    }
}
