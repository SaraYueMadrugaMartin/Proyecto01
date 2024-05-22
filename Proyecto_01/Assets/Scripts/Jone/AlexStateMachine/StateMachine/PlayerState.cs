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
        // Aquí tiene que hacer la comprobación de velocidad
    }
    public virtual void FixedUpdate() { }

    public virtual void CambiaAnimaciones(int estado, int arma)
    {
       ControladorAnimaciones.corrAnimaciones(estado, arma);
    }
    
}
