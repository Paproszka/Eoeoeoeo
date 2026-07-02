using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public Transform SpawnPosition;

    public void Init(Transform spawnPosition)
    {
        SpawnPosition = spawnPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<PickupSpawner>().FreeSpace(SpawnPosition);
            other.GetComponent<PlayerController>().ActivateSpeedBoost();
            Destroy(gameObject);
        }
    }
}
