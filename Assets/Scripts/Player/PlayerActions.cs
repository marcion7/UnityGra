using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions
{
    private Player player;

    private bool IsPlayerCrouching = false;

    public PlayerActions(Player player)
    {
        this.player = player;
    }

    public void Move(Transform transform)
    {
        if (!IsPlayerCrouching)
        {
            player.Components.Rigidbody.velocity = new Vector2(player.Stats.Direction.x * player.Stats.RunSpeed * Time.deltaTime, player.Components.Rigidbody.velocity.y);

            if (player.Stats.Direction.x != 0)
            {
                transform.localScale = new Vector3(player.Stats.Direction.x < 0 ? -0.7f : 0.7f, 0.7f, 1);
                player.Components.Animator.TryPlayAnimation("Running");
            }
            else if (player.Components.Rigidbody.velocity == Vector2.zero)
            {
                player.Components.Animator.TryPlayAnimation("Idle");
            }
        }
    }

    internal void PickUpWeapon(WEAPON weapon)
    {
        player.Stats.Weapons[weapon] = true;
        switch (weapon)
        {
            case WEAPON.Sword:
                player.References.UIWeaponImages[0].SetActive(true);
                break;
            case WEAPON.WaterGun:
                player.References.UIWeaponImages[1].SetActive(true);
                break;
            case WEAPON.Pistol:
                player.References.UIWeaponImages[2].SetActive(true);
                break;
            case WEAPON.Hammer:
                player.References.UIWeaponImages[3].SetActive(true);
                break;
            case WEAPON.NerfGun:
                player.References.UIWeaponImages[4].SetActive(true); ;
                break;
            default:
                break;
        }
        SoundManager.instance.PlaySound(player.References.PickupSound);
    }

    internal void PickUpDoubleJump()
    {
        player.Stats.PickedUpDoubleJump = true;
        SoundManager.instance.PlaySound(player.References.PickupSound);
    }

    internal void PickUpLive()
    {
        if (player.Stats.Lives < 10)
        {
            player.Stats.Lives++;
            UIManager.Instance.AddLife(1);
        }
        SoundManager.instance.PlaySound(player.References.PickupSound);
    }

    public void Jump()
    {
        if (!IsPlayerCrouching)
        {
            if (player.Utilities.IsGrounded())
            {
                player.Components.Rigidbody.AddForce(new Vector2(0, player.Stats.JumpForce), ForceMode2D.Impulse);
                player.Components.Animator.TryPlayAnimation("Jumping");
                SoundManager.instance.PlaySound(player.References.JumpSound);
            }
            else if (player.Stats.CanDoubleJump)
            {
                player.Components.Collider.enabled = false;
                player.Components.Rigidbody.velocity = Vector2.zero;
                player.Components.Rigidbody.AddForce(new Vector2(0, player.Stats.DoubleJumpForce), ForceMode2D.Impulse);
                player.Stats.CanDoubleJump = false;
                player.Components.Collider.enabled = true;
                player.Components.Animator.TryPlayAnimation("Jumping");
                SoundManager.instance.PlaySound(player.References.JumpSound);
            }
        }
    }

    public void Attack()
    {
        if ((int)player.Stats.Weapon != 0)
        {
            if(player.Utilities.IsGrounded() == false)
            {
                player.Components.Animator.TryPlayAnimation("Air_Attacking");
            }
            else if(player.Components.Rigidbody.velocity == Vector2.zero)
            {
                player.Components.Animator.TryPlayAnimation("Attacking");  
            }
            else
            {
                player.Components.Animator.TryPlayAnimation("Run_Attacking");
            }
                
        }
    }
    public void Crouch()
    {
        IsPlayerCrouching = true;
        player.Components.Rigidbody.velocity = Vector2.zero;
        player.Components.Animator.TryPlayAnimation("Crouching");
    }

    public void StopCrouch()
    {
        IsPlayerCrouching = false;
        player.Components.Animator.OnAnimationDone("Crouching");
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
            GameObject projectilePrefab = player.References.BulletPrefab;
            if((int)player.Stats.Weapon == 5)
            {
                projectilePrefab = player.References.DartPrefab;
                SoundManager.instance.PlaySound(player.References.NerfGunSound);
            }
            else if ((int)player.Stats.Weapon == 2)
            {
                projectilePrefab = player.References.WaterPrefab;
                SoundManager.instance.PlaySound(player.References.WaterGunSound);
            }
            else
            {
                SoundManager.instance.PlaySound(player.References.PistolSound);
            }

            GameObject go = Object.Instantiate(projectilePrefab, player.References.GunBarrel.position, Quaternion.identity);
            Vector3 direction = new Vector3(player.transform.localScale.x, 0);
            go.GetComponent<Projectile>().Setup(direction);
        }
    }

    public void TakeHit(int damage = 1)
    {
        if (!player.Stats.IsImmortal)
        {
            if (player.Stats.Lives > 0)
            {
                player.Components.Animator.TryPlayAnimation("Hurt");
                SoundManager.instance.PlaySound(player.References.HurtSound);
                UIManager.Instance.RemoveLife();
                player.Stats.Lives -= damage;
            }
            
            if (player.Stats.Alive)
            {
                player.StartCoroutine(Immortality());
            }
            
            if (!player.Stats.Alive)
            {
                player.Components.Animator.TryPlayAnimation("Dying");
                SoundManager.instance.PlaySound(player.References.DyingSound);
                player.StartCoroutine(Respawn());
                player.StartCoroutine(Immortality());
            }
        }
    }

    private IEnumerator Blink()
    {
        while (player.Stats.IsImmortal)
        {
            player.Components.ChildObjectToBlink.SetActive(false);
            yield return new WaitForSeconds(.2f);
            player.Components.ChildObjectToBlink.SetActive(true);
            yield return new WaitForSeconds(.2f);
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        player.Components.Animator.TryPlayAnimation("Idle");
        player.transform.position = player.References.RespawnPoint;
        player.Stats.Lives = 3;
        UIManager.Instance.AddLife(3);
    }

    private IEnumerator Immortality()
    {
        player.Stats.IsImmortal = true;
        player.StartCoroutine(Blink());
        yield return new WaitForSeconds(player.Stats.ImmortalityTime);
        player.Stats.IsImmortal = false;
    }

    public void Collide(Collider2D collison)
    {
        if(collison.tag == "Collectible")
        {
            collison.GetComponent<ICollectibles>().Collect();
        }
    }
}
