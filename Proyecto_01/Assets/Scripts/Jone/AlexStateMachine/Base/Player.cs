using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

// En esta clase se gestiona todo lo que tiene que ver con Alex y sus estados

public class Player : MonoBehaviour
{
    Animator anim;
    #region Variables Player Stats

    // Corrupción
    static public int contadorCorr = 0; // Enemigos totales asesinados
    static public float corrupcion = 0; // Barra de corrupción
    static public int estadoCorr = 0; // Identificador del estado de corrupción
    static public int monedasCorr = 0; // Contador de monedas para usar en gramola y bajar corrupción

    // Salud
    static public float saludMax = 100;
    static public float saludActual; // Barra salud

    static public int municion = 0;

    #endregion

    #region Variables Player Movement
    [SerializeField] float movimiento = 2f;
    static public float multiplicadorVelocidad = 1f;
    private bool miraDerecha = true;
    private bool estaCorriendo = false;
    private bool cambiaEstado = false;

    #endregion

    #region Variables Player Combat
    static public float multiplicadorAtaque = 1f;
    private Puntero puntero;
    static public int armaEquipada = 0;

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
        anim = GetComponent<Animator>();
        puntero = GetComponent<Puntero>();

        saludActual = saludMax;

        StateMachine.Initialize(Corr0State);
    }

    private void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
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
            
            anim.Play(ControladorAnimaciones.diccionarioAnimaciones[4]); // Run animation
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            multiplicadorVelocidad -= 0.2f;
            // Anim sigilo
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            estaCorriendo = false;
            cambiaEstado = false;
            multiplicadorVelocidad -= 0.6f;
            // parar la animación de correr
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            multiplicadorVelocidad += 0.2f;
            // parar anim sigilo
        }
        if (estaCorriendo && !cambiaEstado)
        {            
            multiplicadorVelocidad += 0.6f;
            Debug.Log("cambia vel");
            cambiaEstado = true;
        }
    }
    private void Mover()
    {
        float velocidadX = Input.GetAxis("Horizontal") * movimiento * multiplicadorVelocidad * Time.deltaTime;
        float velocidadY = Input.GetAxis("Vertical") * movimiento * multiplicadorVelocidad * Time.deltaTime;

        if (velocidadX != 0f || velocidadY != 0f)
            anim.Play(ControladorAnimaciones.diccionarioAnimaciones[2]); // Walk animation
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
}
