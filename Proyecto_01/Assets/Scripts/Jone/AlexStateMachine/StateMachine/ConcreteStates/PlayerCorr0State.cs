using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorr0State : PlayerState
{
    // Plantear si hacer una interfaz o un struct con velocidad, daño ataque y anim
    
    public PlayerCorr0State(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Player.estadoCorr = 0;
        base.CambiaAnimaciones(Player.estadoCorr, Player.armaEquipada);
        Player.multiplicadorVelocidad = 1;
        Player.multiplicadorAtaque = 1;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        // Aquí se comprueba la condición para el cambio de estado y se llama a change state de PlayerStateMachine
        if (Player.corrupcion >= 25)
            player.StateMachine.ChangeState(player.Corr1State);
    }
}
