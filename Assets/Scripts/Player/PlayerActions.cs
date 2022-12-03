using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions
{
    private Player player;

    public PlayerActions(Player player)
    {
        this.player = player;
    }
    public void Move(Transform transform)
    {
        player.Components.Rigidbody.velocity = new Vector2(player.Stats.Direction.x * player.Stats.Speed * Time.deltaTime, player.Components.Rigidbody.velocity.y);

        if(player.Stats.Direction.x != 0)
        {
            transform.localScale = new Vector3(player.Stats.Direction.x < 0 ? -0.7f : 0.7f, 0.7f, 1);
            player.Components.Animator.TryPlayAnimation("Running");
        }
        else if(player.Components.Rigidbody.velocity == Vector2.zero)
        {
            player.Components.Animator.TryPlayAnimation("Idle");
        }
    }

    internal void PickUpWeapon(WEAPON weapon)
    {
        player.Stats.Weapons[weapon] = true;
    }

    public void Jump()
    {
        if (player.Utilities.isGrounded())
        {
            player.Components.Rigidbody.AddForce(new Vector2(0, player.Stats.JumpForce), ForceMode2D.Impulse);
            player.Components.Animator.TryPlayAnimation("Jumping");
        }
    }

    public void Attack()
    {
        if (player.Stats.Weapon != 0)
        {
            player.Components.Animator.TryPlayAnimation("Attacking");
        }
    }

    public void TrySwapWeapon(WEAPON weapon)
    {
        if (player.Stats.Weapons[weapon] == true)
        {
            player.Stats.Weapon = weapon;
            SwapWeapon();
        }
    }

    public void SwapWeapon()
    {
        for (int i = 1; i < player.References.WeaponObjects.Length; i++)
        {
            player.References.WeaponObjects[i].SetActive(false);
        }

        if (player.Stats.Weapon > 0)
        {
            player.References.WeaponObjects[(int)player.Stats.Weapon].SetActive(true);
            if((int)(player.Stats.Weapon) == 1 || (int)(player.Stats.Weapon) == 4)
            {
                player.Components.Animator.SetWeapon(0);
            }
            else
            {
                player.Components.Animator.SetWeapon(1);
            }
        }
    }

    public void Shoot(string animation)
    {
        if (animation == "Shoot")
        {
            GameObject go = GameObject.Instantiate(player.References.ProjectilePrefab, player.References.GunBarrel.position, Quaternion.identity);

            Vector3 direction = new Vector3(player.transform.localScale.x, 0);
            go.GetComponent<Projectile>().Setup(direction);
        }
    }

    public void TakeHit()
    {
        if(player.Stats.Lives > 0)
        {
            UIManager.Instance.RemoveLife();
            player.Stats.Lives--;
            player.Components.Animator.TryPlayAnimation("Hurt");
        }
    }

    public void Collide(Collider2D collison)
    {
        if(collison.tag == "Collectible")
        {
            collison.GetComponent<ICollectibles>().Collect();
        }
    }
}
