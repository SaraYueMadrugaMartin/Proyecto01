using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

// En esta clase se gestiona todo lo que tiene que ver con Alex y sus estados

public class Player : MonoBehaviour
{
    Animator anim;

    SFXManager sfxManager;
    
    #region Variables Player Stats
    // Corrupción
    static public int contadorCorr = 0; // Enemigos totales asesinados
    static public float corrupcion = 0; // Barra de corrupción
    static public int estadoCorr = 0; // Identificador del estado de corrupción
    static public int monedasCorr = 0; // Contador de monedas para usar en gramola y bajar corrupción
    static public int enemigosFinMalo = 2;
    Animator animCorr;

    // Salud
    static public float saludMax = 100;
    static public float saludActual; // Barra salud

    static public int municion = 0;
    #endregion

    #region Variables Player Movement
    [SerializeField] float movimiento = 2f;
    static public float multiplicadorVelocidad = 1f;
    private bool miraDerecha = true;
    public static bool estaCorriendo = false;
    public static bool estaSigilo = false;
    public static bool sePuedeMover = true;
    #endregion

    #region Variables Player Combat
    // Daño
    [SerializeField] public float dañoAtaque = 20f;
    static public float multiplicadorAtaque = 1f;
    static public int armaEquipada = 0;

    // Cuerpo a cuerpo
    [SerializeField] float rangoAtaque = 0.5f;
    [SerializeField] Transform puntoAtaque;
    public float ratioAtaque = 2f;
    private float tiempoSiguienteAtaque = 0f;

    // A distancia
    [SerializeField] Transform puntoDisparo;
    private Puntero puntero;
    private GameObject pistola;
    public static bool apuntando = false;
    public static bool recargando = false;
    private Animator animPistola, animBrazo;
    private int cargador = 6;
    public static bool direccionDerecha = true;

    public LayerMask enemigos;
    #endregion

    #region Paneles
    [SerializeField] private GameObject panelMuerte;
    #endregion

    #region Variables Máquina Estado

    public PlayerStateMachine StateMachine { get; set; }
    public PlayerCorr0State Corr0State { get; set; }
    public PlayerCorr1State Corr1State { get; set; }
    public PlayerCorr2State Corr2State { get; set; }
    public PlayerCorr3State Corr3State { get; set; }
    public PlayerCorr4State Corr4State { get; set; }

    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        Corr0State = new PlayerCorr0State(this, StateMachine);
        Corr1State = new PlayerCorr1State(this, StateMachine);
        Corr2State = new PlayerCorr2State(this, StateMachine);
        Corr3State = new PlayerCorr3State(this, StateMachine);
        Corr4State = new PlayerCorr4State(this, StateMachine);
    }

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        anim = GetComponent<Animator>();
        puntero = GetComponent<Puntero>();
        pistola = transform.Find("Puntero").gameObject;
        animPistola = pistola.GetComponent<Animator>(); //transform.Find("Puntero").GetComponent<Animator>();
        animBrazo = transform.Find("Puntero").Find("Brazo").GetComponent<Animator>();

        saludActual = saludMax;

        pistola.SetActive(false);
        puntero.enabled = false;

        StateMachine.Initialize(Corr0State);
    }

    private void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
        Ataque();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.FixedUpdate();
        Velocidad();
        Mover();       
    }

    #region Funciones Movimiento
    private void Velocidad()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            estaCorriendo = true;
            PlayPasosSound(sfxManager.clipsDeAudio[4]);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            estaSigilo = true;
            // Anim sigilo
        }
        else if (Input.GetKey(KeyCode.E))
        {
            estaCorriendo = false;
            estaSigilo = false;
            AlexAnimator.PlayAnimacion(6, anim, PrioridadAnimacion.Alta);
        }
        else
        {
            estaCorriendo = false;
            estaSigilo = false;
        }
       
    }
    private void Mover()
    {
        float velocidadX = Input.GetAxis("Horizontal") * movimiento * multiplicadorVelocidad * Time.deltaTime;
        float velocidadY = Input.GetAxis("Vertical") * movimiento * multiplicadorVelocidad * Time.deltaTime;

        if (velocidadX != 0f || velocidadY != 0f)
        {
            if (!estaCorriendo)
            {
                AlexAnimator.PlayAnimacion(2, anim, PrioridadAnimacion.Baja); // Walk animation
                // Sonido walk
                PlayPasosSound(sfxManager.clipsDeAudio[3]);
            }
            else
            {
                AlexAnimator.PlayAnimacion(4, anim, PrioridadAnimacion.Baja); // Run animation             
            }
        }
        else if (!apuntando)
        {
            AlexAnimator.PlayAnimacion(1, anim, PrioridadAnimacion.Baja); // Idle animation
            StopPasosSound();
        }
        else if (apuntando)
        {
            AlexAnimator.PlayAnimacion(10, anim, PrioridadAnimacion.Media); // Aiming animation
        }

        if (sePuedeMover)
        {
            transform.Translate(velocidadX, 0, 0);
            transform.Translate(0, velocidadY, 0);
        }    

        // Girar Sprite
        if (velocidadX > 0 && !miraDerecha)
        {
            Flip();
            puntero.VolteaPuntero();
        }
        if (velocidadX < 0 && miraDerecha)
        {
            Flip();
            puntero.VolteaPuntero();
        }
    }

    private void Flip()
    {
        miraDerecha = !miraDerecha;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }

    public void SePuedeMover(bool puede)
    {
        sePuedeMover = puede;
    }

    #endregion

    #region Funciones Ataque

    public void EquiparArma(int arma)
    {
        MostrarArmaEquipada.ArmaEquipada(arma);
        switch (arma)
        {
            case 0:
                Debug.Log("No tengo ningún arma equipada");
                armaEquipada = 0;
                break;
            case 1:
                Debug.Log("Equipo bate");
                armaEquipada = 1;
                break;
            case 2:
                Debug.Log("Equipo pistola");
                armaEquipada = 2;
                break;
        }
        ControladorAnimaciones.corrAnimaciones(estadoCorr, armaEquipada);
    }
    private void Ataque()
    {
        if (armaEquipada != 0)
        {
            if (armaEquipada == 1)
            {
                AtaqueBate();
            }
            else if (armaEquipada == 2)
            {
                AtaquePistola();
            }
        }
    }

    private void AtaqueBate()
    {
        if (Time.time >= tiempoSiguienteAtaque) // Cooldown entre ataques
        {
            if (Input.GetMouseButtonDown(0)) // Click izquierdo del ratón
            {
                AlexAnimator.PlayAnimacion(5, anim, PrioridadAnimacion.Alta); // Ataque bate animation
                                                                               // Sonido ataque

                sfxManager.PlayRandomAlexHit();
                sfxManager.PlaySFX(sfxManager.clipsDeAudio[5]);

                // Detecta los enemigos en el rango de ataque 
                Collider2D[] golpeaEnemigos = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, enemigos);

                // Hacerles daño a los enemigos
                foreach (Collider2D enemigo in golpeaEnemigos)
                {
                    Enemigo enemigoScript = enemigo.GetComponent<Enemigo>();
                    if (enemigoScript != null)
                    {
                        enemigoScript.recibeDamage(dañoAtaque);
                    }
                    else
                    {
                        // Para que funcione también con Xela:
                        Xela xela = enemigo.GetComponent<Xela>();
                        if (xela != null)
                        {
                            xela.recibeDamage(dañoAtaque);
                        }
                    }
                }

                tiempoSiguienteAtaque = Time.time + 1f / ratioAtaque;
            }
        }
    }

    private void AtaquePistola()
    {
        if (Input.GetButton("Fire2"))
        {
            // Implementación visual de número de balas

            pistola.SetActive(true);
            puntero.enabled = true;
            apuntando = true;           
            if (Input.GetButtonDown("Fire1"))
            {
                if (cargador > 0)
                {
                    Dispara();
                    sfxManager.PlaySFX(sfxManager.clipsDeAudio[6]);
                }

                else
                {
                    // Hay que mostrar un mensaje en pantalla
                    Debug.Log("Sin munición, pulsa R mientras apuntas para recargar");
                }
            }
            if (Input.GetKeyDown("r"))
            {
                Recarga();
                sfxManager.PlaySFX(sfxManager.clipsDeAudio[7]);
            }
        }
        else
        {
            pistola.SetActive(false);
            puntero.enabled = false;
            apuntando = false;
        }
    }

    private void Dispara()
    {
        animPistola.SetTrigger("dispara");
        animBrazo.SetTrigger("dispara");
        AlexAnimator.PlayAnimacion(7, anim, PrioridadAnimacion.Media); // Fire animation

        // Establece la dirección de la bala
        if (!Puntero.cambiaAngulos)
            direccionDerecha = true;
        else
            direccionDerecha = false;

        GameObject objetoBala = PoolingBalas.instancia.GetBala();

        objetoBala.transform.position = puntoDisparo.position;
        objetoBala.transform.rotation = puntoDisparo.rotation;

        --cargador;
    }

    private void Recarga()
    {
        if (municion > 0)
        {
            recargando = true;
            AlexAnimator.PlayAnimacion(8, anim, PrioridadAnimacion.Media); // Recharge animation
            StartCoroutine(CambiarValorDespuesDeEsperar());
            Debug.Log("Recargado");
            cargador = 6;
            municion -= 6;
        }
        else
        {
            Debug.Log("No hay suficiente munición");
            // Implementar texto de aviso
        }
    }

    public void PlayPasosSound(AudioClip clip)
    {
        if (!sfxManager.SFXScore.isPlaying)
        {
            sfxManager.SFXScore.clip = clip;
            sfxManager.SFXScore.Play();
        }
    }

    public void StopPasosSound()
    {
        if (sfxManager.SFXScore.isPlaying)
        {
            sfxManager.SFXScore.Stop();
        }
    }

    private IEnumerator CambiarValorDespuesDeEsperar()
    {
        yield return new WaitForSeconds(0.6f);
        recargando = false;
    }
    #endregion

    #region Funciones Damage
    // Función que se llama desde el ataque de los enemigos
    public void recibeDamage (float damage)
    {
        saludActual -= damage;
        Debug.Log("Salud: " + saludActual);

        AlexAnimator.PlayAnimacion(3, anim, PrioridadAnimacion.Alta);
        // Sonido hurt
        sfxManager.PlayRandomAlexHerida();

        if (saludActual < 0f)
            Muere();
    }

    private void Muere()
    {
        AlexAnimator.PlayAnimacion(9, anim, PrioridadAnimacion.Alta); // Die animation
        // Sonido muerte
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[18]);
        StartCoroutine(MostrarPanelMuerte());
        //Invoke("MostrarPanelMuerte", 1.5f); // Espera a que termine la animación
    }

    IEnumerator MostrarPanelMuerte()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        EntradaFinal.DesactivaPanel();
        panelMuerte.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Reintentar()
    {
        Time.timeScale = 1f;
        GameManager.instance.ReiniciarEscena();
        Debug.Log("Datos cargados.");
    }

    #endregion
    
    // Para ver el punto de ataque de Alex
    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
            return;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }



    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
    public bool GetMiraDerecha()
    {
        return miraDerecha;
    }

    public void SetMiraDerecha(bool value)
    {
        if (miraDerecha != value)
        {
            Flip(); // Si 'miraDerecha' es diferente de 'value', llamamos a la función 'Flip()'.
        }
    }   
}
