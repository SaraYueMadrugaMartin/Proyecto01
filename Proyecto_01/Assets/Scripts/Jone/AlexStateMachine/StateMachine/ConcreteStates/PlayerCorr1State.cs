using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorr1State : PlayerState
{
    public PlayerCorr1State(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Player.estadoCorr = 1;
        base.CambiaAnimaciones(Player.estadoCorr, Player.armaEquipada);
        velocidadBase = 0.9f;
        velocidadCorriendo = 1.5f;
        velocidadSigilo = 0.7f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Aquí se comprueba la condición para el cambio de estado y se llama a change state de PlayerStateMachine
        if (Player.corrupcion >= 50)
            player.StateMachine.ChangeState(player.Corr2State);
        else if (Player.corrupcion < 25)
            player.StateMachine.ChangeState(player.Corr0State);
    }
}
