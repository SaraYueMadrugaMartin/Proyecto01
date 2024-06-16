using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed = 1.75f;
    [SerializeField] private float _stuckTimeThreshold = 2f;

    private Vector3 _lastPosition;
    private float _stuckTimer;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _lastPosition = enemy.transform.position;
        _stuckTimer = 0f;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 moveDirection = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(moveDirection * _movementSpeed);

        // Comprueba si la posición del enemigo ha cambiado
        if (Vector3.Distance(enemy.transform.position, _lastPosition) < 0.02f)
        {
            _stuckTimer += Time.deltaTime;
        }
        else
        {
            _stuckTimer = 0f;
        }

        // Actualiza la última posición para la comprobación de movimiento
        _lastPosition = enemy.transform.position;

        // Si el enemigo ha estado atascado más del tiempo permitido, vuelve al estado de idle
        if (_stuckTimer >= _stuckTimeThreshold)
        {
            Debug.Log("Lleva mucho tiempo quieto, vuelve a idle");
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void DoPhysiscsLogic()
    {
        base.DoPhysiscsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
