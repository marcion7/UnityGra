using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePickup : MonoBehaviour, ICollectibles
{
    public void Collect()
    {
        FindObjectOfType<Player>().Actions.PickUpLive();
        Destroy(gameObject);
    }
}
