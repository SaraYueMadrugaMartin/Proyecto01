using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] public AudioSource SFXScore;

    public AudioClip[] clipsDeAudio;

    public AudioClip[] audiosEnemigos;

    [Range(0f, 1f)] public float volumen;

    public AudioClip[] AlexHitClips;
    public AudioClip[] AlexHeridaClips;

    private List<GameObject> tempAudioObjects = new List<GameObject>();

    private float volumenEfectos = 1.0f; // Multiplicador de volumen de efectos

    private bool isPlayingLoop = false;
    private AudioSource loopAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        SFXScore.volume = volumen * volumenEfectos;
    }

    public void PlaySFX(AudioClip efecto)
    {
        if (efecto != null)
        {
            // Para que el sonido nos e corte en mitad de la acción creamos un GameObject temporal, mientras dura el sonido.
            GameObject tempGO = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempGO.AddComponent<AudioSource>();
            tempAudioSource.clip = efecto;
            tempAudioSource.volume = volumen * volumenEfectos;
            tempAudioSource.Play();
            tempAudioObjects.Add(tempGO);
            Destroy(tempGO, efecto.length); // Cuando acabe, se destruye el GameObjectTemporal.
        }
    }

    public void PlaySFXLoop(AudioClip efecto)
    {
        if (efecto != null && !isPlayingLoop)
        {
            if (loopAudioSource == null)
            {
                GameObject tempGO = new GameObject("LoopAudio");
                loopAudioSource = tempGO.AddComponent<AudioSource>();
            }

            loopAudioSource.clip = efecto;
            loopAudioSource.volume = volumen * volumenEfectos;
            loopAudioSource.loop = true;
            loopAudioSource.Play();
            isPlayingLoop = true;
        }
    }

    public void StopSFXLoop()
    {
        if (loopAudioSource == null)
            Debug.LogWarning("No encuentra audio loop");
        if (loopAudioSource != null && isPlayingLoop)
        {
            loopAudioSource.Stop();
            isPlayingLoop = false;
        }
    }

    public void PlayRandomAlexHit()
    {
        if (AlexHitClips != null && AlexHitClips.Length > 0)
        {
            int randomIndex = Random.Range(0, AlexHitClips.Length);
            AudioClip randomClip = AlexHitClips[randomIndex];
            PlaySFX(randomClip);
        }
    }

    public void PlayRandomAlexHerida()
    {
        if (AlexHeridaClips != null && AlexHeridaClips.Length > 0)
        {
            int randomIndex = Random.Range(0, AlexHeridaClips.Length);
            AudioClip randomClip = AlexHeridaClips[randomIndex];
            PlaySFX(randomClip);
        }
    }

    public void StopAudios()
    {
        foreach (GameObject tempGO in tempAudioObjects)
        {
            if (tempGO != null)
            {
                AudioSource tempAudioSource = tempGO.GetComponent<AudioSource>();
                if (tempAudioSource != null)
                {
                    tempAudioSource.Stop();
                }
                Destroy(tempGO);
            }
        }
        tempAudioObjects.Clear();
    }

    #region AjusteVolumen
    public void ActualizarVolumenEfectos(float nuevoVolumen)
    {
        volumenEfectos = nuevoVolumen;
        // Guardar el volumen de los efectos
        GuardarVolumen();
    }

    private void GuardarVolumen()
    {
        PlayerPrefs.SetFloat("VolumenEfectos", volumenEfectos);
        PlayerPrefs.Save();
    }

    private void CargarVolumen()
    {
        if (PlayerPrefs.HasKey("VolumenEfectos"))
        {
            volumenEfectos = PlayerPrefs.GetFloat("VolumenEfectos");
            ActualizarVolumenEfectos(volumenEfectos);
        }
    }
    #endregion
}
