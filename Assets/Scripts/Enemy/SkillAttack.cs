using UnityEngine;

public enum SkillType
{
    Melee = 0,
    Ranged = 1,
    CrowdControl = 2
}

public abstract class SkillAttack : MonoBehaviour
{
    public SkillType skillType;
    
    [Header("Tweakable Numbers")]
    public float damage;

    protected Animator _animator;
    
    public void Init(Animator animator)
    {
        _animator = animator;
    }

    public virtual void PlayAnimation()
    {
        _animator.StartPlayback();
    }

    public void StopAnimation()
    {
        _animator.StopPlayback();
    }
}