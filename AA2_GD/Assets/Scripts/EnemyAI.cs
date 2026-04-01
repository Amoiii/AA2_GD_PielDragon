using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private GameManager gameManager;

    [Header("Estadísticas Aleatorias")]
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
        // Buscamos al jugador de forma segura
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("ERROR: ¡El enemigo no encuentra al Player! Asegúrate de ponerle el Tag 'Player' al jugador en Unity.");
        }

        gameManager = FindObjectOfType<GameManager>();

        speed = Random.Range(minSpeed, maxSpeed);
        fireRate = Random.Range(minFireRate, maxFireRate);

        PickRandomDirection();
    }

    void Update()
    {
        if (gameManager == null || player == null) return;

        // Modo violento (Persigue y dispara)
        if (gameManager.isViolentMusicOn)
        {
            if (!wasViolentLastFrame)
            {
                nextFireTime = Time.time + reactionTime;
                wasViolentLastFrame = true;
            }

            // Calculamos hacia dónde está el jugador
            Vector2 direction = player.position - transform.position;

            // Hacemos que el enemigo mire a izquierda/derecha sin rotar
            FlipEnemy(direction);

            // Movemos al enemigo
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Disparo
            if (Time.time >= nextFireTime)
            {
                // Calculamos la rotación matemática para que la BALA mire hacia el jugador
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

                // Instanciamos la bala con su rotación correcta
                GameObject miBala = Instantiate(bulletPrefab, transform.position, bulletRotation);
                miBala.GetComponent<Bullet>().targetTag = "Player";
                miBala.GetComponent<Bullet>().speed = 5f;
                nextFireTime = Time.time + fireRate;
            }
        }
        // Modo tranqui (Pasea por el escenario)
        else
        {
            wasViolentLastFrame = false;

            if (Time.time >= nextChangeTime)
            {
                PickRandomDirection();
            }

            // Hacemos que mire hacia donde está caminando
            FlipEnemy(randomDirection);

            // Movemos al enemigo
            transform.position += (Vector3)randomDirection * (speed * 0.5f) * Time.deltaTime;
        }
    }

    void PickRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        nextChangeTime = Time.time + changeDirectionTime;
    }

    // --- NUEVO MÉTODO PARA VOLTEAR AL ENEMIGO ---
    void FlipEnemy(Vector2 direction)
    {
        Vector3 scale = transform.localScale;

        // Si la dirección X es negativa (izquierda), escala negativa. Si es positiva (derecha), escala positiva.
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