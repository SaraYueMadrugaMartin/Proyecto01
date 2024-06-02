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
    static public int enemigosFinMalo = 1;
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
        sfxManager = SFXManager.instance;
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
            sfxManager.PlaySFX(sfxManager.correrAlex);
            //anim.Play(ControladorAnimaciones.diccionarioAnimaciones[4]); // Run animation
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            estaSigilo = true;
            // Anim sigilo
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
            anim.Play(ControladorAnimaciones.diccionarioAnimaciones[2]); // Walk animation
            // Sonido walk
            sfxManager.PlaySFX(sfxManager.pasosAlex);
        }
            
        else
            anim.Play(ControladorAnimaciones.diccionarioAnimaciones[1]); // Idle animation

        transform.Translate(velocidadX, 0, 0);
        transform.Translate(0, velocidadY, 0);

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
                anim.Play(ControladorAnimaciones.diccionarioAnimaciones[5]);
                // Sonido ataque
                sfxManager.PlaySFX(sfxManager.ataqueBate);

                // Detecta los enemigos en el rango de ataque 
                Collider2D[] golpeaEnemigos = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, enemigos);

                // Hacerles daño a los enemigos
                foreach (Collider2D enemigo in golpeaEnemigos)
                {
                    enemigo.GetComponent<Enemigo>().recibeDamage(dañoAtaque);
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
                    sfxManager.PlaySFX(sfxManager.disparoPistola);
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
                sfxManager.PlaySFX(sfxManager.recargaPistola);
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

        anim.Play(ControladorAnimaciones.diccionarioAnimaciones[3]);
        // Sonido hurt

        if (saludActual < 0f)
            Muere();
    }

    private void Muere()
    {
        // Animación muerte
        // Sonido muerte
        Invoke("MostrarPanelMuerte", 1.5f); // Espera a que termine la animación
    }

    private void MostrarPanelMuerte()
    {
        EntradaFinal.DesactivaPanel();
        panelMuerte.SetActive(true);
    }
    #endregion
    // Para ver el punto de ataque de Alex
    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
            return;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
