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
            animator.SetBool("Firing", true);
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

    public void StopAttack()
    {
        animator.SetBool("Firing", false);
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
                    if (DamageType == "Sword" || DamageType == "Bullet" || DamageType == "Hammer")
                        Hurt();
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
                        Hurt();
                    break;
                case EnemyType.TargerHead:
                    if (DamageType == "Dart")
                        Hurt();
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

    public void Hurt()
    {
        Lives--;
        animator.SetTrigger("Hurt");
    }

    IEnumerator DeleteAfterSek()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
