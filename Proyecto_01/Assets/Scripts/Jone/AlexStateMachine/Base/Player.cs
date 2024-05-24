using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

// En esta clase se gestiona todo lo que tiene que ver con Alex y sus estados

public class Player : MonoBehaviour
{
    Animator anim;
    
    #region Variables Player Stats
    // Corrupci�n
    static public int contadorCorr = 0; // Enemigos totales asesinados
    static public float corrupcion = 0; // Barra de corrupci�n
    static public int estadoCorr = 0; // Identificador del estado de corrupci�n
    static public int monedasCorr = 0; // Contador de monedas para usar en gramola y bajar corrupci�n

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
    // Da�o
    [SerializeField] public float da�oAtaque = 20f;
    static public float multiplicadorAtaque = 1f;
    static public int armaEquipada = 0;

    // Cuerpo a cuerpo
    [SerializeField] float rangoAtaque = 0.5f;
    [SerializeField] Transform puntoAtaque;
    public float ratioAtaque = 2f;
    private float tiempoSiguienteAtaque = 0f;

    // A distancia
    private Puntero puntero;

    public LayerMask enemigos;
    #endregion

    #region Paneles
    [SerializeField] private GameObject panelMuerte;
    #endregion

    #region Variables M�quina Estado

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
        anim = GetComponent<Animator>();
        puntero = GetComponent<Puntero>();

        saludActual = saludMax;

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

    private void Velocidad()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            estaCorriendo = true;
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
            ;//anim.Play(ControladorAnimaciones.diccionarioAnimaciones[2]); // Walk animation
            // Sonido walk
        else
            ;//anim.Play(ControladorAnimaciones.diccionarioAnimaciones[1]); // Idle animation

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
            if (Input.GetMouseButtonDown(0)) // Click izquierdo del rat�n
            {
                // Animaci�n ataque
                // Sonido ataque

                // Detecta los enemigos en el rango de ataque 
                Collider2D[] golpeaEnemigos = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, enemigos);

                // Hacerles da�o a los enemigos
                foreach (Collider2D enemigo in golpeaEnemigos)
                {
                    enemigo.GetComponent<Enemigo>().recibeDa�o(da�oAtaque);
                }

                tiempoSiguienteAtaque = Time.time + 1f / ratioAtaque;
            }
        }
    }

    private void AtaquePistola()
    {

    }

    // Funci�n que se llama desde el ataque de los enemigos
    public void recibeDamage (float damage)
    {
        saludActual -= damage;
        Debug.Log("Salud: " + saludActual);

        // Animaci�n hurt
        // Sonido hurt

        if (saludActual < 0f)
            Muere();
    }

    private void Muere()
    {
        // Animaci�n muerte
        // Sonido muerte
        Invoke("MostrarPanelMuerte", 1.5f); // Espera a que termine la animaci�n
    }

    private void MostrarPanelMuerte()
    {
        panelMuerte.SetActive(true);
    }

    // Para ver el punto de ataque de Alex
    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
            return;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
