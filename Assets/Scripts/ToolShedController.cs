using System.Collections;
using UnityEngine;

public class ToolShedController : BaseEnemyController
{
    private GameObject player;
    private Animator _animator;
    
    private SkillAttack meleeAttack;
    
    public int shots;
    public float meleeDistance;
        
    Vector3 playerDestination;
    bool keepShooting = true;

    private bool skillActive = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        
        meleeAttack = new RakeAttack();
    }
    
    private void Update()
    {
        if (!skillActive)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= meleeDistance)
            {
                MeleeAttack();
            }
            else
            {
                int r = Random.Range(0, 5);
                
                if (r == 0)
                    RangedAttack();
            }            
        }
    }

    protected override void Shoot()
    {
        // DOES NOTHING
    }

    protected override void Move()
    {
        // TO PREVENT IT FROM MOVING
    }
    
    protected override void MeleeAttack()
    {
        skillActive = true;
        meleeAttack.Init(_animator);
        meleeAttack.PlayAnimation();

        StartCoroutine(MeleeDone());
    }

    private IEnumerator MeleeDone()
    {
        yield return new WaitForSeconds(2f);

        skillActive = false;
        yield return null;
    }
        
    protected override void RangedAttack()
    {
        skillActive = true;
        StartCoroutine(ShootNails());
    }

    private IEnumerator ShootNails()
    {
        int i = shots;
        while (i > 0)
        {
            ShootNail();
            i--;
            yield return new WaitForSecondsRealtime(0.2f);
        }

        skillActive = false;

        yield return null;
    }

    private void ShootNail()
    {
        if (player != null)
            playerDestination = player.transform.position;
        
        float xChange = playerDestination.x - transform.position.x;
        float yChange = playerDestination.y - transform.position.y;
        float angle = Mathf.Atan2(yChange, xChange) * Mathf.Rad2Deg;

        ObjectPooler.Instance.SpawnFromPool("EnemyBullet", transform.position, Quaternion.Euler(0, 0, angle - 90));
    }
}
