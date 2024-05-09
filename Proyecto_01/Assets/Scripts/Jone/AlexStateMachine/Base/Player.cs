using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

// En esta clase se gestiona todo lo que tiene que ver con Alex y sus estados

public class Player : MonoBehaviour
{
    #region Player Stats

    // Corrupción
    static public int contadorCorr = 0; // Enemigos totales asesinados
    static public float corrupcion = 0; // Barra de corrupción
    static public int monedasCorr = 0;

    // Salud
    static public float saludMax = 100;
    static public float saludActual; // Barra salud

    static public int municion = 0;

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
        saludActual = saludMax;

        StateMachine.Initialize(Corr0State);
    }

    private void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
    }
}
