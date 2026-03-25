using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBullet;

    private void Start()
    {
        Invoke("FireEnemyBullet", 1f);
    }

    private void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");

        if (playerShip != null)
        {
            GameObject bullet = Instantiate(enemyBullet);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
