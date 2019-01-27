using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToolShedController : BaseEnemyController
{
    private Animator _animator;
    
    private SkillAttack meleeAttack;

    public float meleeChance;
    public float rangedChance;

    public int meleeDamage;
    public int rangedDamage;

    private bool meleeHit;

    public SpriteRenderer cone;
    public GameObject rippleMask;
    public ParticleSystem ripple;
    private PolygonCollider2D coneCollider;
    
    public int shots;
    public float meleeDistance;
        
    Vector3 playerDestination;
    bool keepShooting = true;

    private bool skillActive = false;

    private void Start()
    {
        coneCollider = cone.gameObject.GetComponent<PolygonCollider2D>();
        coneCollider.enabled = false;
        HideCone();
        rippleMask.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(waitPlease(2.0f));
        //_animator = GetComponent<Animator>();

        //meleeAttack = new RakeAttack();
    }

    IEnumerator waitPlease(float wait)
    {
        yield return new WaitForSeconds(wait);
        _animator = GetComponent<Animator>();

        meleeAttack = new RakeAttack();
    }

    private void FixedUpdate()
    {
        if (!skillActive)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= meleeDistance)
            {
                int r = Random.Range(0, Mathf.FloorToInt(1 / meleeChance));
                if (r == 0)
                    MeleeAttack();
            }
            else
            {
                int r = Random.Range(0, Mathf.FloorToInt(1 / rangedChance));
                
                if (r == 0)
                    RangedAttack();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!meleeHit)
            {
                player.GetComponent<BasePlayer>().ReduceHealth(meleeDamage);
                print("Player got hit by: House Melee Skill");
                meleeHit = true;
            }
        }
    }

    protected override void MeleeAttack()
    {
        skillActive = true;
        meleeAttack.Init(_animator);
        StartCoroutine(ShowCone());
        meleeAttack.PlayAnimation();

        StartCoroutine(MeleeDone());
    }

    private IEnumerator MeleeDone()
    {
        yield return new WaitForSeconds(2.4f);
        
        rippleMask.SetActive(false);
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

    private IEnumerator ShowCone()
    {
        float xChange = transform.position.x - player.transform.position.x;
        float yChange = transform.position.y - player.transform.position.y;
        float angle = Mathf.Atan2(yChange, xChange) * Mathf.Rad2Deg;
        cone.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        rippleMask.transform.rotation = Quaternion.Euler(0, 0, angle - 90); 
        rippleMask.SetActive(false);
        
        Color oldColor = cone.color;
        cone.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1f);
        
        yield return new WaitForSeconds(1f);
        
        HideCone();

        yield return null;
    }

    private void HideCone()
    {
        Color oldColor = cone.color;
        cone.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0f);

        coneCollider.enabled = true;
        rippleMask.SetActive(true);
        meleeHit = false;
    }
}
