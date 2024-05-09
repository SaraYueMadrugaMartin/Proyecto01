using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorr2State : PlayerState
{
    public PlayerCorr2State(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        // Stats base de velocidad y daño de ataque
        // Animaciones base de estado 0
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Aquí se comprueba la condición para el cambio de estado y se llama a change state de PlayerStateMachine
        if (Player.corrupcion >= 75)
            player.StateMachine.ChangeState(player.Corr3State);
        else if (Player.corrupcion < 25)
            player.StateMachine.ChangeState(player.Corr0State);
    }
}
