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

    public GameObject[] WeaponObjects { get => weaponObjects; set => weaponObjects = value; }
    public Transform GunBarrel { get => gunBarrel; private set => gunBarrel = value; }

    public GameObject BulletPrefab { get => bulletPrefab; private set => bulletPrefab = value; }
    public GameObject DartPrefab { get => dartPrefab; private set => dartPrefab = value; }
    public GameObject WaterPrefab { get => waterPrefab; private set => waterPrefab = value; }
}
