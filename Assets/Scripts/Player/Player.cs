using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private PlayerComponents components;

    [SerializeField]
    private PlayerReferences references;

    [SerializeField]
    private PlayerStats stats;

    private PlayerUtilities utilities;

    private PlayerActions actions;

    public PlayerComponents Components { get => components; }
    public PlayerStats Stats { get => stats; }
    public PlayerActions Actions { get => actions; }
    public PlayerUtilities Utilities { get => utilities; }
    public PlayerReferences References { get => references; }


    // Start is called before the first frame update
    private void Start()
    {
        actions = new PlayerActions(this);
        utilities = new PlayerUtilities(this);
        
        stats.Speed = stats.RunSpeed;

        stats.IsImmortal = false;

        AnyStateAnimation[] animations = new AnyStateAnimation[]
        {
            new AnyStateAnimation("Idle", "Attacking", "Hurt", "Dying"),
            new AnyStateAnimation("Running", "Jumping", "Attacking", "Run_Attacking", "Air_Attacking", "Hurt", "Dying"),
            new AnyStateAnimation("Jumping", "Hurt", "Dying"),
            new AnyStateAnimation("Attacking", "Hurt", "Dying"),
            new AnyStateAnimation("Run_Attacking", "Hurt", "Dying"),
            new AnyStateAnimation("Air_Attacking", "Hurt", "Dying"),
            new AnyStateAnimation("Hurt", "Dying"),
            new AnyStateAnimation("Dying")
        };

        Components.Animator.AnimationTriggerEvent += Actions.Shoot;

        stats.Weapons.Add(WEAPON.None, true);
        stats.Weapons.Add(WEAPON.Sword, false);
        stats.Weapons.Add(WEAPON.WaterGun, false);
        stats.Weapons.Add(WEAPON.Pistol, false);
        stats.Weapons.Add(WEAPON.Hammer, false);
        stats.Weapons.Add(WEAPON.NerfGun, false);

        UIManager.Instance.AddLife(stats.Lives);

        components.Animator.AddAnimations(animations);
    }

    private void Update()
    {
        if (stats.Alive)
        {
            utilities.HandleInput();
            if (utilities.IsGrounded() && Stats.PickedUpDoubleJump)
            {
                stats.CanDoubleJump = true;
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (stats.Alive)
        {
            actions.Move(transform);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actions.Collide(collision);
    }
}
