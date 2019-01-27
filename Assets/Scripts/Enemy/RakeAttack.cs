public class RakeAttack : SkillAttack
{
    private const string MELEE_ATTACK = "MeleeAttack";
    
    public override void PlayAnimation()
    {
        _animator.SetTrigger(MELEE_ATTACK);
    }
}