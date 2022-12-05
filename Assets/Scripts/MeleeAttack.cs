using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Sword, Hammer };
public class MeleeAttack : MonoBehaviour
{

    [SerializeField]
    private WeaponType WeaponType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hit = collision.GetComponentInParent<IHitable>();

        if(hit != null)
        {
            if (WeaponType == WeaponType.Sword)
                hit.TakeHit("Sword");
            else
                hit.TakeHit("Hammer");
        }
    }
}
