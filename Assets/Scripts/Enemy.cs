using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Basic, Fire, Armored, Stone, TargerHead };
public class Enemy : MonoBehaviour, ICollisionHandler, IHitable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int Lives = 2;

    [SerializeField]
    private float speed;

    private float DistanceBeetweenPlayerToStop
    {
        get
        {
            if (EnemyType == EnemyType.Fire || EnemyType == EnemyType.Armored)
            {
                return 7;
            }
            else
                return 1.5f;
        }
    }

    [SerializeField]
    private EnemyType EnemyType;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform gunBarrel;

    private Transform target;

    [SerializeField]
    private float attackCooldown;

    private bool canAttack = true;

    private float timeSinceAttack;

    private void Update()
    {
        LookAtTarget();
        MoveToTarget();
        Attack();
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (Lives > 0)
        {
            if (colliderName == "DamageArea" && other.CompareTag("Player"))
            {
                other.GetComponent<Player>().Actions.TakeHit();
            }
            if (colliderName == "Sight" && other.CompareTag("Player"))
            {
                if(target == null)
                {
                    this.target = other.transform;
                }
            }
        }
    }

    public void CollisionExit(string colliderName, GameObject other)
    {
        if (colliderName == "Sight" && other.CompareTag("Player"))
        {
            target = null;
        }
    }

    private void Attack()
    {
        if (!canAttack)
        {
            timeSinceAttack += Time.deltaTime;
        }

        if(timeSinceAttack >= attackCooldown)
        {
            canAttack = true;
        }

        if(canAttack && target != null && Mathf.Abs(target.transform.position.y - transform.position.y) <= 1)
        {
            canAttack = false;
            timeSinceAttack = 0;
            if (EnemyType == EnemyType.Fire || EnemyType == EnemyType.Armored)
            {
                animator.SetBool("Firing", true);
            }
            else
            {
                animator.SetBool("Punching", true);
            }
        }
    }

    public void StopAttack()
    {
        if (EnemyType == EnemyType.Fire || EnemyType == EnemyType.Armored)
        {
            animator.SetBool("Firing", false);
        }
        else 
        { 
            animator.SetBool("Punching", false);
        }
    }

    private void LookAtTarget()
    {
        if(target != null)
        {
            Vector3 scale = transform.localScale;
            scale.x = target.transform.position.x < transform.position.x ? -0.7f : 0.7f;

            transform.localScale = scale;
        }
    }

    private void MoveToTarget()
    {
        if (target != null)
        {
            if (Mathf.Abs(target.transform.position.x - transform.position.x) <= DistanceBeetweenPlayerToStop
                || Mathf.Abs(target.transform.position.y - transform.position.y) >= 3)
            {
                animator.SetBool("Running", false);
            }
            else
            {
                Vector3 destination = target.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                animator.SetBool("Running", true);
            }
        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity);
        Vector3 direction = new Vector3(transform.localScale.x, 0);

        go.GetComponent<Projectile>().Setup(direction);
    }

    public void TakeHit(string DamageType)
    {
        if (Lives > 1)
        {
            switch (EnemyType)
            {
                case EnemyType.Basic:
                    if (DamageType == "Sword" || DamageType == "Hammer")
                    {
                        Hurt();
                    }
                    else if(DamageType == "Bullet")
                    {
                        Hurt(2);
                    }
                    break;
                case EnemyType.Fire:
                    if (DamageType == "WaterDrop")
                        Hurt();
                    break;
                case EnemyType.Armored:
                    if (DamageType == "Sword" || DamageType == "Hammer")
                        Hurt();
                    break;
                case EnemyType.Stone:
                    if (DamageType == "Hammer")
                    {
                        Hurt(3);
                    }
                    else if (DamageType == "WaterDrop")
                    {
                        Hurt(2);
                    }
                    break;
                case EnemyType.TargerHead:
                    if (DamageType == "Dart")
                    {
                        Hurt(3);
                    }
                    else if (DamageType == "Bullet")
                    {
                        Hurt();
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("Dying");
        StartCoroutine(DeleteAfterSek());
    }

    public void Hurt(int damage = 1)
    {
        Lives -= damage;
        animator.SetBool("Hurt", true);
    }

    public void StopHurt()
    {
        animator.SetBool("Hurt", false);
    }

    IEnumerator DeleteAfterSek()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
