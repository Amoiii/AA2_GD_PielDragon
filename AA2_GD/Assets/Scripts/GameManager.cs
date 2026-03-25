using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isViolentMusicOn = false;

    [Header("Audios")]
    public AudioClip calmMusic;
    public AudioClip violentMusic;
    public AudioClip dragonRoarClip; // <-- Nuevo hueco para el rugido

    // Necesitamos DOS reproductores para que no se corten entre sí
    private AudioSource musicSource;
    private AudioSource sfxSource;

    [Header("Tiempos aleatorios (en segundos)")]
    public float minCalmTime = 5f;
    public float maxCalmTime = 10f;
    public float minViolentTime = 5f;
    public float maxViolentTime = 8f;

    private float timer;

    void Start()
    {
        // Creamos el reproductor de la música (en bucle)
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        // Creamos el reproductor para los efectos (NO en bucle)
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        SetCalmMode();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (isViolentMusicOn)
            {
                SetCalmMode();
            }
            else
            {
                SetViolentMode();
            }
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

        Debug.Log("Música TRANQUILA... Escóndete.");
    }

    void SetViolentMode()
    {
        isViolentMusicOn = true;
        timer = Random.Range(minViolentTime, maxViolentTime);

        // 1. Hacemos que suene el dragón INMEDIATAMENTE
        if (dragonRoarClip != null)
        {
            sfxSource.PlayOneShot(dragonRoarClip);
        }

        // 2. Preparamos la música violenta pero... ¡Le decimos que espere 1 segundo!
        if (violentMusic != null)
        {
            musicSource.clip = violentMusic;
            musicSource.PlayDelayed(1f); // <-- Aquí está el retraso de 1 segundo
        }

        Debug.Log("¡RUGIDO! La música violenta empieza en 1s...");
    }
}