using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public bool isActive;
    
    public GameManager gameManager;
    public GameObject bulletPrefab;

    void Update()
    {
        if (!isActive) return;
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        transform.right = direction;

        
        if (Input.GetMouseButtonDown(0) && gameManager.isViolentMusicOn)
        {
            GameObject miBala = Instantiate(bulletPrefab, transform.position, transform.rotation);
            miBala.GetComponent<Bullet>().targetTag = "Enemy";
            miBala.GetComponent<Bullet>().speed = 15f; 
        }
    }
}
