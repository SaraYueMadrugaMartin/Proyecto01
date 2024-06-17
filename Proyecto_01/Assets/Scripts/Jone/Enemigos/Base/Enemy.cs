using BBUnity.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; } = true;
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    #region Variables Máquina Estado

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }


    #endregion

    #region Variables ScriptableObjects

    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    #endregion

    public Animator anim;

    public int idEnemigo;

    public static int contadorEnemigosMuertos = 0;
    [SerializeField] int corrEnemigo = 20;
    public Player player;
    public Collider2D[] colliderEnemigo;
    public bool estaMuerto = false;
    public int tipoEnemigo;
    // 0: Lolita
    // 1: Rabit
    // 2: Boozy


    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        colliderEnemigo = GetComponents<Collider2D>();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    #region Funciones de Salud/Muerte
    
    // Función de recibir daño que se llama desde Player y desde Bala
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        anim.SetTrigger("recibeDamage");
        PlaySonidosEnem(2); // Sonido recibir daño

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    // Función de muerte del enemigo que se llama al recibir daño, cuando le baja la salud a 0
    public void Die()
    {
        SFXManager.instance.StopSFXLoop();
        MoveEnemy(Vector2.zero);
        anim.SetBool("seMuere", true);
        PlaySonidosEnem(3); // Sonido muerte
        //GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < colliderEnemigo.Length; i++)
            colliderEnemigo[i].enabled = false;
        estaMuerto = true;
        this.enabled = false;
        contadorEnemigosMuertos++;

        // Corrupcion jugador
        Player.contadorCorr += 1;
        Player.corrupcion += corrEnemigo;
        Debug.Log("Corrupción: " + Player.corrupcion + "%");

        // Destroy(this.gameObject, 1f);    // Si decidimos que queremos directamente eliminar al enemigo
    }

    public void GuardaMuerto()
    {
        anim.SetBool("isDead", true);
        this.enabled = false;
    }

    public int GetNumEnemMuertos()
    {
        return contadorEnemigosMuertos;
    }

    public void SetNumEnemMuertos(int enemMuertos)
    {
        contadorEnemigosMuertos = enemMuertos;
    }
    #endregion

    #region Funciones de Movimiento
    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f)
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;
            IsFacingRight = !IsFacingRight;
        }
    }
    #endregion

    #region Checks Distancia

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool (bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }

    #endregion

    #region Control Sonidos Enemigos
    public void PlaySonidosEnem (int numAnim)
    {
        switch (numAnim)
        {
            case 0:
                // IDLE
                switch (tipoEnemigo)
                {
                    case 0:
                        SFXManager.instance.PlaySFXLoop(SFXManager.instance.audiosEnemigos[0]);
                        break;
                    case 1:
                        SFXManager.instance.PlaySFXLoop(SFXManager.instance.audiosEnemigos[1]);
                        break;
                    case 2:
                        SFXManager.instance.PlaySFXLoop(SFXManager.instance.audiosEnemigos[2]);
                        break;
                }
                break;
            case 1:
                // ATTACK
                switch (tipoEnemigo)
                {
                    case 0:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[3]);
                        break;
                    case 1:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[4]);
                        break;
                    case 2:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[5]);
                        break;
                }
                break;
            case 2:
                // HURT
                switch (tipoEnemigo)
                {
                    case 0:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[6]);
                        break;
                    case 1:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[7]);
                        break;
                    case 2:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[8]);
                        break;
                }
                break;
            case 3:
                // DIE
                switch (tipoEnemigo)
                {
                    case 0:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[9]);
                        break;
                    case 1:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[10]);
                        break;
                    case 2:
                        SFXManager.instance.PlaySFX(SFXManager.instance.audiosEnemigos[11]);
                        break;
                }
                break;
        }
        
    }
    #endregion

    public void Coroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
