public class ToolshedEnemyController : BaseEnemyController
{
    public SkillAttack meleeAttack;
    public SkillAttack rangedAttack;
        
        
    protected override void MeleeAttack()
    {
        meleeAttack.PlayAnimation();
    }
        
    protected override void RangedAttack()
    {
        
    }
}