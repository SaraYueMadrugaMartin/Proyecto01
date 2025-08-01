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
        Player.estadoCorr = 4;
        base.CambiaAnimaciones(Player.estadoCorr, Player.armaEquipada);
        velocidadBase = 0.6f;
        velocidadCorriendo = 1.2f;
        velocidadSigilo = 0.4f;

        Player.multiplicadorAtaque = 1.4f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Aqu� se comprueba la condici�n para el cambio de estado y se llama a change state de PlayerStateMachine
        if (Player.corrupcion < 25)
            player.StateMachine.ChangeState(player.Corr0State);
    }
}
