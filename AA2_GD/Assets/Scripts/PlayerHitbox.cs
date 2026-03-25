using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public GameManager gameManager;

    public float shrinkSpeed = 0.5f;
    public float growthPerKill = 0.3f;
    public float shrinkPerHit = 0.4f; // <-- NUEVO: Cuánto te encoges de golpe si te dan
    public float minSizeToDie = 0.1f;

    void Update()
    {
        if (gameManager.isViolentMusicOn)
        {
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, 0) * Time.deltaTime;
        }

        if (transform.localScale.x <= minSizeToDie)
        {
            Die();
        }
    }

    public void KillEnemy()
    {
        transform.localScale += new Vector3(growthPerKill, growthPerKill, 0);
        Debug.Log("¡Enemigo eliminado! Hitbox aumentada.");
    }

    // <-- NUEVA FUNCIÓN: Cuando una bala enemiga te toca
    public void TakeDamage()
    {
        transform.localScale -= new Vector3(shrinkPerHit, shrinkPerHit, 0);
        Debug.Log("¡Ouch! Te han dado, te encoges de golpe.");

        // Comprobamos si este tiro te ha matado directamente
        if (transform.localScale.x <= minSizeToDie)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("GAME OVER: Tu hitbox llegó a 0.");
        gameObject.SetActive(false);
    }
}