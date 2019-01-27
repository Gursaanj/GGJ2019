using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HouseController : BaseEnemyController
{
    private const string BUTT_SLAM = "ButtSlam";
    private const string LIGHT = "Light";
    private const string DOOR_OPEN = "DoorOpen";
    private const string DOOR_CLOSE = "DoorClose";
    private const string DEATH = "Death";

    public GameObject mask;
    public SpriteRenderer warning;
    public GameObject carPrefab;
    public Transform doorTransform;
    public float meleeDistance;
    
    public int meleeDamage;
    public int rangedDamage;

    public float ccChance;
    public float meleeChance;
    public float rangedChance;

    private bool meleeHit;
    private bool skillActive;
    private bool isDead;
    
    private Animator _animator;
    private PolygonCollider2D warningCollider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        warningCollider = warning.gameObject.GetComponent<PolygonCollider2D>();
        warningCollider.enabled = false;
        HideWarning();
        mask.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;
        
        if (!skillActive)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            int rand = Random.Range(0, Mathf.FloorToInt(1 / ccChance));
            if (rand == 0)
            {
                skillActive = true;
                CrowdControlAttack();
                return;
            }
            else
            {
                if (distance <= meleeDistance)
                {
                    int r = Random.Range(0, Mathf.FloorToInt(1 / meleeChance));
                    if (r == 0)
                    {
                        skillActive = true;
                        MeleeAttack();
                        return;
                    }
                }
                else
                {
                    int r = Random.Range(0, Mathf.FloorToInt(1 / rangedChance));

                    if (r == 0)
                    {
                        skillActive = true;
                        RangedAttack();
                        return;
                    }
                }
            }
            
            Vector3 destination = new Vector3(player.transform.position.x, transform.position.y);
            transform.position = Vector3.MoveTowards(transform.position, destination, _speed * Time.fixedDeltaTime);
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            base.onDeathDelegate();
        }
    }
    
    protected override void onDeath()
    {
        isDead = true;
        _animator.SetTrigger("Death");
    }

    protected override void MeleeAttack()
    {
        _animator.SetTrigger(BUTT_SLAM);
        
        StartCoroutine(ShowWarning());
        StartCoroutine(MeleeDone());
    }

    private IEnumerator ShowWarning()
    {
        Color oldColor = warning.color;
        warning.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1f);
        
        yield return new WaitForSecondsRealtime(1f);
        
        HideWarning();
    }

    private void HideWarning()
    {
        Color oldColor = warning.color;
        warning.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0f);
        
        warningCollider.enabled = false;
        mask.SetActive(true);
        meleeHit = false;
    }
    
    private IEnumerator MeleeDone()
    {
        yield return new WaitForSeconds(2.4f);
        
        mask.SetActive(false);
        skillActive = false;
        yield return null;
    }

    protected override void RangedAttack()
    {
        _animator.SetTrigger(DOOR_OPEN);

        StartCoroutine(FireCar());
    }

    private IEnumerator FireCar()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        
        Vector2 playerDestination = player.transform.position;
        float xChange = playerDestination.x - doorTransform.position.x;
        float yChange = playerDestination.y - doorTransform.position.y;
        float angle = Mathf.Atan2(yChange, xChange) * Mathf.Rad2Deg;
        
        GameObject car = ObjectPooler.Instance.SpawnFromPool("Car", doorTransform.position, Quaternion.Euler(0, 0, angle + 270));
        car.transform.position = doorTransform.position;
        
        _animator.SetTrigger(DOOR_CLOSE);
        
        yield return new WaitForSecondsRealtime(4f);
        
        car.SetActive(false);
        
        yield return new WaitForSecondsRealtime(1f);
        skillActive = false;

        yield return null;
    }

    protected override void CrowdControlAttack()
    {
        _animator.SetTrigger(LIGHT);

        StartCoroutine(Light());
    }

    private IEnumerator Light()
    {
        yield return new WaitForSecondsRealtime(1f);

        skillActive = false;
    }
}