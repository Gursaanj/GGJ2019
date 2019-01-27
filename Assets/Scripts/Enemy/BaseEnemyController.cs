using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy can only have one of each SkillType attack
/// </summary>
public abstract class BaseEnemyController : BasePlayer
{
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
