using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnPositions;
    public float time1;
    public float time2;

    void Start()
    {
        InvokeRepeating("Spawn", time1, time2);
    }

    void Spawn()
    {
        Instantiate(
            prefab,
            spawnPositions[Random.Range(0, spawnPositions.Length)].position,
            Quaternion.identity);
    }
}