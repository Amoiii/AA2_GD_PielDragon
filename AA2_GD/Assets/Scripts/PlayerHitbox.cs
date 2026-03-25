using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Mecánica de Tamaño")]
    public float shrinkSpeed = 0.5f;
    public float growthPerKill = 0.3f;
    public float shrinkPerHit = 0.4f;
    public float minSizeToDie = 0.1f;

    [Header("Visuales (Pell de Drac)")]
    public SpriteRenderer spriteRenderer; 
    public Color calmColor = Color.white; 
    public Color dragonColor = Color.black; 

    
    public Sprite calmSprite;
    public Sprite dragonSprite;

    void Update()
    {
        if (gameManager.isViolentMusicOn)
        {
            //Reducir tamaño
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, 0) * Time.deltaTime;

            // modo dragón
            spriteRenderer.color = dragonColor;
            if (dragonSprite != null) spriteRenderer.sprite = dragonSprite;
        }
        else
        {
            //modo normal 
            spriteRenderer.color = calmColor;
            if (calmSprite != null) spriteRenderer.sprite = calmSprite;
        }

        if (transform.localScale.x <= minSizeToDie)
        {
            Die();
        }
    }

    public void KillEnemy()
    {
        transform.localScale += new Vector3(growthPerKill, growthPerKill, 0);
    }

    public void TakeDamage()
    {
        transform.localScale -= new Vector3(shrinkPerHit, shrinkPerHit, 0);
        if (transform.localScale.x <= minSizeToDie)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("GAME OVER: La maldición te ha consumido.");
        gameObject.SetActive(false);
    }
}