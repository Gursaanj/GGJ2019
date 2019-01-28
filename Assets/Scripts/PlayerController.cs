using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasePlayer {

    [Header("Cool Down")]
    [SerializeField]
    float _meleeCooldownTime = 0.5f;
    [SerializeField]
    float _RangeCooldownTime = 0.5f;
    [SerializeField]
    float _DashCoolDownTime = 0.5f;

    float meleeCurrentCoolDownTime;
    float rangeCurrentCoolDownTime;
    float dashCurrentCoolDownTime;

     bool _isMeleeOnCoolDown = false;
     bool _isRangeOnCoolDown = false;
     bool _isDashOnCoolDown = false;

    [Header("Damage")]
    [SerializeField]
    protected float _meleeDamage = 10;
    [SerializeField]
    float attackRange = 1.3f;


    [Header("Dash Properties")]
    [SerializeField]
    float maxDash = 3;
    [SerializeField]
    float dashMultiplier = 5;

    [SerializeField]
    HealthController healthbar;

    bool isAlive = true;

    protected override void Awake()
    {
        base.Awake();
        setupCoolDownTime();
    }


    void setupCoolDownTime()
    {
        meleeCurrentCoolDownTime = _meleeCooldownTime;
        rangeCurrentCoolDownTime = _RangeCooldownTime;
        dashCurrentCoolDownTime = _DashCoolDownTime;
    }


    private void FixedUpdate()
    {
        if (isAlive) {
            Shoot();
            Melee();
            Dashing();
            Move();
            CoolDown();
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    #region Dashing
    void Dashing()
    {
        if (InputManager.isDashing() && !_isDashOnCoolDown)
        {
            Vector3 last = InputManager.lastPosition;
            GetComponent<SpriteRenderer>().flipX = last.x < 0;

            transform.Translate(last * dashMultiplier);
            string animName = (last.y != 0) ? "DashVert" : "DashHori";
            GetComponent<Animator>().SetTrigger(animName);

            _isDashOnCoolDown = true;
        }
    }

    #endregion

    #region Shooting
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && !_isRangeOnCoolDown)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float xChange = worldPosition.x - transform.position.x;
            float yChange = worldPosition.y - transform.position.y;
            float angle = Mathf.Atan2(yChange, xChange) * Mathf.Rad2Deg;
            ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position, Quaternion.Euler(0, 0, angle - 90));
            _isRangeOnCoolDown = true;
        }
    }
    #endregion


    #region Melee
    void Melee()
    {
        if (Input.GetMouseButtonDown(1) && !_isMeleeOnCoolDown)
        {
            GetComponent<Animator>().SetTrigger("Attack");
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, attackRange);
            damageEnemiesIfNeeded(enemiesHit);
            _isMeleeOnCoolDown = true;
        }
    }

    void damageEnemiesIfNeeded(Collider2D[] enemies)
    {
        foreach (Collider2D col in enemies)
        {
            GameObject enemy = col.gameObject;
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<BasePlayer>().ReduceHealth(_meleeDamage);
            }
        }
    }
    #endregion

    #region Move
    protected virtual void Move()
    {
        Vector3 direction = InputManager.MainInput();
        bool isWalking = direction != Vector3.zero;
        GetComponent<Animator>().SetBool("isWalking", isWalking);
        characterRigidBody.velocity = (Vector3.Normalize(direction) * _speed);
    }
    #endregion

    #region CoolDown
    private void CoolDown()
    {
        if (_isMeleeOnCoolDown)
        {
            meleeCurrentCoolDownTime -= Time.fixedDeltaTime;
            if (meleeCurrentCoolDownTime <= 0)
            {
                _isMeleeOnCoolDown = false;
                meleeCurrentCoolDownTime = _meleeCooldownTime;
            }
        }

        if (_isRangeOnCoolDown)
        {
            rangeCurrentCoolDownTime -= Time.fixedDeltaTime;
            if (rangeCurrentCoolDownTime <= 0)
            {
                _isRangeOnCoolDown = false;
                rangeCurrentCoolDownTime = _RangeCooldownTime;
            }
        }

        if (_isDashOnCoolDown)
        {
            dashCurrentCoolDownTime -= Time.fixedDeltaTime;
            if (dashCurrentCoolDownTime <= 0)
            {
                _isDashOnCoolDown = false;
                dashCurrentCoolDownTime = _DashCoolDownTime;
            }
        }
    }
    #endregion

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    protected override void updateHealthbar()
    {
        healthbar.decremeantHearts();
    }

    protected override void onDeath()
    {
        characterRigidBody.velocity = Vector2.zero;
        GetComponent<Animator>().SetTrigger("Death");
        Invoke("onDeathAnimationComplete", 1f);
        isAlive = false;
    }

    void onDeathAnimationComplete()
    {
        TransitionManager.instance.OnPlayerDeath();
    }
}
