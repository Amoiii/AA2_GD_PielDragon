using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float speed = 2f;
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float nextFireTime;

    private GameManager gameManager; // Necesita saber cómo está la música

    // Variables para pasear al azar
    private Vector2 randomDirection;
    private float changeDirectionTime = 2f; // Cambia de rumbo cada 2 segundos
    private float nextChangeTime;

    void Start()
    {
        // El enemigo busca al jugador y al GameManager automáticamente al nacer
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = FindObjectOfType<GameManager>();

        PickRandomDirection(); // Elige su primer rumbo aleatorio
    }

    void Update()
    {
        // Si el GameManager no existe o el jugador ha muerto, no hace nada
        if (gameManager == null || player == null) return;

        // 1. MODO VIOLENTO: Te persiguen y disparan
        if (gameManager.isViolentMusicOn)
        {
            Vector2 direction = player.position - transform.position;
            transform.right = direction;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (Time.time >= nextFireTime)
            {
                GameObject miBala = Instantiate(bulletPrefab, transform.position, transform.rotation);
                miBala.GetComponent<Bullet>().targetTag = "Player";
                miBala.GetComponent<Bullet>().speed = 5f;
                nextFireTime = Time.time + fireRate;
            }
        }
        // 2. MODO TRANQUILO: Pasean a su bola
        else
        {
            // Si toca cambiar de dirección, elige una nueva
            if (Time.time >= nextChangeTime)
            {
                PickRandomDirection();
            }

            // Mira hacia la dirección aleatoria y camina un poco más despacio
            transform.right = randomDirection;
            transform.position += (Vector3)randomDirection * (speed * 0.5f) * Time.deltaTime;
        }
    }

    void PickRandomDirection()
    {
        // Elige una dirección al azar (arriba, abajo, izquierda, derecha o diagonales)
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        nextChangeTime = Time.time + changeDirectionTime;
    }
}