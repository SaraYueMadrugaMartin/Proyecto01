using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorr4State : PlayerState
{
    public PlayerCorr4State(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        // Stats base de velocidad y daño de ataque
        base.CambiaAnimaciones(4);
        Player.multiplicadorVelocidad = 0.6f;
        Player.multiplicadorAtaque = 1.4f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Aquí se comprueba la condición para el cambio de estado y se llama a change state de PlayerStateMachine
        if (Player.corrupcion < 25)
            player.StateMachine.ChangeState(player.Corr0State);
    }
}
