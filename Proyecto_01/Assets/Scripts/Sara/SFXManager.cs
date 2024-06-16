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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        SFXScore.volume = volumen;
    }

    public void PlaySFX(AudioClip efecto)
    {
        if (efecto != null)
        {
            // Para que el sonido nos e corte en mitad de la acción creamos un GameObject temporal, mientras dura el sonido.
            GameObject tempGO = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempGO.AddComponent<AudioSource>();
            tempAudioSource.clip = efecto;
            tempAudioSource.volume = volumen;
            tempAudioSource.Play();
            tempAudioObjects.Add(tempGO);
            Destroy(tempGO, efecto.length); // Cuando acabe, se destruye el GameObjectTemporal.
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
}
