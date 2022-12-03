using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICollisionHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if(colliderName == "DamageArea" && other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Actions.TakeHit();
        }
    }
}
