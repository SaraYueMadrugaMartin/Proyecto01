using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorr3State : PlayerState
{
    public PlayerCorr3State(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Player.estadoCorr = 3;
        base.CambiaAnimaciones(Player.estadoCorr, Player.armaEquipada);
        velocidadBase = 0.7f;
        velocidadCorriendo = 1.3f;
        velocidadSigilo = 0.5f;

        Player.multiplicadorAtaque = 1.3f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Aqu� se comprueba la condici�n para el cambio de estado y se llama a change state de PlayerStateMachine
        if (Player.corrupcion >= 90)
            player.StateMachine.ChangeState(player.Corr4State);
        else if (Player.corrupcion < 25)
            player.StateMachine.ChangeState(player.Corr0State);
    }
}
