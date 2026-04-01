using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables

    public bool isInGame;
    
    public bool isViolentMusicOn = false;
    
    public Player player;

    [Header("Audios")]
    public AudioClip calmMusic;
    public AudioClip violentMusic;
    public AudioClip dragonRoarClip; 

    
    private AudioSource musicSource;
    private AudioSource sfxSource;

    [Header("Tiempos aleatorios (en segundos)")]
    public float minCalmTime = 5f;
    public float maxCalmTime = 10f;
    public float minViolentTime = 5f;
    public float maxViolentTime = 8f;

    private float timer;

    public EnemySpawner enemySpawner;

    #endregion

    void Start()
    {
        InitialSettings();
    }

    void Update()
    {
        if (!isInGame) return;
        
        TickSettings();
    }

    void InitialSettings()
    {
        player.SetPlayerState(false);
        
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        SetCalmMode();
    }

    void TickSettings()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (isViolentMusicOn)
                SetCalmMode();
            else
                SetViolentMode();
        }
    }
    
    void SetCalmMode()
    {
        isViolentMusicOn = false;
        timer = Random.Range(minCalmTime, maxCalmTime);

        if (calmMusic != null)
        {
            musicSource.clip = calmMusic;
            musicSource.Play();
        }
    }

    void SetViolentMode()
    {
        isViolentMusicOn = true;
        timer = Random.Range(minViolentTime, maxViolentTime);
        
        if (dragonRoarClip != null)
        {
            sfxSource.PlayOneShot(dragonRoarClip);
        }
        
        if (violentMusic != null)
        {
            musicSource.clip = violentMusic;
            musicSource.PlayDelayed(0.2f); 
        }
    }

    public void InitializeGame()
    {
        isInGame = true;
        player.SetPlayerState(true);
        enemySpawner.isActive = true;
    }
}