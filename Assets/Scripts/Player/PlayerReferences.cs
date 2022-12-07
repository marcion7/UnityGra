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
    private GameObject swordImage;

    [SerializeField]
    private GameObject waterGunImage;

    [SerializeField]
    private GameObject pistolImage;

    [SerializeField]
    private GameObject hammerImage;

    [SerializeField]
    private GameObject nerfGunImage;

    public GameObject[] WeaponObjects { get => weaponObjects; set => weaponObjects = value; }
    public Transform GunBarrel { get => gunBarrel; private set => gunBarrel = value; }

    public GameObject BulletPrefab { get => bulletPrefab; private set => bulletPrefab = value; }
    public GameObject DartPrefab { get => dartPrefab; private set => dartPrefab = value; }
    public GameObject WaterPrefab { get => waterPrefab; private set => waterPrefab = value; }
    public GameObject SwordImage { get => swordImage; private set => swordImage = value; }
    public GameObject WaterGunImage { get => waterGunImage; private set => waterGunImage = value; }
    public GameObject PistolImage { get => pistolImage; private set => pistolImage = value; }
    public GameObject HammerImage { get => hammerImage; private set => hammerImage = value; }
    public GameObject NerfGunImage { get => nerfGunImage; private set => nerfGunImage = value; }
}
