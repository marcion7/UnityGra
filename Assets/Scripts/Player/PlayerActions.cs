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
        player.Components.Animator.TryPlayAnimation("Attacking");
    }

    public void TrySwapWeapon(WEAPON weapon)
    {
        player.Stats.Weapon = weapon;
        SwapWeapon();
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
        }
    }
}
