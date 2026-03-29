using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBullet;

    private void OnEnable()
    {
        InvokeRepeating("FireEnemyBullet", 1f, 2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
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
