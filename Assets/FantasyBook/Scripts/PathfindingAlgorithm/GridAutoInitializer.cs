using UnityEngine;
using UnityEngine.Tilemaps;

public class GridAutoInitializer : Singleton<GridAutoInitializer>
{
    [SerializeField] private Tilemap[] tilemaps;
    [SerializeField] private float cellSize = 1f;
    private Pathfinding pathfinding;
    private int width;
    private int height;

    private void Awake()
    {
        pathfinding = Pathfinding.Instance;
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        if (tilemaps == null || tilemaps.Length == 0) return;

        Vector3Int min = new Vector3Int(int.MaxValue, int.MaxValue, 0);
        Vector3Int max = new Vector3Int(int.MinValue, int.MinValue, 0);

        foreach (var tilemap in tilemaps)
        {
            tilemap.CompressBounds();

            BoundsInt bounds = tilemap.cellBounds;
            min = Vector3Int.Min(min, bounds.min);
            max = Vector3Int.Max(max, bounds.max);
        }

        width = (max.x - min.x);
        height = (max.y - min.y);

        Vector3 origin = min;

        pathfinding.Initialize(width, height, cellSize, origin);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
}
