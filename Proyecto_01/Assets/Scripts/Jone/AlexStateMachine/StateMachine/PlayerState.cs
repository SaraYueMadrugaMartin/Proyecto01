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

    
    public virtual void CambiaAnimaciones(int estado, int armaEquipada)
    {
       player.cambioAnimaciones(estado, armaEquipada);
    }
}
