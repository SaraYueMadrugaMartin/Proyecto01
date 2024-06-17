using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Straight-Single Projectile", menuName = "Enemy Logic/Attack Logic/Straight Single Projectile")]

public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private float _timeBetweenShots = 2f;
    [SerializeField] private float _timeTillExit = 3f;
    [SerializeField] private float _distanceToCountExit = 3f;
    [SerializeField] private float _bulletSpeed = 10f;
    private Transform _posSalida;

    private float _timer;
    private float _exitTimer;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        _posSalida = enemy.transform.Find("PosSalida");
        if (_posSalida == null)
            Debug.LogWarning("No se ha encontrado posición de salida para la botella");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        enemy.MoveEnemy(Vector2.zero);

        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;
            enemy.anim.SetTrigger("ataca");
            enemy.PlaySonidosEnem(1); // Sonido ataque enemigo
            enemy.Coroutine(EsperaAnimacionBotella()); // Hay que llamar a la corrutina desde fuera del scriptable object
        }

        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;

            if (_exitTimer > _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }

        else
        {
            _exitTimer = 0f;
        }

        _timer += Time.deltaTime;
    }

    IEnumerator EsperaAnimacionBotella()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Vector2 posCentralJugador = new Vector2(playerTransform.position.x, playerTransform.position.y + 0.8f);
        Vector2 dir = (posCentralJugador - (Vector2)_posSalida.position).normalized;

        GameObject objetoBotella = PoolingBotellas.instancia.GetBotella();
        objetoBotella.transform.position = _posSalida.position;
        objetoBotella.transform.rotation = enemy.transform.rotation;
        Rigidbody2D botella = objetoBotella.GetComponent<Rigidbody2D>();
        botella.velocity = dir * _bulletSpeed;
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
