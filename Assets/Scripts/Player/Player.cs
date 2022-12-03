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

        AnyStateAnimation[] animations = new AnyStateAnimation[]
        {
            new AnyStateAnimation("Idle", "Attacking"),
            new AnyStateAnimation("Running", "Jumping", "Attacking"),
            new AnyStateAnimation("Jumping"),
            new AnyStateAnimation("Attacking"),
        };

        components.Animator.AddAnimationts(animations);
    }

    private void Update()
    {
        utilities.HandleInput();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        actions.Move(transform);
    }
}
