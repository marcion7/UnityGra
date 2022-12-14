using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField]
    private Transform lifeParent;

    [SerializeField]
    private GameObject lifePrefab;

    private Stack<GameObject> lives = new Stack<GameObject>();

    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType <UIManager>();
            }
            return instance;
        }
    }

    public void AddLife(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            lives.Push(Instantiate(lifePrefab, lifeParent));
        }
    }

    public void RemoveLife()
    {
        Destroy(lives.Pop());
    }
}
