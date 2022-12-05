using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour, ICollectibles
{
    public void Collect()
    {
        FindObjectOfType<Player>().Actions.PickUpDoubleJump();
        Destroy(gameObject);
    }
}
