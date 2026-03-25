using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public MovementJoystick joystick;
    public float speed;
    private Rigidbody2D rb;

    public GameObject bullet;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        GameObject bullet01 = Instantiate(bullet);
        bullet01.transform.position = bulletPosition01.transform.position;

        GameObject bullet02 = Instantiate(bullet);
        bullet02.transform.position = bulletPosition02.transform.position;
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
        if ((collision.tag == "EnemySpaceship") || (collision.tag == "EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}
