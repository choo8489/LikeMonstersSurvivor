using System;
using UnityEngine;

using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    private int level;
    private float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.MaxGameTime / spawnData.Length;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.GameTime / levelTime), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        } 
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.Pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
