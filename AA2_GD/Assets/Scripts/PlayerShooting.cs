using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public bool isActive;

    public GameManager gameManager;
    public GameObject bulletPrefab;
    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetMouseButtonDown(0) && gameManager.isViolentMusicOn)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // 1. Obtenemos dirección del ratón
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direction = (mousePos - transform.position).normalized;

        // 2. Calculamos la rotación para la bala (para que mire hacia donde disparamos)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

        // 3. Instanciamos la bala con la rotación correcta
        GameObject miBala = Instantiate(bulletPrefab, transform.position, bulletRotation);
        miBala.GetComponent<Bullet>().targetTag = "Enemy";
        miBala.GetComponent<Bullet>().speed = 15f;
    }
}