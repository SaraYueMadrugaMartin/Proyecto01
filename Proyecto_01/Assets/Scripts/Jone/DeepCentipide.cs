using System.Collections;
using UnityEngine;

public class DeepCentipide : MonoBehaviour
{
    [SerializeField] Transform puntoSalida;
    [SerializeField] Transform puntoLlegada;
    [SerializeField] Transform puntoComer;
    [SerializeField] float velocidad = 5f;
    [SerializeField] float umbralDistancia = 0.1f; // Umbral para determinar si ha llegado al punto de llegada
    bool quieto = false; // Para que deje de desplazarse una vez mate a Alex
    bool unaVez = true; // Para que solo haga una vez la animación de comer

    public AudioClip[] DeepCentipideClips;
    private AudioSource audioSource;
    private bool isPlayingLoop = false;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        this.transform.position = puntoSalida.position; // Empieza en el punto de salida
    }

    private void Update()
    {
        if (!quieto)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, puntoLlegada.position, velocidad * Time.deltaTime); // Se desplaza hacia el punto de llegada
       
            if (Vector2.Distance(this.transform.position, puntoLlegada.position) < umbralDistancia) // Comprueba si la distancia es menor que el umbral para saber si ha llegado
            {
                this.transform.position = puntoSalida.position; // Vuelve al inicio
            }

            // Si el audio ha terminado, reproducir otro clip aleatorio
            if (!audioSource.isPlaying)
            {
                PlayRandomDeepCentipide();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Cuando Alex entra en la zona ed impacto definida por el collider, la mata
    {
        if (collision.tag == "Player" && unaVez)
        {
            SFXManager.instance.PlaySFX(SFXManager.instance.clipsDeAudio[21]); // Sonido comer Alex
            collision.transform.position = puntoComer.position; // Posición de Alex para que quede bien la animación
            collision.GetComponent<Player>().SePuedeMover(false); // Que Alex no se pueda mover
            anim.SetTrigger("eat");
            StopSFXLoop();
            quieto = true;
            collision.GetComponent<Player>().recibeDamage(200); // Mata a Alex de un golpe
            StartCoroutine(Espera(collision));
            unaVez = false;
        }
    }

    IEnumerator Espera(Collider2D collision)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        collision.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(2f);
        collision.GetComponent<SpriteRenderer>().enabled = true;
        collision.GetComponent<Player>().SePuedeMover(true);
    }

    public void PlaySFXLoop(AudioClip efecto)
    {
        if (efecto != null)
        {
            audioSource.clip = efecto;
            audioSource.volume = SFXManager.instance.volumen * SFXManager.instance.volumenEfectos;
            audioSource.loop = false; // Porque tiene que intercalar audios
            audioSource.Play();
            isPlayingLoop = true;
        }
    }

    public void StopSFXLoop()
    {
        if (isPlayingLoop)
        {
            audioSource.Stop();
            isPlayingLoop = false;
        }
    }

    public void PlayRandomDeepCentipide()
    {
        if (DeepCentipideClips != null && DeepCentipideClips.Length > 0)
        {
            int randomIndex = Random.Range(0, DeepCentipideClips.Length);
            AudioClip randomClip = DeepCentipideClips[randomIndex];
            PlaySFXLoop(randomClip);
        }
    }
}
