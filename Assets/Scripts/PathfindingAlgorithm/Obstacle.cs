using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Pathfinding pathfinding;
    private void Start()
    {
        pathfinding = Pathfinding.Instance;
        Grid<PathNode> grid = pathfinding.GetGrid();

        int width = Mathf.CeilToInt(transform.localScale.x / grid.GetCellSize());
        int height = Mathf.CeilToInt(transform.localScale.y / grid.GetCellSize());

        Vector3 bottomLeft = transform.position - new Vector3(width * grid.GetCellSize() / 2f, height * grid.GetCellSize() / 2f, 0);
        grid.GetXY(bottomLeft, out int startX, out int startY);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pathfinding.SetWalkable(startX + x, startY + y, false);
            }
        }
    }
}
