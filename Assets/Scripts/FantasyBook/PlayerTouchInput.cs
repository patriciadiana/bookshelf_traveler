using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchInput : MonoBehaviour
{
    [SerializeField] private MoveCharacter moveCharacter;
    private Pathfinding pathfinding;

    private void Awake()
    {
        pathfinding = Pathfinding.Instance;

        if (moveCharacter == null)
        {
            Debug.LogError("MoveCharacter reference not assigned!");
        }
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnStartTouch += HandleTouch;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnStartTouch -= HandleTouch;
        }
    }

    private void HandleTouch(Vector2 screenPosition, float time)
    {
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        touchWorldPosition.z = 0f;

        Vector3 characterPosition = moveCharacter.GetPosition();

        pathfinding.GetGrid().GetXY(characterPosition, out int startX, out int startY);
        pathfinding.GetGrid().GetXY(touchWorldPosition, out int targetX, out int targetY);

        if (!IsWithinGrid(targetX, targetY)) return;

        List<PathNode> pathNodes = pathfinding.FindPath(startX, startY, targetX, targetY);

        if (pathNodes != null && pathNodes.Count > 0)
        {
            moveCharacter.SetTargetPosition(ConvertPathToWorldPositions(pathNodes));
        }
        else
        {
            Debug.Log("No valid path found!");
            return;
        }
    }

    private bool IsWithinGrid(int x, int y)
    {
        return x >= 0 && x < GridAutoInitializer.Instance.GetWidth() &&
               y >= 0 && y < GridAutoInitializer.Instance.GetHeight();
    }

    private List<Vector3> ConvertPathToWorldPositions(List<PathNode> pathNodes)
    {
        List<Vector3> worldPositions = new List<Vector3>(pathNodes.Count);

        float cellSize = pathfinding.GetGrid().GetCellSize();

        foreach (PathNode node in pathNodes)
        {
            Vector3 worldPos = pathfinding.GetGrid().GetWorldPosition(node.x, node.y);
            worldPos += Vector3.one * cellSize * 0.5f;
            worldPositions.Add(worldPos);
        }

        return worldPositions;
    }
}
