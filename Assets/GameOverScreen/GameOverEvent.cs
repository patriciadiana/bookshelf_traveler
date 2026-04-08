using System;
using UnityEngine;

public class GameOverEvent : MonoBehaviour
{
    public static event Action OnGameOver;

    public static void TriggerGameOver()
    {
        OnGameOver?.Invoke();
    }
}
