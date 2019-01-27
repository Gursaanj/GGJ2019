using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy can only have one of each SkillType attack
/// </summary>
public abstract class BaseEnemyController : BasePlayer
{
    public EnemyDelegate onDeathDelegate;
    public delegate void EnemyDelegate();

    [SerializeField]
    RectTransform healthBar;

    protected GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void updateHealthbar()
    {
        healthBar.transform.localScale = new Vector3(_health / base._maxHealth, 1, 1);
    }

    protected override void onDeath()
    {
        //throw new System.NotImplementedException();
    }

    /// <summary>
    /// Use to call skill attacks
    /// </summary>
    public void SkillAttack(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Melee:
                MeleeAttack();
                break;
            case SkillType.Ranged:
                RangedAttack();
                break;
            case SkillType.CrowdControl:
                
                break;
        }
    }


    protected virtual void MeleeAttack()
    {
        
    }

    protected virtual void RangedAttack()
    {
        
    }

    protected virtual void CrowdControlAttack()
    {
        
    }
}
