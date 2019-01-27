using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasePlayer {

    public DashState dashState;
    public float dashTimer;
    public float maxDash;
    public float dashMultiplier;


    private void FixedUpdate()
    {
        Shoot();
        Melee();
        Dashing();
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
}
