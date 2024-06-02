using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosXela : MonoBehaviour
{
    [SerializeField] public AudioSource Xela;

    public AudioClip[] audiosXela;

    public AudioClip[] idleXela;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudiosXela(AudioClip efecto, float volumen)
    {
        if (efecto != null)
        {
            // Para que el sonido nos e corte en mitad de la acción creamos un GameObject temporal, mientras dura el sonido.
            GameObject tempGO = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempGO.AddComponent<AudioSource>();
            tempAudioSource.clip = efecto;
            tempAudioSource.Play();

            Destroy(tempGO, efecto.length); // Cuando acabe, se destruye el GameObjectTemporal.
        }
    }

    public void PlayRandomXelaIdle()
    {
        if (idleXela != null && idleXela.Length > 0)
        {
            int randomIndex = Random.Range(0, idleXela.Length);
            AudioClip randomClip = idleXela[randomIndex];
            PlayAudiosXela(randomClip, 1.0f);
        }
    }
}
