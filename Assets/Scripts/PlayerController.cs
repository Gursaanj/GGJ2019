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
    float _DodgeCooldownTime = 0.5f;

    float meleeCurrentCoolDownTime;
    float rangeCurrentCoolDownTime;
    float dodgeCurrentCoolDownTime;

     bool _isMeleeOnCoolDown = false;
     bool _isRangeOnCoolDown = false;
     bool _isDodgeOnCoolDown = false;

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

    DashState dashState;
    float dashTimer = 0;


    private void Awake()
    {
        setupCoolDownTime();
    }


    void setupCoolDownTime()
    {
        meleeCurrentCoolDownTime = _meleeCooldownTime;
        rangeCurrentCoolDownTime = _RangeCooldownTime;
        dodgeCurrentCoolDownTime = _DodgeCooldownTime;
    }


    private void FixedUpdate()
    {
        Shoot();
        Melee();
        Dashing();
        Move();
        CoolDown();
    }

    #region Dashing
    private void Dashing()
    {
        switch (dashState)
        {
            case DashState.Ready:
                var isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
                if (isDashKeyDown)
                {
                    _speed *= dashMultiplier;
                    //Isdodging = true;
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 6;
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    //Isdodging = false;
                    _speed /= dashMultiplier;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
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

        if (_isDodgeOnCoolDown)
        {
            dodgeCurrentCoolDownTime -= Time.fixedDeltaTime;
            if (dodgeCurrentCoolDownTime <= 0)
            {
                _isDodgeOnCoolDown = false;
                dodgeCurrentCoolDownTime = _DodgeCooldownTime;
            }
        }
    }
    #endregion

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    protected override void updateHealthbar()
    {
        //TODO - Update the heart
    }

    protected override void onDeath()
    {
        //TODO - Destroy gameobject?
    }
}
