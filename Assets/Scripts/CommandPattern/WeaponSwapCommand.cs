using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPON { None, Sword, WaterGun, Pistol, Hammer, NerfGun };

public class WeaponSwapCommand : Command
{
    private Player player;
    private WEAPON weapon;

    public WeaponSwapCommand(Player player, WEAPON weapon, KeyCode key) : base(key)
    {
        this.weapon = weapon;
        this.player = player;
    }

    public override void GetKeyDown()
    {
        player.Actions.TrySwapWeapon(weapon);
    }
}
