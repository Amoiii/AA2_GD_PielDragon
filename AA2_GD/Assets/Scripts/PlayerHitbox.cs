using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public bool isActive;
    
    public GameManager gameManager;

    [Header("Size Feature")]
    public float shrinkSpeed = 0.5f;
    public float growthPerKill = 0.3f;
    public float shrinkPerHit = 0.4f;
    public float minSizeToDie = 0.1f;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer; 
    public Color calmColor = Color.white; 
    public Color dragonColor = Color.black; 

    
    public Sprite calmSprite;
    public Sprite dragonSprite;

    void Update()
    {
        if (!isActive) return;
        
        if (gameManager.isViolentMusicOn)
        {
            // Decrease size
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, 0) * Time.deltaTime;

            // Drake mode
            spriteRenderer.color = dragonColor;
            if (dragonSprite != null) spriteRenderer.sprite = dragonSprite;
        }
        else
        {
            // Default mode 
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
        Debug.Log("GAME OVER");
        gameObject.SetActive(false);
    }
}