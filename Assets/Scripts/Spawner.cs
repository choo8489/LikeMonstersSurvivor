using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform[] spawnPoint;

    private float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f)
        {
            timer = 0;
            Spawn();
        } 
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.Pool.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position;

    }
}
