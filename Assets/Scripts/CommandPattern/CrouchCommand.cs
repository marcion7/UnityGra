using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchCommand : Command
{
    private Player player;

    public CrouchCommand(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKeyDown()
    {
        player.Actions.Crouch();
    }

    public override void GetKeyUp()
    {
        player.Actions.StopCrouch();
    }
}
