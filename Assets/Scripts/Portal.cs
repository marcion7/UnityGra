using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Vector2 TeleportTo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = TeleportTo;
    }
}
