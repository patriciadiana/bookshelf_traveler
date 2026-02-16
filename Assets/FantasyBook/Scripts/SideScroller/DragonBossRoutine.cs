using UnityEngine;

public class DragonBossRoutine : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bullet;
    public Transform bulletPos;
    public float shootInterval = 2f;

    [Header("Movement Settings")]
    public float speed = 2f;
    public float groundY = 0f;     
    public float skyY = 20f;       
    public float horizontalOffset = 10f; 
    public float stayOnGroundTime = 5f;
    public float stayInSkyTime = 5f;

    [Header("References")]
    public Transform player;

    [Header("State")]
    public bool isAlive = true;

    private float timer;
    private float shootTimer;

    private Vector3 skyPos;      
    private Vector3 initialPos;   
    private Vector3 targetPos;

    private enum State { Rising, InSky, MoveRight, LowerToGround, Grounded, RiseAgain, MoveLeft, LowerInSky }

    private State currentState = State.InSky;

    private void Start()
    {
        initialPos = transform.position;
        skyPos = new Vector3(transform.position.x, skyY, transform.position.z);
        targetPos = skyPos;
    }

    private void Update()
    {
        if (!isAlive) return;

        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case State.InSky:
                if (timer > stayInSkyTime)
                {
                    if (player != null)
                        targetPos = new Vector3(player.position.x + horizontalOffset, skyY, transform.position.z);
                    currentState = State.Rising;
                    timer = 0;
                }
                break;

            case State.Rising:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, skyY, transform.position.z), speed * Time.deltaTime);
                if (Mathf.Abs(transform.position.y - skyY) < 0.1f)
                {
                    currentState = State.MoveRight;
                    timer = 0;
                }
                break;

            case State.MoveRight:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    targetPos = new Vector3(transform.position.x, groundY, transform.position.z);
                    currentState = State.LowerToGround;
                    timer = 0;
                }
                break;

            case State.LowerToGround:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if (Mathf.Abs(transform.position.y - groundY) < 0.1f)
                {
                    currentState = State.Grounded;
                    timer = 0;
                }
                break;

            case State.Grounded:
                if (timer > stayOnGroundTime)
                {
                    targetPos = new Vector3(transform.position.x, skyY, transform.position.z);
                    currentState = State.RiseAgain;
                    timer = 0;
                }
                break;

            case State.RiseAgain:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    if (player != null)
                        targetPos = new Vector3(player.position.x - horizontalOffset, skyY, transform.position.z);
                    currentState = State.MoveLeft;
                    timer = 0;
                }
                break;

            case State.MoveLeft:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    targetPos = new Vector3(transform.position.x, skyY - 2f, transform.position.z);
                    currentState = State.LowerInSky;
                    timer = 0;
                }
                break;

            case State.LowerInSky:
                transform.position = Vector3.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, initialPos) < 0.1f)
                {
                    initialPos = transform.position;
                    currentState = State.InSky;
                    timer = 0;
                }
                break;
        }
    }

    void HandleShooting()
    {
        if (currentState == State.InSky)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > shootInterval)
            {
                shootTimer = 0;
                Instantiate(bullet, bulletPos.position, Quaternion.identity);
            }
        }
        else
        {
            return;
        }
    }
}
