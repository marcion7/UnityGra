using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtilities
{
    private Player player;

    private List<Command> commands = new List<Command>();
    public PlayerUtilities(Player player)
    {
        this.player = player;

        commands.Add(new JumpCommand(player, KeyCode.UpArrow));
        commands.Add(new JumpCommand(player, KeyCode.W));

        commands.Add(new AttackCommand(player, KeyCode.Space));

        commands.Add(new WeaponSwapCommand(player, WEAPON.Sword, KeyCode.Alpha1));
        commands.Add(new WeaponSwapCommand(player, WEAPON.WaterGun, KeyCode.Alpha2));
        commands.Add(new WeaponSwapCommand(player, WEAPON.Pistol, KeyCode.Alpha3));
        commands.Add(new WeaponSwapCommand(player, WEAPON.Hammer, KeyCode.Alpha4));
        commands.Add(new WeaponSwapCommand(player, WEAPON.NerfGun, KeyCode.Alpha5));
    }
    public void HandleInput()
    {
        player.Stats.Direction = new Vector2(Input.GetAxisRaw("Horizontal"), player.Components.Rigidbody.velocity.y);

        foreach (Command command in commands)
        {
            if (Input.GetKeyDown(command.Key))
            {
                command.GetKeyDown();
            }

            if (Input.GetKeyUp(command.Key))
            {
                command.GetKeyUp();
            }

            if (Input.GetKey(command.Key))
            {
                command.GetKey();
            }
        }
    }

    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(player.Components.Collider.bounds.center, player.Components.Collider.bounds.size, 0, Vector2.down, 0.1f, player.Components.GroundLayer);

        return hit.collider != null;
    }
}
