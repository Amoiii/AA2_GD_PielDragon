using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private GameManager gameManager;

    [Header("Estad�sticas Aleatorias")]
    public float minSpeed = 1f;
    public float maxSpeed = 3.5f;
    private float speed;

    public float minFireRate = 1.5f;
    public float maxFireRate = 3.5f;
    private float fireRate;

    public GameObject bulletPrefab;
    private float nextFireTime;

    [Header("Comportamiento")]
    public float reactionTime = 1.5f;
    private bool wasViolentLastFrame = false;

    // Variables para pasear al azar
    private Vector2 randomDirection;
    private float changeDirectionTime = 2f;
    private float nextChangeTime;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("ERROR: No hay player");

        gameManager = FindObjectOfType<GameManager>();

        speed = Random.Range(minSpeed, maxSpeed);
        fireRate = Random.Range(minFireRate, maxFireRate);

        PickRandomDirection();
    }

    void Update()
    {
        if (gameManager == null || player == null) return;
        
        if (gameManager.isViolentMusicOn)
        {
            if (!wasViolentLastFrame)
            {
                nextFireTime = Time.time + reactionTime;
                wasViolentLastFrame = true;
            }
            
            Vector2 direction = player.position - transform.position;
            
            FlipEnemy(direction);
            
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            
            if (Time.time >= nextFireTime)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
                
                GameObject miBala = Instantiate(bulletPrefab, transform.position, bulletRotation);
                miBala.GetComponent<Bullet>().targetTag = "Player";
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            wasViolentLastFrame = false;

            if (Time.time >= nextChangeTime)
            {
                PickRandomDirection();
            }
            
            FlipEnemy(randomDirection);
            
            transform.position += (Vector3)randomDirection * (speed * 0.5f) * Time.deltaTime;
        }
    }

    void PickRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        nextChangeTime = Time.time + changeDirectionTime;
    }
    
    void FlipEnemy(Vector2 direction)
    {
        Vector3 scale = transform.localScale;
        
        if (direction.x < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (direction.x > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}