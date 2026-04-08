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

    private int health = 3;
    public static event Action OnSpaceshipDied;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectPool = FindFirstObjectByType<ObjectPool>();       
    }

    /*Only for debugging*/
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet01 = objectPool.GetObjectFromPool("PlayerBullet", bulletPosition01.transform.position);
        GameObject bullet02 = objectPool.GetObjectFromPool("PlayerBullet", bulletPosition02.transform.position);
    }

    private void FixedUpdate()
    {
        if(joystick.joystickVector.y != 0)
        {
            rb.linearVelocity = new Vector2(joystick.joystickVector.x * speed, joystick.joystickVector.y * speed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBullet")
        {
            health--;
            collision.GetComponent<EnemyBullet>()?.SendMessage("ReturnToPool");

            if (health <= 0)
            {
                GameOverEvent.TriggerGameOver();
            }
        }

        if (collision.tag == "EnemySpaceship")
        {
            GameOverEvent.TriggerGameOver();
        }
    }
}
