using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public Vector2 Direction { get; set; }

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float doubleJumpForce;

    public bool PickedUpDoubleJump { get; set; } = false;

    public float RunSpeed { get => runSpeed; }
    public float JumpForce { get => jumpForce; }
    public float DoubleJumpForce { get => doubleJumpForce; }
    public WEAPON Weapon { get => weapon; set => weapon = value; }

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private int lives;

    [SerializeField]
    private float immortalityTime;
    public bool CanDoubleJump { get; set; } = false;

    private WEAPON weapon;

    public bool Alive
    {
        get { return lives > 0; }
    }

    public bool IsImmortal { get; set; }

    public Dictionary<WEAPON, bool> Weapons { get; set; } = new Dictionary<WEAPON, bool>();
    public int Lives { get => lives; set => lives = value; }
    public float ImmortalityTime { get => immortalityTime; set => immortalityTime = value; }
 
}
