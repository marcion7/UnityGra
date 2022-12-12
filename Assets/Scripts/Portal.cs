using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Vector2 TeleportTo;

    [SerializeField]
    private AudioClip TeleportationSound;

    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponentInParent<Player>();
            player.transform.position = TeleportTo;
            player.References.RespawnPoint = TeleportTo;
            SoundManager.instance.PlaySound(TeleportationSound);
        }
    }
}
