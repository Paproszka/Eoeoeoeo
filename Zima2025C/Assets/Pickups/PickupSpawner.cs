using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject prefab;
    public List<Transform> spawnPositions = new();
    public float time1;
    public float time2;

    private int freeCount;

    void Start()
    {
        freeCount = spawnPositions.Count;
        InvokeRepeating("Spawn", time1, time2);
    }

    // Update is called once per frame
    void Spawn()
    {
        if (freeCount <= 0)
            return;

        int randomIndex = Random.Range(0, freeCount);
        Transform spawnPoint = spawnPositions[randomIndex];
        var prefabRef = Instantiate(prefab, spawnPoint.position, new Quaternion());
        prefabRef.GetComponent<SpeedPickup>().Init(spawnPoint);
        spawnPositions.Remove(spawnPoint);
        spawnPositions.Add(spawnPoint);
        freeCount--;
    }

    public void FreeSpace(Transform spawnPoint)
    {
        if (spawnPoint == null || !spawnPositions.Contains(spawnPoint))
            return;

        spawnPositions.Remove(spawnPoint);
        spawnPositions.Insert(freeCount, spawnPoint);
        freeCount++;
    }
}
