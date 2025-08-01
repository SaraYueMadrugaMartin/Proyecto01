using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorr0State : PlayerState
{
    // Plantear si hacer una interfaz o un struct con velocidad, da�o ataque y anim
    
    public PlayerCorr0State(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Player.estadoCorr = 0;
        base.CambiaAnimaciones(Player.estadoCorr, Player.armaEquipada);
        velocidadBase = 1f;
        velocidadCorriendo = 1.6f;
        velocidadSigilo = 0.8f;

        Player.multiplicadorAtaque = 1f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();       

        // Aqu� se comprueba la condici�n para el cambio de estado y se llama a change state de PlayerStateMachine
        if (Player.corrupcion >= 25)
            player.StateMachine.ChangeState(player.Corr1State);
    }
}
