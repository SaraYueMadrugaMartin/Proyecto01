using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Run Away", menuName = "Enemy Logic/Chase Logic/Run Away")]

public class EnemyChaseRunAway : EnemyChaseSOBase
{
    [SerializeField] private float _runAwaySpeed = 1.5f;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 runDir = -(playerTransform.position - transform.position).normalized;
        enemy.MoveEnemy(runDir * _runAwaySpeed);

        if(enemy.IsAggroed)
            enemy.PlaySonidosEnem(0); // Sonido idle
        else
            SFXManager.instance.StopSFXLoop();
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
