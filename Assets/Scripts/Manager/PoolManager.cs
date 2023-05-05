using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;

    private List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[enemys.Length];

        for (int i = 0; i < enemys.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (var item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(enemys[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
