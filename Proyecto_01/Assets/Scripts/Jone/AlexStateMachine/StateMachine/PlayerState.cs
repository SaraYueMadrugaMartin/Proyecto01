using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerState
{
    // Clase que define cómo serán los estados del jugador
    
    protected Player player;
    protected PlayerStateMachine playerStateMachine;

    protected float velocidadBase = 1f;
    protected float velocidadCorriendo = 1.6f;
    protected float velocidadSigilo = 0.8f;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate()
    {
        // Si se cambia el arma equipada hay que llamar a cambia animaciones con la nueva arma y el estado en el que estemos
        switch (Player.armaEquipada)
        {
            case 0:
                // Sin arma, no puede atacar
            break;
            case 1:
                // Con bate
                // Se desactiva puntero?
            break;
            case 2:
                // Con pistola
                // Deja de poder hacer ataque cuerpo a cuerpo
            break;
        }

        // Control de velocidad
        if (Player.estaCorriendo)
        {
            Player.multiplicadorVelocidad = velocidadCorriendo;
        }
        else if (Player.estaSigilo)
        {
            Player.multiplicadorVelocidad = velocidadSigilo;
        }
        else
        {
            Player.multiplicadorVelocidad = velocidadBase;
        }
    }
    public virtual void FixedUpdate() { }

    
    public virtual void CambiaAnimaciones(int estado, int arma)
    {
       ControladorAnimaciones.corrAnimaciones(estado, arma);
    }
}
