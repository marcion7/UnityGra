using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }

    [SerializeField]
    private float jumpForce;
    public float RunSpeed { get => runSpeed; }
    public float JumpForce { get => jumpForce; }
    public WEAPON Weapon { get => weapon; set => weapon = value; }

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private int lives;

    private WEAPON weapon;

    public Dictionary<WEAPON, bool> Weapons { get; set; } = new Dictionary<WEAPON, bool>();
    public int Lives { get => lives; set => lives = value; }
}
