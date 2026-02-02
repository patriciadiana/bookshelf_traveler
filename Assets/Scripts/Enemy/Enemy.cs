using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    private float distance;
    public float speed;
    public float distanceOffset;
    private Vector2 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance < distanceOffset)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        HandleMovement();
    }

    /*Function for when the player leaves the range*/
    private void MoveAwayFromPlayer()
    {
        Vector2 direction = transform.position - player.transform.position;
        direction.Normalize();
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 currentPosition = transform.position;
        float velocity = ((currentPosition - lastPosition) / Time.deltaTime).magnitude;

        lastPosition = currentPosition;
    }
}
