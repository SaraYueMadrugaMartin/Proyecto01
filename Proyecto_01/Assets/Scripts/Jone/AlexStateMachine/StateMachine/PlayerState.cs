using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public virtual void FrameUpdate() { }

    // public virtual void PhysicsUpdate() { }

    public virtual void CambiaAnimaciones(int estado)
    {
        switch (estado)
        {
            case 0:
                // Llamar al estado de animaciones 0
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
}
