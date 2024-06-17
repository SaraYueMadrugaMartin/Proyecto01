using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]

public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float RandomMovementRange = 2f;
    [SerializeField] private float RandomMovementSpeed = 1f;

    [SerializeField] private float _stuckTimeThreshold = 1f;

    private Vector3 _targetPos;
    private Vector3 _direction;

    private Vector3 _lastPosition;
    private float _stuckTimer;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        _lastPosition = enemy.transform.position;
        _stuckTimer = 0f;
        _targetPos = GetRandomPointInCircle();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        _direction = (_targetPos - enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * RandomMovementSpeed);

        if ((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInCircle();
        }

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
            _targetPos = GetRandomPointInCircle();
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

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
    }
}
