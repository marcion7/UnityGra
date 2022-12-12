using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerReferences
{
    [SerializeField]
    private GameObject[] weaponObjects;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject dartPrefab;

    [SerializeField]
    private GameObject waterPrefab;

    [SerializeField]
    private Transform gunBarrel;

    [SerializeField]
    private GameObject[] uIWeaponImages;

    private Vector3 respawnPoint;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip pickupSound;

    [SerializeField]
    private AudioClip hurtSound;

    [SerializeField]
    private AudioClip dyingSound;

    [SerializeField]
    private AudioClip slashSound;

    [SerializeField]
    private AudioClip nerfGunSound;

    [SerializeField]
    private AudioClip waterGunSound;

    [SerializeField]
    private AudioClip pistolSound;

    public GameObject[] WeaponObjects { get => weaponObjects; set => weaponObjects = value; }
    public Transform GunBarrel { get => gunBarrel; private set => gunBarrel = value; }

    public GameObject BulletPrefab { get => bulletPrefab; private set => bulletPrefab = value; }
    public GameObject DartPrefab { get => dartPrefab; private set => dartPrefab = value; }
    public GameObject WaterPrefab { get => waterPrefab; private set => waterPrefab = value; }
    public GameObject[] UIWeaponImages { get => uIWeaponImages; set => uIWeaponImages = value; }
    public Vector3 RespawnPoint { get => respawnPoint; set => respawnPoint = value; }
    public AudioClip JumpSound { get => jumpSound; set => jumpSound = value; }
    public AudioClip PickupSound { get => pickupSound; set => pickupSound = value; }
    public AudioClip HurtSound { get => hurtSound; set => hurtSound = value; }
    public AudioClip SlashSound { get => slashSound; set => slashSound = value; }
    public AudioClip NerfGunSound { get => nerfGunSound; set => nerfGunSound = value; }
    public AudioClip WaterGunSound { get => waterGunSound; set => waterGunSound = value; }
    public AudioClip PistolSound { get => pistolSound; set => pistolSound = value; }
    public AudioClip DyingSound { get => dyingSound; set => dyingSound = value; }
}
