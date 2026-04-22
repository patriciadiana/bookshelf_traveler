using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour
{
    public MovementJoystick joystick;
    public float speed;
    private Rigidbody2D rb;

    public GameObject bullet;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    private ObjectPool objectPool;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    public static event Action<float> OnHealthChanged;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectPool = FindFirstObjectByType<ObjectPool>();
        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(currentHealth);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            Shoot();
    }

    public void Shoot()
    {
        GameObject bullet01 = objectPool.GetObjectFromPool("PlayerBullet");
        bullet01.transform.position = bulletPosition01.transform.position;

        GameObject bullet02 = objectPool.GetObjectFromPool("PlayerBullet");
        bullet02.transform.position = bulletPosition02.transform.position;
    }

    private void FixedUpdate()
    {
        if (joystick.joystickVector.y != 0)
            rb.linearVelocity = new Vector2(joystick.joystickVector.x * speed, joystick.joystickVector.y * speed);
        else
            rb.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            currentHealth--;
            collision.GetComponent<Bullet>().ReturnToPool();

            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
                GameOverEvent.TriggerGameOver();
        }

        if (collision.tag == "EnemySpaceship")
        {
            GameOverEvent.TriggerGameOver();
        }
    }
}