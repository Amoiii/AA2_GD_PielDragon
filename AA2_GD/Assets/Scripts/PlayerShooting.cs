using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bulletPrefab;

    void Update()
    {
        // 1. Apuntar con el ratón
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        transform.right = direction; // El jugador rota mirando al ratón

        // 2. Disparar (Click Izquierdo) solo si la música es violenta
        if (Input.GetMouseButtonDown(0) && gameManager.isViolentMusicOn)
        {
            GameObject miBala = Instantiate(bulletPrefab, transform.position, transform.rotation);
            miBala.GetComponent<Bullet>().targetTag = "Enemy"; // Esta bala busca enemigos
            miBala.GetComponent<Bullet>().speed = 15f; // Bala rápida
        }
    }
}
