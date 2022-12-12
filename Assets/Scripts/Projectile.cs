using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum projectileType { WaterDrop, Dart, Bullet, Enemy_Bullet, Enemy_Fireball};
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector2 direction;

    [SerializeField]
    private projectileType projectileType;

    [SerializeField]
    private string targetTag;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Setup(Vector2 direction)
    {
        this.direction = direction;

        GetComponent<SpriteRenderer>().flipX = direction.x == -0.7f ? true : false;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            switch (projectileType)
            {
                case projectileType.WaterDrop:
                {
                    collision.GetComponentInParent<IHitable>().TakeHit("WaterDrop");
                    break;
                }
                case projectileType.Dart:
                {
                    collision.GetComponentInParent<IHitable>().TakeHit("Dart");
                    break;
                }
                case projectileType.Enemy_Bullet:
                {
                    collision.GetComponentInParent<Player>().Actions.TakeHit();
                    break;
                }
                case projectileType.Enemy_Fireball:
                {
                    collision.GetComponentInParent<Player>().Actions.TakeHit();
                    break;
                }
                default:
                {
                    collision.GetComponentInParent<IHitable>().TakeHit("Bullet");
                    break;
                }
            }
            Destroy(gameObject);
        }
    }
}
