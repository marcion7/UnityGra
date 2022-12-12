using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject Portal;

    [SerializeField]
    private List<GameObject> enemies;

    void Update()
    {
        StartCoroutine(EnemyKilled());
        if (enemies.Count == 0)
        {
            Portal.SetActive(true);
        }
    }

    public IEnumerator EnemyKilled()
    {
        yield return 0;
        enemies.RemoveAll(item => item == null);
    }
}
