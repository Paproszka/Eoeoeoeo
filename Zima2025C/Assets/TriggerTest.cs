using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject prefab;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}