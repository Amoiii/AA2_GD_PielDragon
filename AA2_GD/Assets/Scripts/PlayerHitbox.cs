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
    public float maxSize = 3f; // LÍMITE MÁXIMO: Para no volverte un gigante imparable

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public Color calmColor = Color.white;
    public Color dragonColor = Color.black;
    public Sprite calmSprite;
    public Sprite dragonSprite;

    // Guardamos el tamaño absoluto aquí para no liarnos con los números negativos del flip
    private float currentSize;

    void Start()
    {
        // Al empezar, cogemos el tamaño inicial (siempre en positivo)
        currentSize = Mathf.Abs(transform.localScale.y);
    }

    void Update()
    {
        if (!isActive) return;

        if (gameManager.isViolentMusicOn)
        {
            // Encogemos restando a nuestra variable, NO a la escala directamente
            currentSize -= shrinkSpeed * Time.deltaTime;

            // Modo Dragón
            spriteRenderer.color = dragonColor;
            if (dragonSprite != null) spriteRenderer.sprite = dragonSprite;
        }
        else
        {
            // Modo Calma
            spriteRenderer.color = calmColor;
            if (calmSprite != null) spriteRenderer.sprite = calmSprite;
        }

        // Comprobamos si hemos muerto ANTES de aplicar la escala
        if (currentSize <= minSizeToDie)
        {
            Die();
            return;
        }

        // Límite duro: No podemos crecer más del maxSize
        if (currentSize > maxSize)
        {
            currentSize = maxSize;
        }

        // Aplicamos el tamaño. Mantenemos el signo X que le dio el PlayerMovement (Flip)
        float signX = Mathf.Sign(transform.localScale.x);
        transform.localScale = new Vector3(currentSize * signX, currentSize, 1f);
    }

    public void KillEnemy()
    {
        currentSize += growthPerKill;
    }

    public void TakeDamage()
    {
        currentSize -= shrinkPerHit;
        if (currentSize <= minSizeToDie)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("GAME OVER: La locura te ha consumido.");
        gameObject.SetActive(false);
    }
}