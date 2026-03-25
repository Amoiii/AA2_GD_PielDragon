using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public string targetTag;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag(targetTag))
        {
            if (targetTag == "Enemy")
            {
                FindObjectOfType<PlayerHitbox>().KillEnemy();
                Destroy(hit.gameObject);
            }
            else if (targetTag == "Player") 
            {
                FindObjectOfType<PlayerHitbox>().TakeDamage(); 
            }
            Destroy(gameObject);
        }
    }
}